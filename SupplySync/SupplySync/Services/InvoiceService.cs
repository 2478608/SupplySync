using AutoMapper;
using SupplySync.Constants.Enums;
using SupplySync.DTOs.Finance;
using SupplySync.Models;
using SupplySync.Repositories.Interfaces;
using SupplySync.Services.Interfaces;

namespace SupplySync.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        public InvoiceService(IInvoiceRepository invoiceRepository, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mapper = mapper;
        }
        public async Task<int> CreateInvoiceAsync(CreateInvoiceRequestDto dto)
        {
            var invoice = _mapper.Map<Invoice>(dto);
            var created = await _invoiceRepository.InsertAsync(invoice);
            return created.InvoiceId;
        }

        public async Task UpdateInvoiceAsync(int id, UpdateInvoiceRequestDto dto)
        {
            var existing = await _invoiceRepository.GetByIdAsync(id);
            if (existing == null)
                throw new KeyNotFoundException($"Invoice with ID {id} not found.");

            if (!Enum.TryParse<InvoiceStatus>(dto.Status, true, out var validatedStatus))
            {
                // This is the "Better Logic" - we stop the process and complain
                throw new ArgumentException($"'{dto.Status}' is not a valid invoice status. " +
                                            $"Allowed: Submitted, UnderReview, Approved, Rejected, Paid.");
            }
            _mapper.Map(dto, existing);
            existing.Status = validatedStatus;
            await _invoiceRepository.UpdateAsync(existing);
        }

        public async Task<InvoiceResponseDto?> GetInvoiceByIdAsync(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            return invoice == null ? null : _mapper.Map<InvoiceResponseDto>(invoice);
        }

        public async Task<IEnumerable<InvoiceListResponseDto>> GetInvoiceListAsync()
        {
            var invoices = await _invoiceRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<InvoiceListResponseDto>>(invoices);
        }
    }
}
