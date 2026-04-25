using SupplySync.Constants.Enums;

namespace SupplySync.DTOs.Admin
{
    public class ApprovalWorkflowResponseDto
    {
        public int WorkflowID { get; set; }
        public string Resource { get; set; } = default!;
        public RoleType ApproverRole { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CreateApprovalWorkflowRequestDto
    {
        public string Resource { get; set; } = default!;
        public RoleType ApproverRole { get; set; }
        // e.g. "VendorApplication" public RoleType ApproverRole { get; set; }
    }

    public class CreateVendorCategoryRequestDto
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class VendorCategoryResponseDto
    {
        public int VendorCategoryID { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
