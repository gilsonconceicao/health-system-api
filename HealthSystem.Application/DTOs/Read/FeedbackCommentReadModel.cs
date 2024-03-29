using HealthSystem.Application.DTOs.Read;

namespace HealthSystem.Domain.Entities
{
#nullable disable
    public class FeedbackCommentReadModel
    {
        public string Feedback { get; set; }
        public Guid AppointmentId { get; set; }
        public DateTime CreatedAt {get; set;}
        public string Name {get; set; }
    }
}