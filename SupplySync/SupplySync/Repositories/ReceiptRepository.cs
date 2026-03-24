using Microsoft.EntityFrameworkCore;
using SupplySync.Config;
using SupplySync.Models;
using SupplySync.Repositories.Interfaces;

namespace SupplySync.Repositories
{
    public class ReceiptRepository : IReceiptRepository
    {
        private readonly AppDbContext _context;

        public ReceiptRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Receipt> InsertAsync(Receipt entity)
        {
            await _context.Receipts.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Receipt?> GetByIdAsync(int receiptId)
        {
            return await _context.Receipts
                .Where(x => !x.IsDeleted && x.ReceiptID == receiptId)
                .FirstOrDefaultAsync();
        }

        public async Task<Receipt> UpdateAsync(Receipt entity)
        {
            _context.Receipts.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> SoftDeleteAsync(int receiptId)
        {
            var receipt = await _context.Receipts.FindAsync(receiptId);
            if (receipt == null) return false;

            receipt.IsDeleted = true;
            receipt.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Receipt>> ListAsync(
            int? warehouseId,
            int? deliveryId,
            string? status,
            DateOnly? fromDate,
            DateOnly? toDate)
        {
            var query = _context.Receipts
                .Where(x => !x.IsDeleted)
                .AsQueryable();

            if (warehouseId.HasValue)
                query = query.Where(x => x.WarehouseID == warehouseId.Value);

            if (deliveryId.HasValue)
                query = query.Where(x => x.DeliveryID == deliveryId.Value);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(x => x.Status.ToString() == status);

            if (fromDate.HasValue)
                query = query.Where(x => x.Date >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(x => x.Date <= toDate.Value);

            return await query.OrderByDescending(x => x.Date).ToListAsync();
        }
    }
}
