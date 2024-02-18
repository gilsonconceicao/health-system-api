using AutoMapper;
using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.DTOs.Read;
using HealthSystem.Domain.Entities;

namespace HealthSystem.Application.Mappers
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientCreateModel>().ReverseMap();
            CreateMap<Patient, PatientReadModel>()
                .ForMember(
                    dst => dst.BirthDateDisplay, 
                    src => src.MapFrom(value => value.BirthDate.ToString())
                )
                .ReverseMap();
        }
    }
}