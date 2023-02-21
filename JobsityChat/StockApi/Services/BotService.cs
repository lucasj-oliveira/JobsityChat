using RestSharp;
using StockApi.Domain;
using StockApi.RabbitMQ;

namespace StockApi.Services
{
    public class BotService : IBotService
    {
        IRabbitMQProducer _rabbitMQProducer;

        public BotService()
        {
        }

        public BotService(IRabbitMQProducer rabbitMQProducer) 
        {
            _rabbitMQProducer = rabbitMQProducer;
        }

        public async Task GetStockPrice(string stockCode)
        {
            var client = new RestClient($"https://stooq.com/q/l/?s={stockCode.ToLower()}&f=sd2t2ohlcv&h&e=csv");
            RestRequest request = new RestRequest()
            {
                Method = Method.Post
            };
            RestResponse response = await client.ExecuteAsync(request);
            string[] splitedResponse = response.Content.Split(',');
            var stockResponse = new StockResponse()
            {
                stockCode = stockCode,
                stockPrice = splitedResponse[13]
            };
            _rabbitMQProducer.SendMessage<StockResponse>(stockResponse);

        }
    }
}
