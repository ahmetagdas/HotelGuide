using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data.Entity;
using MassTransit;

namespace OtelService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtelController : ControllerBase
    {
        private readonly OtelDbContext _context;
        private readonly IPublishEndpoint _publishEndpoint;

        public OtelController(OtelDbContext context,
                              IPublishEndpoint publishEndpoint)
        {
            _context = context;
            _publishEndpoint = publishEndpoint;
        }

        // GET: api/otel
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Otel>>> GetOteller()
        {
            var oteller = await _context.Oteller.ToListAsync();
            return oteller;
        }

        // GET: api/otel/5
        [HttpGet("{otelId}")]
        public async Task<ActionResult<Otel>> GetOtelById(int otelId)
        {
            var otel = await _context.Oteller.FindAsync(otelId);

            if (otel == null)
            {
                return NotFound();
            }

            return otel;
        }

        // POST: api/otel
        [HttpPost]
        public async Task<ActionResult<Otel>> CreateOtel(Otel otel)
        {
            _context.Oteller.Add(otel);


            // Yeni rapor talebi oluştur
            var raporTalebi = new Rapor
            {
                Otel = otel,
            };

            // Rapor talebini RabbitMQ'ya gönder
            await _publishEndpoint.Publish(raporTalebi);

            // Rapor oluşturulduğunda raporun durumunu veritabanına kaydedin
            _context.Raporlar.Add(new Rapor
            {
                TalepEdildigiTarih = DateTime.Now,
                Otel = otel,
                OtelYetkilisi = null,
                Durum = "Oluşturuldu"
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOtelById), new { otelId = otel.Id }, otel);
        }

        // PUT: api/otel/5
        [HttpPut("{otelId}")]
        public async Task<IActionResult> UpdateOtel(int otelId, Otel otel)
        {
            if (otelId != otel.Id)
            {
                return BadRequest();
            }

            _context.Entry(otel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OtelExists(otelId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/otel/5
        [HttpDelete("{otelId}")]
        public async Task<IActionResult> DeleteOtel(int otelId)
        {
            var otel = await _context.Oteller.FindAsync(otelId);
            if (otel == null)
            {
                return NotFound();
            }

            _context.Oteller.Remove(otel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OtelExists(int otelId)
        {
            return _context.Oteller.Any(e => e.Id == otelId);
        }


        // GET: api/otel/yetkililer
        [HttpGet("yetkililer")]
        public async Task<ActionResult<IEnumerable<OtelYetkilisi>>> GetOtelYetkilileri()
        {
            // Veritabanından otel yetkililerini al ve listele
            //var yetkililer = await _context.OtelYetkilileri.ToListAsync();

            var yetkililer = new List<OtelYetkilisi>
            {
                new OtelYetkilisi { Id = 1, Ad = "Ali", Soyad = "Taşdeviren", FirmaUnvan = "Yönetici" },
                new OtelYetkilisi { Id = 2, Ad = "Ayşe", Soyad = "Kaplan", FirmaUnvan = "Recepsiyon" }
            };
            // Yeni rapor talebi oluştur

            foreach (var item in yetkililer)
            {
                var raporTalebi = new OtelYetkilisi
                {
                    Ad = item.Ad,
                    Soyad = item.Soyad,
                    FirmaUnvan = item.FirmaUnvan,
                    OtelId = item.OtelId,
                    Otel = item.Otel 
                };

                // Rapor talebini RabbitMQ'ya gönder
                await _publishEndpoint.Publish(raporTalebi);
            }

            return yetkililer;
        }

        // GET: api/otel/{otelId}/detay
        [HttpGet("{otelId}/detay")]
        public async Task<ActionResult<Otel>> GetOtelDetay(int otelId)
        {
            var otel = await _context.Oteller
                .Include(o => o.Yetkililer)
                .Include(o => o.IletisimBilgileri)
                .FirstOrDefaultAsync(o => o.Id == otelId);

            if (otel == null)
            {
                return NotFound();
            }
            return otel;
        }


        //// oluşturulan raporu RabbitMQ yaz
        //[HttpPost("talepet")]
        //public IActionResult TalepEt(Guid otelId)
        //{
        //    // Rapor talebi oluştur ve kaydet
        //    // ...

        //    // Rapor talebini RabbitMQ'ya gönder
        //    _raporTalepService.SendRaporTalebi(otelId);

        //    return Ok("Rapor talebi başarıyla gönderildi.");
        //}

    }
}
