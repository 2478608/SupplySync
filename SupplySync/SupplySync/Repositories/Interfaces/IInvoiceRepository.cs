using SupplySync.Models;

namespace SupplySync.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<Invoice> InsertAsync(Invoice invoice);
    }
}
