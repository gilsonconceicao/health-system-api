using System.ComponentModel.DataAnnotations;
using HealthSystem.Application.DTOs.Update;

namespace HealthSystem.Application.DTOs.Create
{
#nullable disable
    public class AppointmentCreateModel
    {
        public string Reason { get; set; }
        public string FeedbackPatient { get; set; }
        public PatientStatus Status { get; set; }
        public DateTime AppointmentDate { get; set; }
    }
}