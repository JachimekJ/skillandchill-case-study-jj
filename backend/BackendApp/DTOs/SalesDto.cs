namespace BackendApp.DTOs
{
    public class SalesDto
    {
        public int DistributorId { get; set; }
        public string Quarter { get; set; } = string.Empty;
        public string Currency { get; set; } = "PLN";

        public decimal Professional { get; set; }
        public decimal Pharmacy { get; set; }
        public decimal EcommerceB2C { get; set; }
        public decimal EcommerceB2B { get; set; }
        public decimal ThirdParty { get; set; }
        public decimal Other { get; set; }

        public int NewClients { get; set; }
    }
}
