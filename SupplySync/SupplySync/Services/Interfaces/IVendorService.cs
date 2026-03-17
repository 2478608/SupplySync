using Microsoft.AspNetCore.Mvc;
using SupplySync.DTOs;

namespace SupplySync.Services.Interfaces
{
	public interface IVendorService
	{
		Task<VendorResponseDto> CreateVendor(CreateVendorRequestDto createVendorRequestDto);
	}
}
