namespace DynamicChartApp.Models
{
    public class ChartDataRequest
    {
        public string ConnectionString { get; set; }
        public string Query { get; set; }
        public string ChartType { get; set; }
    }
}
