using AutoMapper;
using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.DTOs.Enums;
using HealthSystem.Application.DTOs.Read;
using HealthSystem.Domain.Entities;
using HealthSystem.Domain.Interfaces;
using HealthSystem.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HealthSystem.Infrastructure.Repositories;
#nullable disable
public class AppointmentRepository : IAppointmentRepository
{
    private readonly PatientsContext _patientsContext;
    private readonly IMapper _mapper;

    public AppointmentRepository(PatientsContext patientsContext,
        IMapper mapper)
    {
        _patientsContext = patientsContext;
   
        _mapper = mapper;
    }

    public async Task AddAppointmentAsync(AppointmentCreateModel Model, Guid PatientId)
    {
        Appointment Appointment = _mapper.Map<Appointment>(Model);
        Appointment.PatientId = PatientId;

        await _patientsContext.Set<Appointment>().AddAsync(Appointment);
        await _patientsContext.SaveChangesAsync();
    }

    public async Task<PaginationList<List<AppointmentReadModel>>> GetAllAppointments()
    {
        List<Appointment> appointments = await _patientsContext.Appointments.ToListAsync();
        var query = _mapper.Map<List<AppointmentReadModel>>(appointments);

        return new PaginationList<List<AppointmentReadModel>>()
        {
            Data = query,
            TotalItems = query.Count
        };
    }

    public async Task CancelAppointmentAsync(Appointment Appointment)
    {
        Appointment.IsCanceled = true;
        Appointment.Status = AppointmentStatus.Cancelled;

        await _patientsContext.SaveChangesAsync();
    }

    public async Task<Appointment> GetAppointmentById(Guid Id) => await _patientsContext.Appointments.FirstOrDefaultAsync(x => x.Id == Id);

    public async Task AddFeedbackByIdAsync(Appointment Appointment, string FeedbackMessage)
    {
        if (Appointment.FeedbackPatient.Trim().Length > 0)
        {
            // Appointment.IsEdited = true;
        }

        Appointment.FeedbackPatient = FeedbackMessage;
        await _patientsContext.SaveChangesAsync();
    }
}
