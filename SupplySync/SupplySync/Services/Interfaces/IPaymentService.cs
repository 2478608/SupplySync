using SupplySync.DTOs.Finance;

namespace SupplySync.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<int> CreatePaymentAsync(CreatePaymentRequestDto dto);
        Task UpdatePaymentAsync(int id , UpdatePaymentRequestDto dto);

        Task<PaymentResponseDto?> GetPaymentByIdAsync(int id);
        Task<IEnumerable<PaymentListResponseDto>> GetAllPaymentsAsync();
    }
}
