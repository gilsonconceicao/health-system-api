using AutoMapper;
using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.DTOs.Update;
using HealthSystem.Domain.Entities;
using HealthSystem.Domain.Interfaces;
using HealthSystem.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HealthSystem.Infrastructure.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly PatientsContext _patientsContext;
    private readonly IMapper _mapper;

    public AppointmentRepository(PatientsContext patientsContext, IMapper mapper)
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
}
