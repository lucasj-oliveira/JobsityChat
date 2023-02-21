using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMqConsole.Domain;
using RestSharp;
using StockApi.Domain;
using System.Text;
using System.Text.Json;

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
    StockResponse stockResponse = JsonSerializer.Deserialize<StockResponse>(message);

    if(stockResponse != null)
    {
        var msgDto = new MessageDto()
        {
            user = "BOT",
            msgText = $"{stockResponse.stockCode.ToUpper()} Quote is {stockResponse.stockPrice} per share." 
        };
        var client = new RestClient($"https://localhost:7213/api/Chat/send");
        RestRequest request = new RestRequest()
        {
            Method = Method.Post
        };
        request.AddParameter("application/json", msgDto, ParameterType.RequestBody);
        RestResponse response = await client.ExecuteAsync(request);
    }
};
channel.BasicConsume(queue: "getStockPriceQueue", autoAck: true, consumer: consumer);
Console.ReadKey();