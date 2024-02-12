using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthSystem.Application.DTOs.Enums;
#nullable disable
public enum AppointmentStatus
{
    [Description("Agendado")]
    Scheduled = 0,
    [Description("Cancelada")]
    Cancelled = 1,
    [Description("Concluída")]
    Completed = 2,
}
