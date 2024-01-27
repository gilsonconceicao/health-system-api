using System.ComponentModel.DataAnnotations;
namespace HealthSystem.Application.DTOs.Create
{
    #nullable disable
    public class PatientCreateModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Sobrenome é obrigatório")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Gênero é obrigatório")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Email é obrigatório")]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Smoker { get; set; }
        public bool RegularExercise { get; set; }
        public DateTime BirthDate { get; set; }
        public AddressCreateModel Address { get; set; }
    }
}