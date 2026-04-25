using SupplySync.Constants.Enums;

namespace SupplySync.DTOs.Finance
{

    public class CreateInvoiceRequestDto
    {
        public int POID { get; set; }
        public int VendorId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public InvoiceStatus Status { get; set; }
    }
    public class CreatePaymentRequestDto
    {
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        //public string Method { get; set; } // NEFT , RTGS, IMPS, Cheque , UPI
    }
    public class InvoiceListResponseDto
    {
        public int InvoiceId { get; set; }
        public int POID { get; set; }
        public string VendorName { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public DateOnly Date { get; set; }
    }
    public class InvoiceResponseDto
    {
        public int InvoiceId { get; set; }
        public int POID { get; set; }
        public int VendorId { get; set; }
        public string VendorName { get; set; }
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    public class PaymentListResponseDto
    {
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public string Status { get; set; }
    }
    public class PaymentResponseDto
    {
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public DateOnly Date { get; set; }
        public string Method { get; set; }
        public string Status { get; set; }
    }
    public class UpdateInvoiceRequestDto
    {
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
    public class UpdatePaymentRequestDto
    {
        public decimal Amount { get; set; }
        public string Status { get; set; } // Success, Failed, etc.
        public string Method { get; set; }
    }
}
