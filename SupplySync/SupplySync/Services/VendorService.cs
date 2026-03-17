using Microsoft.AspNetCore.Mvc;
using SupplySync.DTOs;
using SupplySync.Models;
using SupplySync.Repositories.Interfaces;
using SupplySync.Services.Interfaces;

namespace SupplySync.Services
{
	public class VendorService : IVendorService
	{
		private readonly IVendorRepository _vendorRepository;

		public VendorService(IVendorRepository vendorRepository)
		{
			_vendorRepository = vendorRepository;
		}

		public async Task<VendorResponseDto> CreateVendor(CreateVendorRequestDto createVendorRequestDto)
		{
			Vendor newVendor = new Vendor();
			// after mapping we will get vendor
			var vendor =  _vendorRepository.CreateVendor(newVendor);
			// map with response dto

			return new VendorResponseDto();
		}

	}
}
