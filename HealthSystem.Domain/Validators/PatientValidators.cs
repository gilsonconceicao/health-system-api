using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.DTOs.Enums;
using HealthSystem.Application.Services;
using HealthSystem.Application.Validations;
using HealthSystem.Domain.Entities;
using Microsoft.VisualBasic;

namespace HealthSystem.Application.Validators;
#nullable disable
public class PatientValidators
{
    public ValidationsHandleErrors NotFoundPatient(Patient entity, Guid Id)
    {
        if (entity == null)
        {
            return new ValidationsHandleErrors
            {
                ErrorMessage = "Paciente não encontrado ou não existe",
                Identification = Id.ToString(),
                Resource = ErrorType.NOT_FOUND_REGISTER.GetDescription(),
            };
        }
        return null;
    }
}