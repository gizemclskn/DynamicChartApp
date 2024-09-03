using DynamicChartApp.Models;
using DynamicChartApp.Repositories;
using DynamicChartApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DynamicChartApp.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class DatabaseController : ControllerBase
    {
        private readonly IDatabaseService _databaseService;

        public DatabaseController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }
        [HttpPost("getdata")]
        public async Task<IActionResult> GetData([FromBody] ChartDataRequest request)
        {
            try
            {
                // SQL sorgusunun çalıştırılması
                var dataTable = await _databaseService.ExecuteQueryAsync(request.ConnectionString, request.Query);

                // Yanıt verisinin hazırlanması
                var responseData = new ChartDataResponse
                {
                    Labels = new List<string>(),
                    DataSets = new List<List<object>>
            {
                new List<object>()
            }
                };

                // Sütun adlarının eklenmesi
                foreach (DataColumn column in dataTable.Columns)
                {
                    if (column.ColumnName != "Id")
                    {
                        responseData.Labels.Add(column.ColumnName);
                    }
                }

                // Satır verilerinin eklenmesi
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 1; i < dataTable.Columns.Count; i++)
                    {
                        responseData.DataSets[0].Add(row[i]);
                    }
                }

                // Yanıtın JSON olarak döndürülmesi
                return Ok(new { Type = request.ChartType, Data = responseData });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
                return StatusCode(500, "Veritabanına bağlanırken bir hata oluştu.");
            }
        }

    }
}
