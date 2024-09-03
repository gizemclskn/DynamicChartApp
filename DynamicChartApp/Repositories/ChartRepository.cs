using DynamicChartApp.Models;
using Microsoft.Data.SqlClient;

namespace DynamicChartApp.Repositories
{
    public class ChartRepository : IChartRepository
    {
        public async Task<ChartData> GetChartData(string connectionString, string dataSetName, string chartType)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                var command = new SqlCommand($"SELECT * FROM {dataSetName}", connection);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    var labels = new List<string>();
                    var values = new List<decimal>();

                    while (await reader.ReadAsync())
                    {
                        labels.Add(reader[0].ToString());
                        values.Add(Convert.ToDecimal(reader[1]));
                    }

                    return new ChartData
                    {
                        Labels = labels,
                        Values = values
                    };
                }
            }
        }
    }
}
