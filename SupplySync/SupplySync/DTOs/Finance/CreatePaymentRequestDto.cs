namespace SupplySync.DTOs.Finance
{
    public class CreatePaymentRequestDto
    {
        public int InvoiceId { get; set; } 
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } 
        public string Method { get; set; } // NEFT , RTGS, IMPS, Cheque , UPI
    }
}
