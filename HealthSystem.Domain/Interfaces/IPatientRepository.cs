using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.DTOs.Read;
using HealthSystem.Application.DTOs.Update;
using HealthSystem.Domain.Entities;

namespace HealthSystem.Domain.Interfaces;
public interface IPatientRepository
{
    Task<PaginationList<List<PatientReadModel>>> GetAllPatiets(
        int Page, 
        int Size, 
        string Name, 
        string Email,
        DateTime? BirthDate
    );
    Task AddPatientAsync(PatientCreateModel model); 
    Task UpdatePatientAsync(Patient current, PatientUpdateModel model); 
}
