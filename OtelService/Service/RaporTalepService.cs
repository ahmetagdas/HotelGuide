using RabbitMQ.Client;
using System.Text;

namespace OtelService.Service
{
    public class RaporTalepService
    {
        private readonly RabbitMQSettings _rabbitMQSettings;

        public RaporTalepService(RabbitMQSettings rabbitMQSettings)
        {
            _rabbitMQSettings = rabbitMQSettings;
        }

        public void SendRaporTalebi(Guid otelId)
        {
            var factory = new ConnectionFactory() { HostName = _rabbitMQSettings.HostName };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _rabbitMQSettings.QueueName,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = otelId.ToString();
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: _rabbitMQSettings.QueueName,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
