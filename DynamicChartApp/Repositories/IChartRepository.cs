using DynamicChartApp.Models;

namespace DynamicChartApp.Repositories
{
    public interface IChartRepository
    {
        Task<ChartData> GetChartData(string connectionString, string dataSetName, string chartType);
    }
}
