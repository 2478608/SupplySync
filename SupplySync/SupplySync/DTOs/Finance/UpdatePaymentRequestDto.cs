namespace SupplySync.DTOs.Finance
{
    public class UpdatePaymentRequestDto
    {
        public decimal Amount { get; set; }
        public string Status { get; set; } // Success, Failed, etc.
        public string Method { get; set; }
    }
}
