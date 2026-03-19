using AutoMapper;
using SupplySync.Constants.Enums;
using SupplySync.DTOs.Finance;
using SupplySync.Models;
using SupplySync.Repositories.Interfaces;
using SupplySync.Services.Interfaces;

namespace SupplySync.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<int> CreatePaymentAsync(CreatePaymentRequestDto dto)
        {
            if (!Enum.TryParse<PaymentMethod>(dto.Method, true, out var method))
                throw new ArgumentException($"'{dto.Method}' is not a valid payment method. " +
                                            $"Allowed: NEFT, RTGS, IMPS, Cheque, UPI");
            var payment = _mapper.Map<Payment>(dto);
            payment.Method = method;
            var createdPayment = await _paymentRepository.InsertAsync(payment);
            return createdPayment.PaymentId;
        }

        public async Task UpdatePaymentAsync(int id, UpdatePaymentRequestDto dto)
        {
            var existing = await _paymentRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Payment with ID {id} not found.");

            if (!Enum.TryParse<PaymentStatus>(dto.Status, true, out var status))
                throw new ArgumentException($"'{dto.Status}' is not a valid payment status. " +
                                            $"Allowed: Initiated, Success, Failed, Reversed");

            if (!Enum.TryParse<PaymentMethod>(dto.Method, true, out var method))
                throw new ArgumentException($"'{dto.Method}' is not a valid payment method. " +
                                            $"Allowed: NEFT, RTGS, IMPS, Cheque, UPI");

            _mapper.Map(dto, existing);
            existing.Status = status;
            existing.Method = method;
            await _paymentRepository.UpdateAsync(existing);
        }

        public async Task<PaymentResponseDto?> GetPaymentByIdAsync(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            return payment == null ? null : _mapper.Map<PaymentResponseDto>(payment);
        }

        public async Task<IEnumerable<PaymentListResponseDto>> GetAllPaymentsAsync()
        {
            var payments = await _paymentRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PaymentListResponseDto>>(payments);

        }
    }
}
