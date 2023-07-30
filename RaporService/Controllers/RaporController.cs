using Data.Entity;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RaporService.Controllers
{
    public class RaporController : ControllerBase
    {
        private readonly OtelDbContext _context;
        private readonly IRequestClient<Rapor> _requestClient;

        public RaporController(OtelDbContext context,
                               IRequestClient<Rapor> requestClient)
        {
            _context = context;
            _requestClient = requestClient;
        }

        // GET: api/raporlar
        [HttpGet("raporlar")]
        public async Task<ActionResult<IEnumerable<Rapor>>> GetRaporlar()
        {
            var raporlar = await _context.Raporlar.ToListAsync();
            return raporlar;
        }

        // GET: api/raporlar/{raporId}
        [HttpGet("raporlar/{raporId}")]
        public async Task<ActionResult<Rapor>> GetRaporById(int raporId)
        {
            // Rapor durumu isteğini gönder ve sonucu al
            var response = await _requestClient.GetResponse<RaporDurumuModel>(new { RaporId = raporId });

            var raporDurumu = response.Message;

            return Ok(raporDurumu);
        }

        // POST: api/raporlar
        [HttpPost("raporlar")]
        public async Task<ActionResult<Rapor>> CreateRapor(Rapor rapor)
        {
            _context.Raporlar.Add(rapor);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRaporById), new { raporId = rapor.Id }, rapor);
        }
    }
}
