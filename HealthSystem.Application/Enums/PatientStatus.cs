using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthSystem.Application.DTOs.Enums;
#nullable disable
public enum PatientStatus
{
    [Description("Agendado")]
    Scheduled = 0,
    [Description("Cancelada")]
    Cancelled = 1,
    [Description("Pendente")]
    Pending = 2,
    [Description("Realizada")]
    Realize = 3,
    [Description("Concluída")]
    Completed = 4,
}
