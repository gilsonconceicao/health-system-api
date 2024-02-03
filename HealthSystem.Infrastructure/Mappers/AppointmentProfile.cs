using AutoMapper;
using HealthSystem.Application.DTOs.Create;
using HealthSystem.Domain.Entities;

namespace HealthSystem.Application.Mappers
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentCreateModel>()
                .ReverseMap();
            CreateMap<Appointment, AppointmentReadModel>()
                .ReverseMap();
        }
    }
}