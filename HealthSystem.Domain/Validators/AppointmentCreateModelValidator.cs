using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.DTOs.Enums;
using HealthSystem.Application.Services;
using HealthSystem.Application.Validations;
using HealthSystem.Domain.Entities;

namespace HealthSystem.Application.Validators;
#nullable disable
public class AppointmentModelsValidator
{

    public List<ValidationsHandleErrors> ValidateCreateModel(Guid PatientId, AppointmentCreateModel model, Patient patient, List<Appointment> appointments)
    {
        var errors = new List<ValidationsHandleErrors>();

        var dateValue = model.AppointmentDate;
        var currentDate = DateTime.Now;

        var sameMonth = dateValue.Month == currentDate.Month;
        var sameYear = dateValue.Year == currentDate.Year;

        if (model.AppointmentDate == default)
        {
            errors.Add(new ValidationsHandleErrors
            {
                Resource = ErrorType.INVALID_FIELD.GetDescription(),
                ErrorMessage = "Data da consulta precisa ser preenchida.",
                Identification = PatientId.ToString()
            });
        }

        if (model.AppointmentDate.Date < DateTime.Today)
        {
            errors.Add(new ValidationsHandleErrors
            {
                Resource = ErrorType.INVALID_FIELD.GetDescription(),
                ErrorMessage = "Data da consulta não pode estar no passado.",
                Identification = PatientId.ToString()
            });
        };

        if (dateValue.Day == currentDate.Date.Day && sameMonth && sameYear)
        {
            errors.Add(new ValidationsHandleErrors
            {
                ErrorMessage = "Não é possível agendar para hoje. Tente outra data",
                Identification = PatientId.ToString(),
                Resource = ErrorType.INVALID_FIELD.GetDescription()
            });
        }

        if (patient == null)
        {
            errors.Add(new ValidationsHandleErrors
            {
                ErrorMessage = "Paciente não encontrado ou não existe",
                Identification = PatientId.ToString(),
                Resource = ErrorType.NOT_FOUND_REGISTER.GetDescription()
            });
        }
        else
        {
            var countAppointmentByPatientId = appointments.Where((appointment) => appointment.PatientId == patient.Id).ToList();
            if (countAppointmentByPatientId.Count() >= 3)
            {
                errors.Add(new ValidationsHandleErrors
                {
                    ErrorMessage = "Paciente já possui 3 consultas. Agendamento não pode ser realizado.",
                    Identification = PatientId.ToString(),
                    Resource = ErrorType.EXPIRED_QUANTITY.GetDescription()
                });
            }
        }


        if (appointments.Count > 0)
        {
            var validateAppointmentDate = appointments.Where((x) =>
            {
                return x.AppointmentDate.ToString() == model.AppointmentDate.ToString();
            });

            if (validateAppointmentDate.Count() > 0)
            {
                errors.Add(new ValidationsHandleErrors
                {
                    ErrorMessage = "Já existe uma consulta para este horário",
                    Identification = model.AppointmentDate.ToString(),
                    Resource = ErrorType.EXPIRED_QUANTITY.GetDescription()
                });
            }
        }

        return errors;
    }

    
}