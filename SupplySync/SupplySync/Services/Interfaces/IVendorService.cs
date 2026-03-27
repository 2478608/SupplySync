using Microsoft.AspNetCore.Mvc;
using SupplySync.DTOs.Vendor;

namespace SupplySync.Services.Interfaces
{
	public interface IVendorService
	{
		Task<VendorResponseDto> CreateVendor(CreateVendorRequestDto createVendorRequestDto);
		Task<VendorDocumentResponseDto> CreateVendorDocument(CreateVendorDocumentRequestDto createVendorDocumentRequestDto);
		Task<bool> DeleteVendorById(int vendorId);
		Task<bool> DeleteVendorDocument(int vendorId, int documentId);
		Task<List<VendorDocumentResponseDto>> GetAllVendorDocument(int vendorId);
		Task<List<VendorResponseDto>> GetAllVendorWithFilter(GetVendorFiltersRequestDto getVendorFiltersRequestDto);
		Task<VendorResponseDto> GetVendorById(int userId, int vendorId);
		Task<VendorResponseDto> UpdateVendor(int vendorId, UpdateVendorRequestDto updateVendorRequestDto);
	}
}
