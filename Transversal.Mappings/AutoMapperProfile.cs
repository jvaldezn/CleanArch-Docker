using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Transversal.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
