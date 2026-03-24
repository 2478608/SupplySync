using Microsoft.EntityFrameworkCore;
using SupplySync.Config;
using SupplySync.Models;
using SupplySync.Repositories.Interfaces;

namespace SupplySync.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly AppDbContext _context;

        public WarehouseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Warehouse> InsertAsync(Warehouse entity)
        {
            await _context.Warehouses.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Warehouse?> GetByIdAsync(int warehouseId)
        {
            return await _context.Warehouses
                .Where(x => !x.IsDeleted && x.WarehouseID == warehouseId)
                .FirstOrDefaultAsync();
        }

        public async Task<Warehouse> UpdateAsync(Warehouse entity)
        {
            _context.Warehouses.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> SoftDeleteAsync(int warehouseId)
        {
            var warehouse = await _context.Warehouses.FindAsync(warehouseId);
            if (warehouse == null) return false;

            warehouse.IsDeleted = true;
            warehouse.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Warehouse>> ListAsync(
            string? location,
            string? status,
            int? capacity)
        {
            var query = _context.Warehouses
                .Where(x => !x.IsDeleted)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(location))
                query = query.Where(x => x.Location.Contains(location));

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(x => x.Status.ToString() == status);

            if (capacity.HasValue)
                query = query.Where(x => x.Capacity == capacity.Value);

            return await query.OrderBy(x => x.Location).ToListAsync();
        }
    }
}
