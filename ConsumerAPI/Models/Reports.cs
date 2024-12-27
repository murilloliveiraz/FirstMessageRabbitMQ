namespace ConsumerAPI.Models
{
    public static class Reports
    {
        public static List<RequestReport> ReportsList = new();
    }

    public class RequestReport
    {
        public Guid Id{ get; set; }
        public string Name{ get; set; }
        public string Status{ get; set; }
        public DateTime? ProcessedTime{ get; set; }
    }
}
