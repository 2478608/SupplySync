using Microsoft.AspNetCore.Mvc;
using SupplySync.DTOs;
using SupplySync.Models;

namespace SupplySync.Repositories.Interfaces
{
	public interface IVendorRepository
	{
		Task<Vendor> CreateVendor(Vendor newVendor);
	}
}
