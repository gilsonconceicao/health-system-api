namespace HealthSystem.Domain.Entities; 
#nullable disable
    public class Patient : Base
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Gender { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool Smoker { get; set; }
    public bool RegularExercise { get; set; }
    public DateTime BirthDate { get; set; }
#pragma warning disable CS8632
    public virtual Address? Address { get; set; }
#pragma warning restore CS8632
}
