using DynamicChartApp.Models;
using DynamicChartApp.Repositories;

namespace DynamicChartApp.Services
{
    public class ChartService : IChartService
    {
        private readonly IChartRepository _chartRepository;

        public ChartService(IChartRepository chartRepository)
        {
            _chartRepository = chartRepository;
        }

        public async Task<ChartData> GetChartDataAsync(string connectionString, string dataSetName, string chartType)
        {
            return await _chartRepository.GetChartData(connectionString, dataSetName, chartType);
        }
    }
}
