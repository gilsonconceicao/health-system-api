using System.ComponentModel.DataAnnotations;
using HealthSystem.Application.DataAnnotations;

namespace HealthSystem.Application.DTOs.Create
{
#nullable disable
    public class AppointmentCreateModel
    {
        [DateAttributeValidate]
        [Required(ErrorMessage = "Data da consulta: precisa ser preenchida")]
        public DateTime AppointmentDate { get; set; }
        [Required(ErrorMessage = "Razão da consulta: precisa ser preenchida")]
        public string Reason { get; set; }
    }
}