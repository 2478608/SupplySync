namespace SupplySync.DTOs.Finance
{
    public class UpdateInvoiceRequestDto
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
