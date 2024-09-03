using DynamicChartApp.Models;

namespace DynamicChartApp.Services
{
    public interface IChartService
    {
        Task<ChartData> GetChartDataAsync(string connectionString, string dataSetName, string chartType);
    }
}
