using SupplySync.DTOs.Finance;
using SupplySync.Models;

namespace SupplySync.Services.Interfaces
{
    public interface IInvoiceService
    {
        Task<int> CreateInvoiceAsync(CreateInvoiceRequestDto dto);
    }
}
