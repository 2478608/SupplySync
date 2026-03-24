using AutoMapper;

namespace SupplySync.Mappers
{
    public partial class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Initialize mappings for each domain/model
            ConfigureAuditMappings();
            ConfigureComplianceRecordMappings();
            ConfigureReportMappings();
            ConfigureWarehouseMappings();
            ConfigureInventoryMappings();
            ConfigureReceiptMappings();


            ConfigureInvoiceMappings();
            ConfigurePaymentMappings();


            ConfigureVendorMappings();
            ConfigureContractMappings();


        }
    }
}