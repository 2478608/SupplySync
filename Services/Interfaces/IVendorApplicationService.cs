using System.Collections.Generic;
using System.Threading.Tasks;
using SupplySync.DTOs.Vendor;

namespace SupplySync.Services.Interfaces
{
    public interface IVendorApplicationService
    {
        Task<VendorApplicationResponseDto> CreateApplicationAsync(
            CreateVendorApplicationRequestDto dto);

        Task<List<VendorApplicationResponseDto>> ListPendingAsync();

        Task<VendorApplicationResponseDto?> GetByIdAsync(int applicationId);

        Task<VendorResponseDto?> ApproveApplicationAsync(int id);

        Task<bool> RejectApplicationAsync(int applicationId, string reason);
    }
}