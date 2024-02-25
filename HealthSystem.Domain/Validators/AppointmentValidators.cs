using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.DTOs.Enums;
using HealthSystem.Application.Services;
using HealthSystem.Application.Validations;
using HealthSystem.Domain.Entities;
using Microsoft.VisualBasic;

namespace HealthSystem.Application.Validators;
#nullable disable
public class AppointmentValidators
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
            if (countAppointmentByPatientId.Count() >= 1)
            {
                errors.Add(new ValidationsHandleErrors
                {
                    ErrorMessage = "Paciente já contém uma consulta em andamento. Agendamento não pode ser realizado.",
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
    public List<ValidationsHandleErrors> ValidateCancelAppointment(Appointment appointment, Guid Id)
    {
        var errors = new List<ValidationsHandleErrors>();
        var isAppointmentNotFound = NotFoundAppointment(appointment, Id);

        if (isAppointmentNotFound != null) errors.Add(isAppointmentNotFound);

        if (appointment.Status == AppointmentStatus.Cancelled)
        {
            errors.Add(new ValidationsHandleErrors
            {
                ErrorMessage = "Não é possível cancelar uma consulta que já se encontra cancelada",
                Identification = Id.ToString(),
                Resource = "Não foi possível cancelar consulta"
            });
        }

        return errors;
    }
    public List<ValidationsHandleErrors> ValidateCreateFeedback(Appointment appointment, Guid Id)
    {
        var errors = new List<ValidationsHandleErrors>();
        var isAppointmentNotFound = NotFoundAppointment(appointment, Id);

        if (isAppointmentNotFound != null) errors.Add(isAppointmentNotFound);

        if (appointment.Status == AppointmentStatus.Cancelled || appointment.Status == AppointmentStatus.awaitConfirmParticipation)
        {
            errors.Add(new ValidationsHandleErrors
            {
                ErrorMessage = "Não é possível adicionar um feedback em uma consulta que ainda não foi concluída ou de presença confirmada",
                Identification = Id.ToString(),
                Resource = $"CurrentStatus: {appointment.Status}, expected: {AppointmentStatus.Completed}"
            });
        }

        return errors;
    }

    public List<ValidationsHandleErrors> ValidateFinishedAppointment(Appointment appointment, Guid Id)
    {
        var errors = new List<ValidationsHandleErrors>();

        var isAppointmentNotFound = NotFoundAppointment(appointment, Id);

        if (isAppointmentNotFound != null)
        {
            errors.Add(isAppointmentNotFound);
        }
        else
        {
            var sameMonth = appointment.AppointmentDate.Month == DateTime.Now.Month;
            var sameYear = appointment.AppointmentDate.Year == DateTime.Now.Year;
            if ((sameMonth && sameYear) && appointment.AppointmentDate.Day + 1 >= DateTime.Now.Day)
            {
                errors.Add(new ValidationsHandleErrors
                {
                    ErrorMessage = "Só pode concluir uma consulta um dia após ser realizada",
                    Identification = Id.ToString(),
                    Resource = $"Data da consulta definida {appointment.AppointmentDate}"
                });
            }
        }

        return errors;
    }
    public List<ValidationsHandleErrors> ValidateConfirmParticipation(Appointment appointment, Guid Id)
    {
        var errors = new List<ValidationsHandleErrors>();
        var isAppointmentNotFound = NotFoundAppointment(appointment, Id);

        if (isAppointmentNotFound != null)
        {
            errors.Add(isAppointmentNotFound);
        }
        else
        {
            var sameMonth = appointment.AppointmentDate.Month == DateTime.Now.Month;
            var sameYear = appointment.AppointmentDate.Year == DateTime.Now.Year;
            if ((sameMonth && sameYear) && DateTime.Now.Day == appointment.AppointmentDate.Day)
            {
                errors.Add(new ValidationsHandleErrors
                {
                    ErrorMessage = "Não é possível confirmar presença no mesmo dia no qual foi agendado",
                    Identification = appointment.AppointmentDate.ToString(),
                    Resource = "Não foi possível confirmar consulta"
                });
            }

            if (appointment.Status == AppointmentStatus.confirmParticipation)
            {
                errors.Add(new ValidationsHandleErrors
                {
                    ErrorMessage = "Consulta já se encontra confirmada",
                    Identification = appointment.Id.ToString(),
                    Resource = "Não foi possível confirmar consulta"
                });
            }
        }

        return errors;
    }

    public ValidationsHandleErrors NotFoundAppointment(Appointment appointment, Guid Id)
    {
        if (appointment == null)
        {
            return new ValidationsHandleErrors
            {
                ErrorMessage = "Consulta não encontrada ou não existe",
                Identification = Id.ToString(),
                Resource = ErrorType.NOT_FOUND_REGISTER.GetDescription(),
            };
        }
        return null;
    }


}