using System.ComponentModel.DataAnnotations;

namespace HealthSystem.Application.DTOs.Create
{
#nullable disable
    public class AppointmentCreateModel
    {
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }
    }
}