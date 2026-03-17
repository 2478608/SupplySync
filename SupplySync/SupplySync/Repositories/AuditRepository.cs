using Microsoft.EntityFrameworkCore;
using SupplySync.Models;
using SupplySync.Repositories.Interfaces;
using SupplySync.Config;

namespace SupplySync.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly AppDbContext _context;


        public AuditRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<Audit> InsertAsync(Audit audit)
        {
            await _context.Audits.AddAsync(audit);
            await _context.SaveChangesAsync();
            return audit;
        }


    }
}
