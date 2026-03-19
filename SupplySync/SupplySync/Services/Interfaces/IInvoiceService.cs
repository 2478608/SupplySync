using SupplySync.DTOs.Finance;
using SupplySync.Models;

namespace SupplySync.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<int> CreateInvoiceAsync(CreateInvoiceRequestDto dto);
        
        Task UpdateInvoiceAsync(int id, UpdateInvoiceRequestDto dto);
        Task<InvoiceResponseDto?> GetInvoiceByIdAsync(int id);
        Task<IEnumerable<InvoiceListResponseDto>> GetInvoiceListAsync();
    }
}
