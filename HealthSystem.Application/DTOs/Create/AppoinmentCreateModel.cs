using System.ComponentModel.DataAnnotations;
using HealthSystem.Application.DataAnnotations;
using HealthSystem.Application.DTOs.Enums;
using HealthSystem.Application.DTOs.Update;

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
        public string? FeedbackPatient { get; set; }
        public PatientStatus Status { get; set; }
    }
}