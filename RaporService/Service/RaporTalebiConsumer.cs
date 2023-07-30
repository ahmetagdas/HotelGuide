using Data.Entity;
using MassTransit;

namespace RaporService.Service
{
    public class RaporTalebiConsumer : IConsumer<Rapor>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public RaporTalebiConsumer(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public Task Consume(ConsumeContext<Rapor> context)
        {
            var raporTalebi = context.Message;

            // Rapor oluşturma işlemleri...
            // Burada rapor oluşturulma süreci gerçekleştirilebilir.

            // Rapor oluşturulduğunda rapor durumunu yayınla
            var raporDurumu = new RaporDurumuModel
            {
                Id = raporTalebi.Id,
                RaporId = raporTalebi.Id,
                Durum = RaporDurumu.Tamamlandi,
            };

            _publishEndpoint.Publish(raporDurumu);

            return Task.CompletedTask;
        }
    }
}
