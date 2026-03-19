namespace SupplySync.DTOs.Finance
{
    public class PaymentResponseDto
    {
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; } 
        public string Method { get; set; }
        public string Status { get; set; }
    }
}
