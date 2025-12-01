using Microsoft.AspNetCore.Mvc;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCareProfessionalController : ControllerBase
    {
        private readonly IHealthCareProfissionalMapper _healthCareProfessionalMapper;

        public HealthCareProfessionalController(IHealthCareProfissionalMapper healthCareProfessionalMapper)
        {
            _healthCareProfessionalMapper = healthCareProfessionalMapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync(CancellationToken ct)
        {
            var result = await _healthCareProfessionalMapper.ListAsync(null, ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var result = await _healthCareProfessionalMapper.GetByIdAsync(id, ct);

            return Ok(result);
        }

        [HttpGet("availabilities/{ProfessionalId:guid}")]
        public async Task<IActionResult> GetHealthCareProfissionalByAvailabilitiesAsync(Guid professionalId, CancellationToken ct)
        {
            var result = await _healthCareProfessionalMapper.GetHealthCareProfissionalByAvailabilitiesAsync(professionalId, ct);
            return Ok(result);
        }

        [HttpGet("session-notes/{ProfessionalId:guid}")]
        public async Task<IActionResult> GetHealthCareProfissionalBySessionNotesAsync(Guid professionalId, CancellationToken ct)
        {
            var result = await _healthCareProfessionalMapper.GetHealthCareProfissionalBySessionNotesAsync(professionalId, ct);
            return Ok(result);
        }

        [HttpGet("session-schedule/{ProfessionalId:guid}")]
        public async Task<IActionResult> GetHealthCareProfissionalBySessionScheduleAsync(Guid professionalId, CancellationToken ct)
        {
            var result = await _healthCareProfessionalMapper.GetHealthCareProfissionalBySessionScheduleAsync(professionalId, ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] HealthCareProfissionalViewModel healthCareProfissionalViewModel, CancellationToken ct)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new
                    {
                        Field = x.Key,
                        Error = x.Value.Errors.First().ErrorMessage
                    });
                return BadRequest(errors);
            }
            await _healthCareProfessionalMapper.AddAsync(healthCareProfissionalViewModel, ct);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = healthCareProfissionalViewModel.Id }, healthCareProfissionalViewModel);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] HealthCareProfissionalViewModel healthCareProfissionalViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new
                    {
                        Field = x.Key,
                        Error = x.Value.Errors.First().ErrorMessage
                    });
                return BadRequest(errors);
            }
            _healthCareProfessionalMapper.Update(healthCareProfissionalViewModel);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken ct)
        {
            var current = await _healthCareProfessionalMapper.GetByIdAsync(id, ct);

            _healthCareProfessionalMapper.Delete(current, ct);
            return NoContent();
        }
    }
}
