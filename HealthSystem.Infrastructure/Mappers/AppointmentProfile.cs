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
                    dst => dst.StatusDisplay,
                    map => map.MapFrom(data => data.Status.GetDescription()))
                .ReverseMap();
        }
    }
}