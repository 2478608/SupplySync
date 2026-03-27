using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupplySync.Config;
using SupplySync.DTOs.Vendor;
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

		public async Task<Vendor?> GetVendorById(int vendorId)
		{
			var vendor = await _appDbContext.Vendors.FirstOrDefaultAsync(x=> x.VendorID == vendorId);
			return vendor;
		}

		public async Task<List<Vendor>> GetAllVendorWithFilter(GetVendorFiltersRequestDto filters)
		{
			var query = _appDbContext.Vendors.AsQueryable();

			if (!string.IsNullOrWhiteSpace(filters.Name))
			{
				query = query.Where(x => x.Name.Contains(filters.Name));
			}

			if (filters.Category.HasValue)
			{
				query = query.Where(v => v.Category == filters.Category);
			}

			if (filters.Status.HasValue)
			{
				query = query.Where(v => v.Status == filters.Status);
			}

			query = query.Where(v => !v.IsDeleted);
			return await query
						.Skip((filters.Page - 1) * filters.PageSize)
						.Take(filters.PageSize)
						.ToListAsync();
		}

		public async Task<Vendor?> CreateVendor(Vendor vendor)
		{
			await _appDbContext.Vendors.AddAsync(vendor);
			await _appDbContext.SaveChangesAsync();
			return vendor;
		}
		public async Task<Vendor> UpdateVendor(Vendor vendor)
		{
			_appDbContext.Vendors.Update(vendor);
			await _appDbContext.SaveChangesAsync();
			return vendor;
		}

		public async Task<bool> DeleteVendorById(int vendorId)
		{
			Vendor? vendor = await GetVendorById(vendorId);
			if (vendor == null || vendor.IsDeleted == true)
			{
				throw new KeyNotFoundException($"Vendor with ID {vendorId} not found.");
			}

			vendor.IsDeleted = true;
			vendor.UpdatedAt = DateTime.UtcNow;
			await _appDbContext.SaveChangesAsync();
			return true;
		}

		public async Task<VendorDocument?> GetVendorDocumentById(int vendorDocumentId)
		{
			var vendorDocument = await _appDbContext.VendorDocuments.FirstOrDefaultAsync(x => x.DocumentID == vendorDocumentId);
			return vendorDocument;
		}

		public async Task<VendorDocument> CreateVendorDocument(VendorDocument newVendorDocument)
		{
			await _appDbContext.VendorDocuments.AddAsync(newVendorDocument);
			await _appDbContext.SaveChangesAsync();
			return newVendorDocument;
		}

		public async Task<List<VendorDocument>> GetAllVendorDocuments(int vendorId)
		{
			return await _appDbContext.VendorDocuments.Where(x => x.VendorID == vendorId && !x.IsDeleted).ToListAsync();

		}

		public async Task<bool> DeleteVendorDocument(int documentId)
		{
			VendorDocument? vendorDocument = await GetVendorDocumentById(documentId);
			if (vendorDocument == null || vendorDocument.IsDeleted == true)
			{
				throw new KeyNotFoundException($"Document with ID {documentId} not found.");
			}
			vendorDocument.IsDeleted = true;
			vendorDocument.UpdatedAt = DateTime.UtcNow;
			await _appDbContext.SaveChangesAsync();
			return true;
		}
	}

}
