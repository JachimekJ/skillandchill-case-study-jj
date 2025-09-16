namespace BackendApp.Models
{
    public class PurchaseReports
    {
        public int Id { get; set; }
        public int DistributorId { get; set; }
        public string Quarter { get; set; } = string.Empty;

        // dane finansowe (wejściowe)
        public decimal LastYearSales { get; set; }
        public decimal Purchases { get; set; }
        public decimal Budget { get; set; }

        // dane automatyczne / wyliczane
        public decimal ActualSales { get; set; }      // z SalesReports.Total
        public decimal YearVsLastYear { get; set; }   // ActualSales - LastYearSales
        public decimal YearVsBudget { get; set; }     // ActualSales - Budget

        // POS
        public int TotalPOS { get; set; }
        public int NewOpenings { get; set; }
        public int OpeningsTarget { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
