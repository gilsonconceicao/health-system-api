using System.ComponentModel.DataAnnotations;
namespace HealthSystem.Application.DTOs.Create
{
    #nullable disable
    public class PatientCreateModel
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Smoker { get; set; }
        public bool RegularExercise { get; set; }
        public DateTime BirthDate { get; set; }
        public AddressCreateModel Address { get; set; }
    }
}