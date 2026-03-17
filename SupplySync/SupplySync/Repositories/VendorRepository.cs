using Microsoft.AspNetCore.Mvc;
using SupplySync.Config;
using SupplySync.Models;
using SupplySync.Repositories.Interfaces;

namespace SupplySync.Repositories
{
	public class VendorRepository : IVendorRepository
	{
		private readonly AppDbContext _appDbContext;

		public VendorRepository(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public  async Task<Vendor?> CreateVendor(Vendor vendor)
		{
			await _appDbContext.Vendors.AddAsync(vendor);
			await _appDbContext.SaveChangesAsync();

			return vendor;
		}
	}
}
