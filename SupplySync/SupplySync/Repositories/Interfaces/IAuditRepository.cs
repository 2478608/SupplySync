using SupplySync.Models;

namespace SupplySync.Repositories.Interfaces
{
    public interface IAuditRepository
    {
        Task<Audit> InsertAsync(Audit audit);
    }
}
