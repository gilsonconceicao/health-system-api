using AutoMapper;
using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.DTOs.Update;
using HealthSystem.Domain.Entities;

namespace HealthSystem.Application.Mappers
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, AddressCreateModel>().ReverseMap();
            CreateMap<AddressReadModel, Address>().ReverseMap();
            CreateMap<Address, AddressUpdateModel>().ReverseMap();
        }
    }
}