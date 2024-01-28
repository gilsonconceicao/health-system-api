using HealthSystem.Application.DTOs.Create;

namespace HealthSystem.Domain.Interfaces;
public interface IAppointmentRepository
{
    Task AddAppointmentAsync(AppointmentCreateModel Model, Guid PatientId);
    Task<bool> CheckExistsAppointmentByPatientIdAsync(Guid PatientId);    
}
