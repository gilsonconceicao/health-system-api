
using AutoMapper;
using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.DTOs.Read;
using HealthSystem.Application.DTOs.Update;
using HealthSystem.Application.Validations;
using HealthSystem.Domain.Entities;
using HealthSystem.Domain.Interfaces;
using HealthSystem.Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace HealthSystem.Infrastructure.Repositories;
#nullable disable
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

    public async Task<PaginationList<List<PatientReadModel>>> GetAllPatiets(
        int Page = 1,
        int Size = 5,
        string Name = null,
        string Email = null,
        DateTime? BirthDate = null
    )
    {
        List<Patient> patientsList = await _patientsContext.Patients
                                                        .Take(Size)
                                                        .Skip(Size * Page)
                                                        .ToListAsync();
        var queryData = _mapper.Map<List<PatientReadModel>>(patientsList);

        if (new ValidateFilterValue<string>(Name).IsValidField())
        {
            queryData = queryData.Where(opt => opt.Name.ToLower().Contains(Name.ToLower())).ToList();
        };

        if (new ValidateFilterValue<string>(Email).IsValidField())
        {
            queryData = queryData.Where(p => p.Email.ToLower().Contains(Email.ToLower())).ToList();
        };

        if (BirthDate != null && new ValidateFilterValue<string>(BirthDate.ToString()).IsValidField())
        {
            queryData = queryData.Where(p =>
            {
                string format = "dd/MM/yyyy";
                var currentValue = p.BirthDate.ToString(format);
                var birthDate = BirthDate.GetValueOrDefault().ToString(format);
                return currentValue.Contains(birthDate);
            }).ToList();
        };

        return new PaginationList<List<PatientReadModel>>()
        {
            Data = queryData,
            TotalItems = queryData.Count
        };
    }


    public async Task UpdatePatientAsync(Patient currentEntity, PatientUpdateModel updatedEntity)
    {
        if (currentEntity.Address != null)
        {
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

        _patientsContext.Patients.Update(currentEntity);
        await _patientsContext.SaveChangesAsync();
    }

}
