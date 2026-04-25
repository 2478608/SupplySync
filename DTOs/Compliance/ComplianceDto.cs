using SupplySync.Constants.Enums;

namespace SupplySync.DTOs.Compliance
{

    public class ComplianceRecordListResponseDto
    {
        public int ComplianceID { get; set; }
        public int ContractID { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Result { get; set; } = string.Empty;
        public DateOnly Date { get; set; }
        public string? Notes { get; set; }
    }
    public class ComplianceRecordResponseDto
    {
        public int ComplianceID { get; set; }
        public int ContractID { get; set; }
        public ComplianceType Type { get; set; }
        public ComplianceResult Result { get; set; }
        public DateOnly Date { get; set; }
        public string? Notes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class CreateComplianceRecordRequestDto
    {
        public int ContractID { get; set; }
        public ComplianceType Type { get; set; }
        public ComplianceResult Result { get; set; }
        public DateOnly Date { get; set; }
        public string? Notes { get; set; }
    }
        public class UpdateComplianceRecordRequestDto
    {

        public ComplianceType Type { get; set; }
        public ComplianceResult Result { get; set; }
        public DateOnly Date { get; set; }
        public string? Notes { get; set; }
    }
}
