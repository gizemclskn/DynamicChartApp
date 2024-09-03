using Microsoft.Data.SqlClient;
using System.Data;

namespace DynamicChartApp.Services
{
    public class DatabaseService : IDatabaseService
    {
        private readonly IConfiguration _configuration;
        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<DataTable> ExecuteQueryAsync(string connectionString, string query)
        {
            try
            {
                connectionString = connectionString.Trim();
                using (var connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand(query, connection))
                    {
                        using (var adapter = new SqlDataAdapter(command))
                        {
                            var dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            return dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
                throw;
            }
        }
    }
}

