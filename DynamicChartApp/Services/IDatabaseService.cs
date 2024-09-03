using System.Data;

namespace DynamicChartApp.Services
{
    public interface IDatabaseService
    {
              Task<DataTable> ExecuteQueryAsync(string connectionString, string query);
    }
}
