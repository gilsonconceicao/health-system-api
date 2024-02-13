using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.DTOs.Read;
using HealthSystem.Domain.Entities;

namespace HealthSystem.Domain.Interfaces;
public interface IAppointmentRepository
{
    Task<PaginationList<List<AppointmentReadModel>>> GetAllAppointments();
    Task AddAppointmentAsync(AppointmentCreateModel Model, Guid PatientId);
    Task CancelAppointmentAsync(Appointment Appointment);
    Task CompletedAppointmentAsync(Appointment Appointment);
    Task ConfirmParticipationAsync(Appointment Appointment);
    Task AddFeedbackByIdAsync(Appointment Appointment, string Feedback);
    Task<Appointment> GetAppointmentById(Guid Id);
}
