using Microsoft.AspNetCore.Mvc;
using RestSharp;
using StockApi.Services;

namespace StockApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BotController : ControllerBase
    {
        private IBotService _botService;
        public BotController(IBotService botService)
        {
            _botService = botService;
        }

        [HttpGet (Name = "GetStock")]
        public async Task GetAsync(string stockCode)
        {
            await _botService.GetStockPrice(stockCode); 
        }
    }
}