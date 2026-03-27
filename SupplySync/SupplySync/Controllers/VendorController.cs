using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupplySync.DTOs.Vendor;
using SupplySync.Models;
using SupplySync.Services;
using SupplySync.Services.Interfaces;

namespace SupplySync.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class VendorController : ControllerBase
	{
		private readonly IVendorService _vendorService;
		public VendorController(IVendorService vendorService) 
		{
			_vendorService = vendorService;
		}
		
		/// <summary>
		///  Vendor Endpoints
		/// </summary>

		/// <summary>
		/// Retrieves a vendor by its unique vendor ID.
		/// </summary>

		[Authorize]
		[HttpGet("{vendorId}")]
		public async Task<IActionResult> GetVendorById([FromRoute] int vendorId)
		{
			var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
			VendorResponseDto vendorResponseDto = await _vendorService.GetVendorById(userId, vendorId);
			return Ok(vendorResponseDto);
		}

		/// <summary>
		/// Retrieves all vendors based on the provided filter criteria.
		/// Only users with specific roles are authorized to access this endpoint.
		/// </summary>
		[Authorize(Roles = "Admin,ProcurementOfficer,WarehouseManager,FinanceOfficer,ComplianceOfficer")]
		[HttpGet("")]
		public async Task<IActionResult> GetAllVendorWithFilter([FromQuery] GetVendorFiltersRequestDto getVendorFiltersRequestDto)
		{
			List<VendorResponseDto> vendorResponseDto = await _vendorService.GetAllVendorWithFilter(getVendorFiltersRequestDto);
			return Ok(vendorResponseDto);
		}



		/// <summary>
		/// Creates a new vendor in the system.
		/// </summary>
		[HttpPost("")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateVendor([FromBody] CreateVendorRequestDto createVendorRequestDto)
		{


			try
			{
				VendorResponseDto createdVendor = await _vendorService.CreateVendor(createVendorRequestDto);

				return Ok(createdVendor);
			}
			catch (DbUpdateException e)
			{
				throw new InvalidOperationException("Database Error, May Data Already Available.");
			}
}

		/// <summary>
		/// Updates vendor information for the specified vendor ID.
		/// </summary>
		[Authorize(Roles = "Admin,VendorUser")]
		[HttpPut("{vendorId}")]
		public async Task<IActionResult> UpdateVendor([FromRoute] int vendorId, [FromBody] UpdateVendorRequestDto updateVendorRequestDto)
		{
			VendorResponseDto? vendorResponseDto = await _vendorService.UpdateVendor(vendorId, updateVendorRequestDto);

			return Ok(vendorResponseDto);
		}

		/// <summary>
		/// Deletes the vendor associated with the specified vendor ID.
		/// </summary>
		[Authorize(Roles = "Admin")]
		[HttpDelete("{vendorId}")]
		public async Task<IActionResult> DeleteVendorById([FromRoute] int vendorId) {
			bool isDeleted = await _vendorService.DeleteVendorById(vendorId);
			return Ok(isDeleted);
		}

		/// <summary>
		///  Vendor Document Endpoints
		/// </summary>

		/// <summary>
		/// Retrieves all documents associated with the specified vendor.
		/// </summary>
		[Authorize]
		[HttpGet("{vendorId}/documents")]
		public async Task<IActionResult> GetAllVendorDocument([FromRoute] int vendorId)
		{
			List<VendorDocumentResponseDto> vendorDocumentResponseDtos = await _vendorService.GetAllVendorDocument(vendorId);
			return Ok(vendorDocumentResponseDtos);
		}

		/// <summary>
		/// Creates and uploads a new document for the specified vendor.
		/// A document file is required.
		/// </summary>
		[Authorize(Roles = "Admin,VendorUser")]
		[HttpPost("{vendorId}/documents")]
		public async Task<IActionResult> CreateVendorDocument([FromForm] CreateVendorDocumentRequestDto createVendorDocumentRequestDto)
		{
			if(createVendorDocumentRequestDto.DocFile == null)
			{
				throw new ArgumentException("File is required.");
			}
			VendorDocumentResponseDto createdVendorDocument = await _vendorService.CreateVendorDocument(createVendorDocumentRequestDto);
			return Ok(createdVendorDocument);
		}

		/// <summary>
		/// Deletes a specific vendor document using the vendor ID and document ID.
		/// </summary>
		[Authorize(Roles = "Admin,VendorUser")]
		[HttpDelete("{vendorId}/documents/{documentId}")]
		public async Task<IActionResult> DeleteVendorDocument([FromRoute] int vendorId, [FromRoute] int documentId)
		{
			bool isDeleted = await _vendorService.DeleteVendorDocument(vendorId, documentId);
			return Ok(isDeleted);
		}
	}
}
