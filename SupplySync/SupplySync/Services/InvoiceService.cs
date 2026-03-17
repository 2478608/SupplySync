using AutoMapper;
using SupplySync.DTOs.Finance;
using SupplySync.Repositories.Interfaces;
using SupplySync.Services.Interfaces;
using SupplySync.Models;

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
    }
}
