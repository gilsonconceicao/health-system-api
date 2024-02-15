using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.DTOs.Read;
using HealthSystem.Domain.Entities;

namespace HealthSystem.Domain.Interfaces;
public interface IDashboardRepository
{
    Task<DashboardAppointmentReadModel> GetDashboardDataAppointment();
}
