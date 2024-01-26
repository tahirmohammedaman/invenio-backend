using AutoMapper;
using invenio.Models.Dtos.Product;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace invenio.Models.Dtos;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Models.Product, ProductDto>();
        CreateMap<CreateProductDto, Models.Product>();
        CreateMap<UpdateProductDto, Models.Product>();
    }
}