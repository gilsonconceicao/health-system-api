using AutoMapper;
using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.DTOs.Read;
using HealthSystem.Application.Services;
using HealthSystem.Domain.Entities;

namespace HealthSystem.Application.Mappers
{
    public class AppointmentProfile : Profile
    {
        public AppointmentProfile()
        {
            CreateMap<Appointment, AppointmentCreateModel>()
                .ReverseMap();
            CreateMap<Appointment, DashboardAppointmentReadModel>()
                .ReverseMap();
            CreateMap<Appointment, AppointmentReadModel>()
                .ForMember(
                    dest => dest.StatusDisplay,
                    map => map.MapFrom(data => data.Status.GetDescription())
                )
                .ForMember(
                    dest => dest.AppointmentDateDisplay,
                    map => map.MapFrom(item => item.AppointmentDate.ToString("dd/MM/yyyy"))
                )
                .ReverseMap();
        }
    }
}