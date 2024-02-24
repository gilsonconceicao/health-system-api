using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.Validations;

namespace HealthSystem.Application.Validators;
#nullable disable
public class AppointmentCreateModelValidator
{
    public ValidationsHandleErrors Validate(AppointmentCreateModel model, string? Identification = null)
    {
        string resource = "Appointment"; 
        var dateValue = model.AppointmentDate;
        var currentDate = DateTime.Now;

        var sameMonth = dateValue.Month == currentDate.Month;
        var sameYear = dateValue.Year == currentDate.Year;

        if (model.AppointmentDate == default)
        {
            return new ValidationsHandleErrors
            {
                Resource = resource,
                ErrorMessage = "Data da consulta precisa ser preenchida.",
                Identification = Identification
            };
        }
        
        if (model.AppointmentDate.Date < DateTime.Today)
        {
            return new ValidationsHandleErrors
            {
                Resource = resource,
                ErrorMessage = "Data da consulta não pode estar no passado.",
                Identification = Identification
            };
        };

        if (dateValue.Day == currentDate.Date.Day && sameMonth && sameYear) 
        {
            return new ValidationsHandleErrors {
                ErrorMessage = "Não é possível agendar para hoje. Tente outra data", 
                Identification = Identification,
                Resource = resource
            };
        }        


        return null;
    }
}