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
            CreateMap<PatientReadModel, Patient>().ReverseMap();
        }
    }
}