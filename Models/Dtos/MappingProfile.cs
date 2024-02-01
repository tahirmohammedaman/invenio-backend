using AutoMapper;
using invenio.Models.Dtos.Category;
using invenio.Models.Dtos.Product;

namespace invenio.Models.Dtos;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Product
        CreateMap<Models.Product, ProductDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
        CreateMap<CreateProductDto, Models.Product>();
        CreateMap<UpdateProductDto, Models.Product>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        
        // Category
        CreateMap<Models.Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Models.Category>();
        CreateMap<UpdateCategoryDto, Models.Category>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        
        // Supplier
        CreateMap<Models.Supplier, SupplierDto>();
        CreateMap<CreateSupplierDto, Models.Supplier>();
        CreateMap<UpdateSupplierDto, Models.Supplier>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        
        // Supply
        CreateMap<Models.Supply, SupplyDto>();
        CreateMap<CreateSupplyDto, Models.Supply>();
        CreateMap<UpdateSupplyDto, Models.Supply>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

    }
}