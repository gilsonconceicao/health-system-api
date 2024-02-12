using AutoMapper;
using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.DTOs.Enums;
using HealthSystem.Application.DTOs.Read;
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
        /// Agendar nova consulta
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
                var appointments = await _genericRepository.GetAll();
                var listAppointment = appointments.Where(x => x.IsCanceled == false);


                if (patient == null)
                {
                    return BadRequest(new
                    {
                        message = "Paciente não encontrado ou não existe",
                        PatientId
                    });
                }

                var countAppointmentByPatientId = listAppointment.Where((appointment) => appointment.PatientId == patient.Id);

                if (countAppointmentByPatientId.Count() >= 3)
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
                // await _AppointmentRepository.AddAppointmentAsync(model, PatientId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cancelar consulta
        /// </summary>
        /// <returns>Cancela uma consulta </returns>
        /// <response code="204">204 Consulta cancelada com sucesso</response>
        /// <response code="200">200 Retorno dos dados com sucesso</response>
        /// <response code="400">400 se houver falha na requisição</response>
        [HttpPut("{Id}/Cancel")]
        public async Task<IActionResult> CancelAppointmentsAsync(Guid Id)
        {
            try
            {
                Appointment findAppointment = await _AppointmentRepository.GetAppointmentById(Id);

                if (findAppointment == null)
                {
                    return BadRequest(new
                    {
                        message = "Consulta não encontrada ou não existe",
                        Id
                    });
                }

                if (findAppointment.Status == AppointmentStatus.Cancelled)
                {
                    return BadRequest(new
                    {
                        message = "Não é possível cancelar uma consulta que já se encontra cancelada",
                        Id
                    });
                }


                await _AppointmentRepository.CancelAppointmentAsync(findAppointment);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adicionar ou edita um feedback à consulta
        /// </summary>
        /// <returns>Adiciona um feedback/comentário</returns>
        /// <response code="200">200 Retorno dos dados com sucesso</response>
        /// <response code="400">400 se houver falha na requisição</response>
        [HttpPost("{Id}/Feedback")]
        public async Task<IActionResult> AddFeedbackAsync(Guid Id, [FromBody] string FeedbackMessage)
        {
            try
            {
                Appointment findAppointment = await _AppointmentRepository.GetAppointmentById(Id);

                if (findAppointment == null)
                {
                    return BadRequest(new
                    {
                        message = "Consulta não encontrada ou não existe",
                        Id
                    });
                }

                if (findAppointment.Status != AppointmentStatus.Completed)
                {
                    return BadRequest(new
                    {
                        message = "Não é possível adicionar um feedback em uma consulta que ainda não foi concluída",
                        Id,
                        details = $"CurrentStatus: {findAppointment.Status}, expected: {AppointmentStatus.Completed}"
                    });
                }
                await _AppointmentRepository.AddFeedbackByIdAsync(findAppointment, FeedbackMessage);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Lista todas as consultas cadastradas
        /// </summary>
        /// <returns>Obtem uma lista de consultas</returns>
        /// <response code="200">200 Retorno dos dados com sucesso</response>
        /// <response code="400">400 se houver falha na requisição</response>
        [HttpGet]
        public async Task<IActionResult> GetAllAppointmentsAsync()
        {
            try
            {
                PaginationList<List<AppointmentReadModel>> query = await _AppointmentRepository.GetAllAppointments();
                return Ok(query);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove um consulta por id
        /// </summary>
        /// <returns>Remove uma consulta pelo seu identificador</returns>
        /// <response code="204">204 Retorno com sucesso</response>
        /// <response code="400">400 se houver falha na requisição</response>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAppintmentByIdAsync(Guid Id)
        {
            try
            {
                Appointment findAppointment = await _genericRepository.GetByIdAsync(Id);

                if (findAppointment == null)
                {
                    return BadRequest(new
                    {
                        message = "Consulta não encontrada ou não existe",
                        Id
                    });
                }

                await _genericRepository.Delete(findAppointment);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}