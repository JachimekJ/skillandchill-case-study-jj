namespace BackendApp.DTOs
{
    public class PurchaseReportDto
    {
        public int DistributorId { get; set; }
        public string Quarter { get; set; } = string.Empty;

        public decimal LastYearSales { get; set; }
        public decimal Purchases { get; set; }
        public decimal Budget { get; set; }

        public int TotalPOS { get; set; }
        public int NewOpenings { get; set; }
        public int OpeningsTarget { get; set; }
    }
}
