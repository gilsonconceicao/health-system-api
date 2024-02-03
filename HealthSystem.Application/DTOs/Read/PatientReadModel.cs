using HealthSystem.Domain.Entities;

namespace HealthSystem.Application.DTOs.Read;

public class PatientReadModel
#nullable disable
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public string Name { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool Smoker { get; set; }
    public bool RegularExercise { get; set; }
    public DateTime BirthDate { get; set; }
    public virtual AddressReadModel Address { get; set; }
    public virtual ICollection<AppointmentReadModel>? Appointments { get; set; } = new List<AppointmentReadModel>();
}
