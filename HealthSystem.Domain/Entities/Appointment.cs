using HealthSystem.Application.DTOs.Update;

namespace HealthSystem.Domain.Entities;
public class Appointment : Base
#nullable disable
{
    public string Reason { get; set; }
    public string FeedbackPatient { get; set; }
    public PatientStatus Status { get; set; }
    public DateTime AppointmentDate { get; set; }
    public virtual Patient Patient { get; set; }
    public Guid PatientId { get; set; }
}
