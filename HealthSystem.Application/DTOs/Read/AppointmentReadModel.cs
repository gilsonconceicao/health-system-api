using HealthSystem.Application.DTOs.Enums;
using HealthSystem.Application.DTOs.Read;
using HealthSystem.Application.DTOs.Update;

namespace HealthSystem.Domain.Entities;
public class AppointmentReadModel
#nullable disable
{
    public Guid Id { get; set; }
    public string Reason { get; set; }
    public string FeedbackPatient { get; set; }
    public AppointmentStatus Status { get; set; }
    public string StatusDisplay { get; set; }
    public bool IsCanceled { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string AppointmentDateDisplay { get; set; }
    public Guid PatientId { get; set; }
    public PatientReadModel Patient { get; set; }
}
