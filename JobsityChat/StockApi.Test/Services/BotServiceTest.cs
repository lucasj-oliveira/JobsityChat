
using Moq;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using StockApi.Services;
using Xunit;
using System.Text;

namespace JobsityChat.Test.Services
{
    public class BotServiceTest
    {
        private readonly BotService botService;
        public BotServiceTest()
        {
            botService = new BotService(new Mock<StockApi.RabbitMQ.IRabbitMQProducer>().Object);
        }


        [Fact]
        public async Task GetStockPrice()
        {
            //would work in a dev environment
            botService.GetStockPrice("aapl.us");
            Thread.Sleep(1000);
            bool isConsumed = false;
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };
            var connection = factory.CreateConnection(); using
            var channel = connection.CreateModel();
            channel.QueueDeclare("getStockPriceQueue", exclusive: false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, eventArgs) => {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                if(message != null) isConsumed = true;
            };
            channel.BasicConsume(queue: "getStockPriceQueue", autoAck: true, consumer: consumer);


            Assert.True(isConsumed);
        }
    }
}
