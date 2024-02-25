using AutoMapper;
using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.DTOs.Read;
using HealthSystem.Application.Validators;
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
        private readonly AppointmentValidators _validator;
        private readonly GenericRepository<Patient> _genericRepositoryPatient;
        private readonly AppointmentRepository _AppointmentRepository;

        public AppointmentController(PatientsContext patientsContext, IMapper mapper, AppointmentValidators validator)
        {
            _genericRepository = new GenericRepository<Appointment>(patientsContext);
            _genericRepositoryPatient = new GenericRepository<Patient>(patientsContext);

            _AppointmentRepository = new AppointmentRepository(patientsContext, mapper);
            _validator = validator;
        }

        /// <summary>
        /// Agendar nova consulta
        /// </summary>
        /// <returns>Agendar consulta</returns>
        /// <response code="200">200 Para sucesso ao agendar consulta</response>
        /// <response code="400">400 se houver falha na requisição</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AppointmentCreateModel))]
        [HttpPost("{PatientId}")]
        public async Task<IActionResult> CreateAppointmentAsync(Guid PatientId, [FromBody] AppointmentCreateModel model)
        {
            try
            {
                Patient patient = await _genericRepositoryPatient.GetByIdAsync(PatientId);
                var appointments = await _genericRepository.GetAll();
                var listAppointmentNotCanceled = appointments.Where(x => x.IsCanceled == false).ToList();

                var errors = _validator.ValidateCreateModel(PatientId, model, patient, listAppointmentNotCanceled);

                if (errors != null && errors.Count >= 0)
                {
                    return BadRequest(errors);
                }

                await _AppointmentRepository.AddAppointmentAsync(model, PatientId);
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{Id}/Cancel")]
        public async Task<IActionResult> CancelAppointmentsAsync(Guid Id)
        {
            try
            {
                Appointment appointment = await _genericRepository.GetByIdAsync(Id);
                var errors = _validator.ValidateCancelAppointment(appointment, Id);

                if (errors.Count > 0)
                {
                    return BadRequest(errors);
                }

                await _AppointmentRepository.CancelAppointmentAsync(appointment);
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
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(FeedbackCreateModel))]
        [HttpPost("{Id}/Feedback")]
        public async Task<IActionResult> AddFeedbackAsync(Guid Id, [FromBody] FeedbackCreateModel model)
        {
            try
            {
                Appointment appointment = await _genericRepository.GetByIdAsync(Id);
                var errors = _validator.ValidateCreateFeedback(appointment, Id);

                if (errors.Count > 0)
                {
                    return BadRequest(errors);
                }

                await _AppointmentRepository.AddFeedbackByIdAsync(appointment, model.FeedbackMessage);
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationList<List<AppointmentReadModel>>))]
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
        /// Confirmar presença
        /// </summary>
        /// <returns>Confirmar presença pendente</returns>
        /// <response code="204">204 Retorno dos dados com sucesso</response>
        /// <response code="400">400 se houver falha na requisição</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{Id}/ConfirmParticipation")]
        public async Task<IActionResult> ConfirmPresenceAppointmentAsync(Guid Id)
        {
            try
            {
                Appointment appointment = await _genericRepository.GetByIdAsync(Id);
                var errors = _validator.ValidateConfirmParticipation(appointment, Id);

                if (errors.Count > 0)
                {
                    return BadRequest(errors);
                }

                await _AppointmentRepository.ConfirmParticipationAsync(appointment);
                return NoContent();
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAppintmentByIdAsync(Guid Id)
        {
            try
            {
                Appointment appointment = await _genericRepository.GetByIdAsync(Id);
                var error = _validator.NotFoundAppointment(appointment, Id);
                if (error != null)
                {
                    return BadRequest(error);
                }

                await _genericRepository.Delete(appointment);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Finalizar consulta
        /// </summary>
        /// <returns>Marcar como concluída a consulta</returns>
        /// <response code="204">204 Retorno com sucesso</response>
        /// <response code="400">400 se houver falha na requisição</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("{Id}/Completed")]
        public async Task<IActionResult> CompletedAppintmentByIdAsync(Guid Id)
        {
            try
            {
                Appointment appointment = await _genericRepository.GetByIdAsync(Id);
                var errors = _validator.ValidateFinishedAppointment(appointment, Id);

                if (errors.Count > 0)
                {
                    return BadRequest(errors);
                }

                await _AppointmentRepository.CompletedAppointmentAsync(appointment);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}