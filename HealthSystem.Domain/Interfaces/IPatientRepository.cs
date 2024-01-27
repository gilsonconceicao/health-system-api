using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.DTOs.Update;
using HealthSystem.Domain.Entities;

namespace HealthSystem.Domain.Interfaces;
public interface IPatientRepository
{
    Task AddPatientAsync(PatientCreateModel model); 
    void UpdatePatientAsync(Patient current, PatientUpdateModel model); 
}
