namespace BackendApp.Models
{
    public class SalesReports
    {
        public int Id { get; set; }

        public int DistributorId { get; set; }   // tymczasowo int, później FK do Distributors
        public string Quarter { get; set; } = string.Empty;
        public string Currency { get; set; } = "PLN";

        public decimal Professional { get; set; }
        public decimal Pharmacy { get; set; }
        public decimal EcommerceB2C { get; set; }
        public decimal EcommerceB2B { get; set; }
        public decimal ThirdParty { get; set; }
        public decimal Other { get; set; }

        public int NewClients { get; set; }

        // Pola wyliczane
        public decimal Total { get; set; }
        public decimal EurTotal { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
