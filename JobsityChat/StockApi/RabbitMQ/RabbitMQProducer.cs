
using RabbitMQ.Client;
using StockApi.Domain;
using System.Text;
using System.Text.Json;

namespace StockApi.RabbitMQ
{
    public class RabbitMQProducer : IRabbitMQProducer
    {
        public void SendMessage<T>(StockResponse message)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            var connection = factory.CreateConnection();
            using
            var channel = connection.CreateModel();
            channel.QueueDeclare("getStockPriceQueue", exclusive: false);
            var json = JsonSerializer.Serialize<StockResponse>(message);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(exchange: "", routingKey: "getStockPriceQueue", body: body);
        }
    }
}