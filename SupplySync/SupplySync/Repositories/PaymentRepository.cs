using SupplySync.Config;
using SupplySync.Repositories.Interfaces;
using SupplySync.Models;
using Microsoft.EntityFrameworkCore;

namespace SupplySync.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;
        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Payment> InsertAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return payment;
        }

        public async Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments.FirstOrDefaultAsync(x => x.PaymentId == id && !x.IsDeleted);
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Payments.Where(x => !x.IsDeleted).ToListAsync();
        }
    }
}
