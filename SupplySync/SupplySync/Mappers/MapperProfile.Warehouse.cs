using SupplySync.DTOs.InventoryandWarehouse;
using SupplySync.Models;
using AutoMapper;

namespace SupplySync.Mappers
{
    public partial class MapperProfile
    {
        public void ConfigureWarehouseMappings()
        {
            CreateMap<CreateWarehouseDto, Warehouse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(_ => false));

            CreateMap<UpdateWarehouseRequestDto, Warehouse>().ReverseMap();
                //.ForMember(dest => dest.Status, opt => opt.Condition(src => src.Status.HasValue) && opt.MapFrom(src => src.Status.Value))
                //.ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                //.ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

            CreateMap<Warehouse, WarehouseResponseDto>();

            CreateMap<Warehouse, WarehouseListResponseDto>();
        }
    }
}
