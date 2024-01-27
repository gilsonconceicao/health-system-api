using AutoMapper;
using HealthSystem.Application.DTOs.Create;
using HealthSystem.Domain.Entities;

namespace HealthSystem.Application.Mappers
{
    public class PatientProfile : Profile
    {
        public PatientProfile()
        {
            CreateMap<Patient, PatientCreateModel>().ReverseMap();
        }
    }
}