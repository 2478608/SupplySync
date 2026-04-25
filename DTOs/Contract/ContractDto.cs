using SupplySync.Constants.Enums;

namespace SupplySync.DTOs.Contract
{



    public class ContractFiltersRequestDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? StartValue { get; set; }
        public decimal? EndValue { get; set; }
        public ContractStatus? Status { get; set; }
    }
    public class ContractResponseDto
    {
        public int ContractID { get; set; }
        public int VendorID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Value { get; set; }
        public string? Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
    public class ContractTermFiltersRequestDto
    {
        public bool? ComplianceFlag { get; set; }
    }
    public class ContractTermResponseDto
    {
        public int TermID { get; set; }
        public int ContractID { get; set; }
        public string Description { get; set; } = default!;
        public bool ComplianceFlag { get; set; }
        public int? DeliveryTimelineDays { get; set; }
        public string? QualityRequirement { get; set; }
        public string? PenaltyClause { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ContractWithTermsResponseDto : ContractResponseDto
    {
        public List<ContractTermResponseDto>? Terms { get; set; }
    }
    public class CreateContractRequestDto
    {
        public int VendorID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Value { get; set; }
        public List<CreateContractTermRequestDto>? Terms { get; set; }
    }



    public class CreateContractTermRequestDto
    {
        public int ContractID { get; set; }
        public string Description { get; set; } = default!;
        public bool ComplianceFlag { get; set; }
        public int? DeliveryTimelineDays { get; set; }
        public string? QualityRequirement { get; set; }
        public string? PenaltyClause { get; set; }
    }
    public class UpdateContractRequestDto
    {
        public int? VendorID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal? Value { get; set; }
    }
}


