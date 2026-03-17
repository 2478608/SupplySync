using SupplySync.Constants.Enums;
using SupplySync.DTOs.Finance;
using SupplySync.Models;

namespace SupplySync.Mappers
{
    public partial class MapperProfile
    {
        private void ConfigureFinanceMappings()
        {
            CreateMap<CreateInvoiceRequestDto, Invoice>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ConvertInvoiceStatus(src.Status)))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false));
        }
        private static InvoiceStatus ConvertInvoiceStatus(string status)
        {
            return Enum.TryParse<InvoiceStatus>(status, true, out var parsed)
                ? parsed
                : InvoiceStatus.Submitted; 
        }
    }
}
