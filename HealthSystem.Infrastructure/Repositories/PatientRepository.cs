using AutoMapper;
using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.DTOs.Update;
using HealthSystem.Domain.Entities;
using HealthSystem.Domain.Interfaces;
using HealthSystem.Infrastructure.Data.Contexts;

namespace HealthSystem.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly PatientsContext _patientsContext;
    private readonly IMapper _mapper;

    public PatientRepository(PatientsContext patientsContext, IMapper mapper)
    {
        _patientsContext = patientsContext;
        _mapper = mapper;
    }

    public async Task AddPatientAsync(PatientCreateModel model)
    {
        Patient patient = _mapper.Map<PatientCreateModel, Patient>(model);
        await _patientsContext.AddAsync(patient);
        _patientsContext.SaveChanges();
    }

    public async void UpdatePatientAsync(Patient currentEntity, PatientUpdateModel updatedEntity)
    {
        if (currentEntity.Address != null) {
            currentEntity.Address.ZipCode = updatedEntity.Address.ZipCode;
            currentEntity.Address.State = updatedEntity.Address.State; 
            currentEntity.Address.Street = updatedEntity.Address.Street; 
            currentEntity.Address.City = updatedEntity.Address.City; 
        }

        currentEntity.Name = updatedEntity.Name;
        currentEntity.LastName = updatedEntity.LastName;
        currentEntity.Email = updatedEntity.Email;
        currentEntity.BirthDate = updatedEntity.BirthDate;
        currentEntity.Gender = updatedEntity.Gender;
        currentEntity.PhoneNumber = updatedEntity.PhoneNumber;
        currentEntity.RegularExercise = updatedEntity.RegularExercise;
        currentEntity.Smoker = updatedEntity.Smoker;

        await _patientsContext.SaveChangesAsync();
    }

}
