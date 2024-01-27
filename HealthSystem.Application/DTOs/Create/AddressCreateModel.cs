using System.ComponentModel.DataAnnotations;

namespace HealthSystem.Application.DTOs.Create
{
    #nullable disable
    public class AddressCreateModel
    {
        [Required(ErrorMessage = "Rua é obrigatório")]
        public string Street { get; set; }
        [Required(ErrorMessage = "Estado é obrigatório")]
        public string State { get; set; }
        [Required(ErrorMessage = "CEP é obrigatório")]
        public string ZipCode { get; set; }
        [Required(ErrorMessage = "Cidade é obrigatório")]
        public string City { get; set; }
        public string Number { get; set; }
    }
}