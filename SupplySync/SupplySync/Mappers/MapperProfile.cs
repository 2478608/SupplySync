using AutoMapper;

namespace SupplySync.Mappers
{
    public partial class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Initialize mappings for each domain/model
            ConfigureAuditMappings();
<<<<<<< HEAD
            ConfigureFinanceMappings();
=======
            ConfigureComplianceRecordMappings();
            ConfigureReportMappings();

>>>>>>> 20bd240adc0c13a000aecedd79c55df8786a01cb
        }
    }
}