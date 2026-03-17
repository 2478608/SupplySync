using SupplySync.Config;
using SupplySync.Models;
using SupplySync.Repositories.Interfaces;

namespace SupplySync.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext _context; 
        public InvoiceRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Invoice> InsertAsync(Invoice invoice)
        {
            await _context.Invoices.AddAsync(invoice);
            await _context.SaveChangesAsync();
            return invoice;
        }
    }
}
