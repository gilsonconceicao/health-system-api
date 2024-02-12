using System.ComponentModel.DataAnnotations;

namespace HealthSystem.Application.DataAnnotations;

#nullable disable
class DateAttributeValidate : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var dateValue = (DateTime)value;
        var currentDate = DateTime.Now;

        var sameMonth = dateValue.Month == currentDate.Month;
        var sameYear = dateValue.Year == currentDate.Year; 

        if ((dateValue.Month < currentDate.Month || dateValue.Year < currentDate.Year) || (dateValue.Day < currentDate.Day && sameMonth && sameYear)) 
        {
            return new ValidationResult(ErrorMessage = "Data não pode estar no passado");
        }

        if (dateValue.Day == currentDate.Date.Day && sameMonth && sameYear)
        {
            return new ValidationResult(ErrorMessage = "Não é possível agendar para hoje. Tente outra data.");
        }


        return ValidationResult.Success;
    }
}