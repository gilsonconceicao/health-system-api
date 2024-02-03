using AutoMapper;
using HealthSystem.Application.DTOs.Create;
using HealthSystem.Domain.Entities;
using HealthSystem.Infrastructure.Data.Contexts;
using HealthSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HealthSystem.Web.Controller
{
    [ApiController]
    [Route("[Controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly GenericRepository<Appointment> _genericRepository;
        private readonly GenericRepository<Patient> _genericRepositoryPatient;
        private readonly AppointmentRepository _AppointmentRepository;

        public AppointmentController(PatientsContext patientsContext, IMapper mapper)
        {
            _genericRepository = new GenericRepository<Appointment>(patientsContext);
            _genericRepositoryPatient = new GenericRepository<Patient>(patientsContext);

            _AppointmentRepository = new AppointmentRepository(patientsContext, mapper);
        }

        /// <summary>
        /// Agenda uma nova consulta por tomador
        /// </summary>
        /// <returns>Realiza o agendamento de uma consulta</returns>
        /// <response code="200">200 Para sucesso ao realizat consulta</response>
        /// <response code="400">400 se houver falha na requisição</response>
        [HttpPost("{PatientId}")]
        public async Task<IActionResult> CreateAppointmentAsync(Guid PatientId, [FromBody] AppointmentCreateModel model)
        {
            try
            {
                Patient patient = await _genericRepositoryPatient.GetByIdAsync(PatientId);
                var listAppointment = await _genericRepository.GetAll();


                if (patient == null)
                {
                    return BadRequest(new
                    {
                        message = "Paciente não encontrado ou não existe",
                        PatientId
                    });
                }

                var countAppointmentByPatientId = listAppointment.Where((appointment) => appointment.PatientId == patient.Id);

                if (countAppointmentByPatientId.Count() > 3)
                {
                    return BadRequest(new
                    {
                        message = "Paciente já possui 3 consultas. Agendamento não pode ser realizado.",
                        PatientId
                    });
                }


                var validateAppointmentDate = listAppointment.Where((x) =>
                {
                    return x.AppointmentDate.ToString() == model.AppointmentDate.ToString();
                });

                if (validateAppointmentDate.Count() > 0)
                {
                    return BadRequest(new
                    {
                        message = "Já existe uma consulta para este horário",
                        AppointmentDate = model.AppointmentDate
                    });
                }

                await _AppointmentRepository.AddAppointmentAsync(model, PatientId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}