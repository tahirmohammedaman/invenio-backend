using AutoMapper;
using invenio.Models.Dtos.Category;
using invenio.Models.Dtos.Product;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace invenio.Models.Dtos;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Models.Product, ProductDto>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
        CreateMap<CreateProductDto, Models.Product>();
        CreateMap<UpdateProductDto, Models.Product>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<Models.Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Models.Category>();

        CreateMap<UpdateCategoryDto, Models.Category>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
        
        CreateMap<Models.Supplier, SupplierDto>();
        CreateMap<CreateSupplierDto, Models.Supplier>();
        CreateMap<UpdateSupplierDto, Models.Supplier>()
            .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

    }
}