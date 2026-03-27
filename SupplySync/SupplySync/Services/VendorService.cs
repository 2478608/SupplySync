using System.Numerics;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SupplySync.Constants.Enums;
using SupplySync.DTOs.Vendor;
using SupplySync.Models;
using SupplySync.Repositories.Interfaces;
using SupplySync.Services.Interfaces;

namespace SupplySync.Services
{
	public class VendorService : IVendorService
	{
		private readonly IUserRepository _userRepository;
		private readonly IVendorRepository _vendorRepository;
		private readonly IAuditLogService _auditLogService;
		private readonly IMapper _mapper;

		public VendorService(IVendorRepository vendorRepository, 
							IMapper mapper, 
							IUserRepository userRepository,
							IAuditLogService auditLogService)
		{
			_vendorRepository = vendorRepository;
			_mapper = mapper;
			_userRepository = userRepository;
			_auditLogService = auditLogService;
		}

		/// <summary>
		///  Vendor Endpoints
		/// </summary>


		public async Task<VendorResponseDto> GetVendorById(int userId, int vendorId)
		{
			var vendor = await _vendorRepository.GetVendorById(vendorId);

			if (vendor == null) {
				throw new KeyNotFoundException("Vendor Not Found");
			}

			await _auditLogService.WriteAsync(
				userId: userId,               // actor (or null if system)
				userName:"UnKnown",
				action: $"Get Vendor with id {vendorId}",
				resource: $"VendorId :{vendorId}"
			);

			return _mapper.Map<VendorResponseDto>(vendor);
		}

		public async Task<List<VendorResponseDto>> GetAllVendorWithFilter(GetVendorFiltersRequestDto getVendorFiltersRequestDto)
		{
			List<Vendor> vendors = await _vendorRepository.GetAllVendorWithFilter(getVendorFiltersRequestDto);
			if (vendors.Count <= 0)
			{
				throw new KeyNotFoundException("No Vendors Available");
			}
			List<VendorResponseDto> vendorResponseDtos = _mapper.Map<List<VendorResponseDto>>(vendors);
			return vendorResponseDtos;
		}

		public async Task<VendorResponseDto> CreateVendor(CreateVendorRequestDto createVendorRequestDto)
		{
			if(createVendorRequestDto?.UserID == null){
				throw new KeyNotFoundException("UserId Should Available");
			}

			User? user = await _userRepository.GetByIdWithRolesAsync(createVendorRequestDto.UserID);

			if(user == null || user.IsDeleted){
				throw new KeyNotFoundException("User Not available");
			}

			if (user.UserRoles.Any(r => r.Role.RoleType != RoleType.VendorUser))
			{
				throw new InvalidOperationException("User must have Vendor role");
			}

			Vendor newVendor = _mapper.Map<Vendor>(createVendorRequestDto);
			// after mapping we will get vendor

			Vendor? vendor =  await _vendorRepository.CreateVendor(newVendor);
			if (vendor == null) {
				throw new ArgumentException("Vendor Not Created, some error occured");
			}
			// map with response dto
			VendorResponseDto vendorResponseDto = _mapper.Map<VendorResponseDto>(vendor);

			return vendorResponseDto;
		}

		public async Task<VendorResponseDto?> UpdateVendor(int vendorId, UpdateVendorRequestDto updateVendorRequestDto)
		{
			Vendor? existingVendor = await _vendorRepository.GetVendorById(vendorId);
			if (existingVendor == null || existingVendor.IsDeleted==true)
			{
				throw new KeyNotFoundException($"Vendor with ID {vendorId} not found.");
			}
			
			_mapper.Map(updateVendorRequestDto, existingVendor);

			existingVendor.UpdatedAt = DateTime.UtcNow;

			Vendor? updatedVendor = await _vendorRepository.UpdateVendor(existingVendor);

			if (updatedVendor == null)
			{
				throw new ArgumentException("Vendor Not Updated, some error occured");
			}

			VendorResponseDto vendorResponseDto = _mapper.Map<VendorResponseDto>(updatedVendor);

			return vendorResponseDto;
		}

		public async Task<bool> DeleteVendorById(int vendorId)
		{
			bool IsDeleted = await _vendorRepository.DeleteVendorById(vendorId);
			return IsDeleted;
		}



		/// <summary>
		///  Vendor Document Endpoints
		/// </summary>

		public async Task<VendorDocumentResponseDto> CreateVendorDocument(CreateVendorDocumentRequestDto createVendorDocumentRequestDto)
		{
			VendorDocument newVendorDocument = _mapper.Map<VendorDocument>(createVendorDocumentRequestDto);

			var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "vendor-documents");


			if (!Directory.Exists(uploadFolder))
				Directory.CreateDirectory(uploadFolder);


			var originalName = createVendorDocumentRequestDto.DocFile.FileName.Replace(" ", "_");
			var fileName = $"{Guid.NewGuid()}_{originalName}";
			var filePath = Path.Combine(uploadFolder, fileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await createVendorDocumentRequestDto.DocFile.CopyToAsync(stream);
			}


			newVendorDocument.FileURI = $"/uploads/vendor-documents/{fileName}";

			var vendorDocument = await _vendorRepository.CreateVendorDocument(newVendorDocument);
			if(vendorDocument == null)
			{
				throw new ArgumentException("Document Not Created, some error occured");
			}
			VendorDocumentResponseDto vendorDocumentResponseDto = _mapper.Map<VendorDocumentResponseDto>(vendorDocument);

			return vendorDocumentResponseDto;
		}

		public async Task<List<VendorDocumentResponseDto>> GetAllVendorDocument(int vendorId)
		{

			Vendor? existingVendor = await _vendorRepository.GetVendorById(vendorId);
			if (existingVendor == null || existingVendor.IsDeleted == true)
			{
				throw new KeyNotFoundException($"Vendor with ID {vendorId} not found.");
			}

			List<VendorDocument> documents = await _vendorRepository.GetAllVendorDocuments(vendorId);
			if (documents.Count <= 0)
			{
				throw new KeyNotFoundException("No Documents Available");
			}
			List<VendorDocumentResponseDto> vendorDocumentResponseDtos = _mapper.Map<List<VendorDocumentResponseDto>>(documents);
			return vendorDocumentResponseDtos;
		}

		public async Task<bool> DeleteVendorDocument(int vendorId, int documentId)
		{
			Vendor? vendor = await _vendorRepository.GetVendorById(vendorId);
			if (vendor == null || vendor.IsDeleted)
			{
				throw new KeyNotFoundException($"Vendor with ID {vendorId} not found.");
			}

			bool isDeleted = await _vendorRepository.DeleteVendorDocument(documentId);

			return isDeleted;
		}
	}
}
