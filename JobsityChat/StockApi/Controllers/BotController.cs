using Microsoft.AspNetCore.Mvc;
using RestSharp;

namespace StockApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BotController : ControllerBase
    {
        private readonly ILogger<BotController> _logger;

        public BotController(ILogger<BotController> logger)
        {
            _logger = logger;
        }

        [HttpGet (Name = "GetStock")]
        public async Task<string> GetAsync(string stockCode)
        {
            var client = new RestClient($"https://stooq.com/q/l/?s={stockCode.ToLower()}&f=sd2t2ohlcv&h&e=csv");
            RestRequest request = new RestRequest()
            {
                Method = Method.Post            
            };
            RestResponse response = await client.ExecuteAsync(request);
            string[] splitedResponse = response.Content.Split(',');
            return splitedResponse[13];
        }
    }
}