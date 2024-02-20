using System.ComponentModel.DataAnnotations;
using HealthSystem.Application.DataAnnotations;

namespace HealthSystem.Application.DTOs.Create
{
#nullable disable
    public class FeedbackCreateModel
    {
        [Required(ErrorMessage = "Feedback: precisa ser preenchido")]
        public string FeedbackMessage { get; set; }
    }
}