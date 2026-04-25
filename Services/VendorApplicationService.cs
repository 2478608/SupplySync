using AutoMapper;
using Microsoft.AspNetCore.Http;
using SupplySync.Constants.Enums;
using SupplySync.DTOs.Notification;
using SupplySync.DTOs.Vendor;
using SupplySync.Models;
using SupplySync.Repositories;
using SupplySync.Repositories.Interfaces;
using SupplySync.Security;
using SupplySync.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SupplySync.Services
{
    public class VendorApplicationService : IVendorApplicationService
    {
        private readonly INotificationDispatcher _notificationDispatcher;
        private readonly IVendorApplicationRepository _applicationRepo;
        private readonly IVendorRepository _vendorRepo;
        private readonly INotificationService _notificationService;
        private readonly IAuditLogService _auditLogService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public VendorApplicationService(
            INotificationDispatcher notificationDispatcher,
            IVendorApplicationRepository applicationRepo,
            IVendorRepository vendorRepo,
            INotificationService notificationService,
            IAuditLogService auditLogService,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _notificationDispatcher = notificationDispatcher;
            _applicationRepo = applicationRepo;
            _vendorRepo = vendorRepo;
            _notificationService = notificationService;
            _auditLogService = auditLogService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        // ✅ CREATE APPLICATION
        public async Task<VendorApplicationResponseDto> CreateApplicationAsync(
            CreateVendorApplicationRequestDto dto)
        {
            var application = new VendorApplication
            {
                Name = dto.Name,
                ContactInfo = dto.ContactInfo,
                Category = dto.Category,
                Status = VendorStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
                Documents = dto.Documents?.Select(d => new VendorApplicationDocument
                {
                    DocType = d.DocType,
                    FileURI = d.FileURI,
                    UploadedDate = DateTime.UtcNow,
                    VerificationStatus = VendorDocumentVerificationStatus.Pending
                }).ToList()
            };

            var createdApplication = await _applicationRepo.CreateAsync(application);

            await _notificationService.SendAsync(
                new DTOs.Notification.CreateBulkNotificationRequestDto
                {
                    Message = $"New vendor application received: {createdApplication.Name}",
                    Category = NotificationCategory.System,
                    RoleTypes = new()
                    {
                        RoleType.ProcurementOfficer
                    }
                });

            await _auditLogService.WriteAsync(
                null,
                null,
                "VendorApplication.Submitted",
                $"Application:{createdApplication.ApplicationID}");  
            
             await _notificationDispatcher.SendNotificationAsync(
                SupplySync.Constants.Enums.NotificationEvent.VendorApplicationSubmitted,
                new Dictionary<string, object> { ["VendorName"] = createdApplication.Name }
            );

            return _mapper.Map<VendorApplicationResponseDto>(createdApplication);
        }

        // ✅ LIST PENDING APPLICATIONS
        public async Task<List<VendorApplicationResponseDto>> ListPendingAsync()
        {
            var applications = await _applicationRepo
                .ListByStatusAsync(VendorStatus.Pending);

            return _mapper.Map<List<VendorApplicationResponseDto>>(applications);
        }

        // ✅ GET APPLICATION BY ID
        public async Task<VendorApplicationResponseDto?> GetByIdAsync(int applicationId)
        {
            var application = await _applicationRepo.GetByIdAsync(applicationId);
            if (application == null) return null;

            return _mapper.Map<VendorApplicationResponseDto>(application);
        }

        // ✅ APPROVE APPLICATION
        public async Task<VendorResponseDto?> ApproveApplicationAsync(int id)
        {
            var app = await _applicationRepo.GetByIdAsync(id);

            // ✅ Guard clause – prevents double approval
            if (app == null || app.Status != VendorStatus.Pending)
                return null;

            // 1️⃣ Create Vendor from Application
            var vendor = new Vendor
            {
                Name = app.Name,
                ContactInfo = app.ContactInfo,
                Category = app.Category,
                Status = VendorStatus.Approved,
                UserId= app.UserId,
                CreatedAt = DateTime.UtcNow
            };

            await _vendorRepo.CreateVendor(vendor);

            // 2️⃣ Copy Documents
            if (app.Documents != null)
            {
                foreach (var doc in app.Documents)
                {
                    await _vendorRepo.CreateVendorDocument(new VendorDocument
                    {
                        VendorID = vendor.VendorID,
                        DocType = doc.DocType,
                        FileURI = doc.FileURI,
                        UploadedDate = DateTime.UtcNow,
                        VerificationStatus = VendorDocumentVerificationStatus.Verified
                    });
                }
            }

            // 3️⃣ Update Application Status
            app.Status = VendorStatus.Approved;
            await _applicationRepo.UpdateAsync(app);

            // 4️⃣ Send Notification (SAFE)
            try
            {
                await _notificationService.SendAsync(new CreateBulkNotificationRequestDto
                {
                    Message = "Your vendor application has been approved.",
                    Category = NotificationCategory.System,
                    RoleTypes = new List<RoleType> { RoleType.VendorUser }
                });
            }
            catch (Exception ex)
            {
                // ✅ Log error, but DO NOT fail approval
                // _logger.LogError(ex, "Notification failed for vendor approval");
            }

            return _mapper.Map<VendorResponseDto>(vendor);
        }

        // ✅ REJECT APPLICATION
        public async Task<bool> RejectApplicationAsync(int applicationId, string reason)
        {
            var application = await _applicationRepo.GetByIdAsync(applicationId);
            if (application == null) return false;

            application.Status = VendorStatus.Blacklisted;
            application.UpdatedAt = DateTime.UtcNow;

            await _applicationRepo.UpdateAsync(application);

            var user = _httpContextAccessor.HttpContext?.User;
            await _auditLogService.WriteAsync(
                user?.GetUserId(),
                user?.Identity?.Name,
                "VendorApplication.Rejected",
                $"Application:{applicationId}, Reason:{reason}");

            return true;
        }
    }
}
