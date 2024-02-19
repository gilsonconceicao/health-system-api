using HealthSystem.Domain.Entities;

namespace HealthSystem.Application.DTOs.Read;

public class PatientToAppointmentsReadModel
#nullable disable
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public string Name { get; set; }
    public string LastName { get; set; }}
