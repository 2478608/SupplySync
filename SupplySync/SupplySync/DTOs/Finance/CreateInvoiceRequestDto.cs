namespace SupplySync.DTOs.Finance
{
    public class CreateInvoiceRequestDto
    {
        public int POID { get; set; }
        public int VendorId { get; set; }
        public decimal Amount { get; set; } 
        public DateTime Date { get; set; } 
    }
}
