using AutoMapper;
using HealthSystem.Application.DTOs.Enums;
using HealthSystem.Domain.Entities;
using HealthSystem.Domain.Interfaces;
using HealthSystem.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HealthSystem.Infrastructure.Repositories;
#nullable disable
public class DashboardRepository : IDashboardRepository
{
    private readonly PatientsContext _patientsContext;
    private readonly IMapper _mapper;

    public DashboardRepository(PatientsContext patientsContext,
        IMapper mapper)
    {
        _patientsContext = patientsContext;
        _mapper = mapper;
    }

    public async Task<DashboardAppointmentReadModel> GetDashboardDataAppointment()
    {
        var appointments = _patientsContext.Appointments;

        var TotalsAppointmentsCancelled = _patientsContext.Appointments.Where(x => x.Status == AppointmentStatus.Cancelled);
        var TotalsAppointmentsConfirmParticipation = _patientsContext.Appointments.Where(x => x.Status == AppointmentStatus.confirmParticipation);

        List<FeedbackCommentReadModel> feedbackList = await appointments
                                                            .Select(item => new FeedbackCommentReadModel
                                                            {
                                                                AppointmentId = item.Id,
                                                                Feedback = item.FeedbackPatient
                                                            }).Where(item => item.Feedback != null).ToListAsync();


        return new DashboardAppointmentReadModel()
        {
            FeedbackPatients = feedbackList,
            TotalsAppointments = appointments.Count(),
            TotalsAppointmentsCancelled = TotalsAppointmentsCancelled.Count(),
            TotalsAppointmentsConfirmed = TotalsAppointmentsConfirmParticipation.Count(), 
            TotalPatients = _patientsContext.Patients.Count()
        };
    }

}
