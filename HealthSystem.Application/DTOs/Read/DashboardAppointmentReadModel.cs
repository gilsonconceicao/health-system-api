using HealthSystem.Application.DTOs.Read;

namespace HealthSystem.Domain.Entities
{
#nullable disable
    public class DashboardAppointmentReadModel
    {
        public int TotalsAppointments { get; set; }
        public int TotalsAppointmentsConfirmed { get; set; }
        public int TotalPatients { get; set; }
        public int TotalsAppointmentsCancelled { get; set; }
        public List<FeedbackCommentReadModel> FeedbackPatients { get; set; }
    }
}