using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.DTOs.Read;
using HealthSystem.Domain.Entities;

namespace HealthSystem.Domain.Interfaces;
public interface IAppointmentRepository
{
    Task<PaginationList<List<AppointmentReadModel>>> GetAllAppointments();
    Task AddAppointmentAsync(AppointmentCreateModel Model, Guid PatientId);
}
