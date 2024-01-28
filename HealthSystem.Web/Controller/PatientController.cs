using AutoMapper;
using HealthSystem.Application.DTOs.Create;
using HealthSystem.Application.DTOs.Update;
using HealthSystem.Domain.Entities;
using HealthSystem.Infrastructure.Data.Contexts;
using HealthSystem.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HealthSystem.Web.Controller
{
    [ApiController]
    [Route("[Controller]")]
    public class PatientController : ControllerBase
    {
        private readonly GenericRepository<Patient> _genericRepository;
        private readonly PatientRepository _patientRepository;

        public PatientController(PatientsContext patientsContext, IMapper mapper)
        {
            _genericRepository = new GenericRepository<Patient>(patientsContext);
            _patientRepository = new PatientRepository(patientsContext, mapper);
        }

        /// <summary>
        /// Lista todos os pacientes cadastrados
        /// </summary>
        /// <returns>Lista de pacientes</returns>
        /// <response code="200">Returns 200</response>
        /// <response code="400">Returns 400 if the query is invalid</response>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _genericRepository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Adiciona um novo paciente
        /// </summary>
        /// <returns>Adição de paciente</returns>
        /// <response code="201">Retorna 200 registro criado com sucesso</response>
        /// <response code="400">Returns 400 se a requisição falhar</response>
        [HttpPost]
        public async Task<IActionResult> CreatePatientAsync([FromBody] PatientCreateModel model)
        {
            try
            {
                await _patientRepository.AddPatientAsync(model);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualiza informações de um paciente pelo seu id
        /// </summary>
        /// <returns>Atualiza um paciente</returns>
        /// <response code="204">Returna 204 atualiza um paciente com sucesso</response>
        /// <response code="400">Returna 400 se a requisição falhar</response>
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdatePatientById(Guid Id, [FromBody] PatientUpdateModel updatedData)
        {
            try
            {
                Patient patient = await _genericRepository.GetByIdAsync(Id);
                if (patient == null)
                {
                    return BadRequest(new
                    {
                        message = "Paciente não encontrado ou não existe",
                        Id
                    });
                }

                _patientRepository.UpdatePatientAsync(patient, updatedData);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Recupera um paciente pelo seu id
        /// </summary>
        /// <returns>Recupera um paciente</returns>
        /// <response code="200">Retorna 200 ao recuperar um paciente</response>
        /// <response code="400">Returna 400 se a requisição falhar</response>
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetPatientById(Guid Id)
        {
            try
            {
                Patient patient = await _genericRepository.GetByIdAsync(Id);
                if (patient == null)
                {
                    return BadRequest(new
                    {
                        message = "Paciente não encontrado ou não existe",
                        Id
                    });
                }
                return Ok(patient);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Remove um paciente por id
        /// </summary>
        /// <returns>Remove de paciente</returns>
        /// <response code="204">Returna 204 ao remover um paciente</response>
        /// <response code="400">Returna 400 se a requisição falhar</response>
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeletePatientAsync(Guid Id)
        {
            try
            {
                Patient patient = await _genericRepository.GetByIdAsync(Id);
                if (patient == null)
                {
                    return BadRequest(new
                    {
                        message = "Paciente não encontrado ou não existe",
                        Id
                    });
                }

                _genericRepository.Delete(patient);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}