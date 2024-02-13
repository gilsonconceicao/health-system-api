using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HealthSystem.Application.DTOs.Enums;
#nullable disable
public enum AppointmentStatus
{
    [Description("Aguardando confirmação de presença")]
    awaitConfirmParticipation = 0,
    [Description("Presença confirmada")]
    confirmParticipation = 1,
    [Description("Cancelada")]
    Cancelled = 2,
    [Description("Concluída")]
    Completed = 3
}
