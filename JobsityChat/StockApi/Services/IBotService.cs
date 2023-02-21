namespace StockApi.Services
{
    public interface IBotService
    {
        Task GetStockPrice(string stockCode);
    }
}
