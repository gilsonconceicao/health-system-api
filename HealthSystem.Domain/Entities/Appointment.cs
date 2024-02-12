using HealthSystem.Application.DTOs.Enums;

namespace HealthSystem.Domain.Entities;
public class Appointment : Base
#nullable disable
{
    public string Reason { get; set; }
    public string FeedbackPatient { get; set; }
    public bool IsCanceled { get; set; }
    public bool IsEdited { get; set; }
    public AppointmentStatus Status { get; set; }
    public DateTime AppointmentDate { get; set; }
    public virtual Patient Patient { get; set; }
    public Guid PatientId { get; set; }
}
