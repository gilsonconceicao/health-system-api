using HealthSystem.Application.DTOs.Create;
using HealthSystem.Domain.Entities;

namespace HealthSystem.Domain.Interfaces;
public interface IAppointmentRepository
{
    Task<List<AppointmentReadModel>> GetAllAppointments();
    Task AddAppointmentAsync(AppointmentCreateModel Model, Guid PatientId);
}
