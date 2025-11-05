using Domain.Notifiers;
using Microsoft.AspNetCore.Mvc;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : NotifierController
    {
        private readonly IPatientMapper _patientMapper;

        public PatientController(IPatientMapper patientMapper, INotifier notifier)
        : base(notifier)
        {
            _patientMapper = patientMapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync(CancellationToken ct)
        {
            var result = await _patientMapper.ListAsync(null, ct);
            return Ok(result);
        }
        
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var result = await _patientMapper.GetByIdAsync(id, ct);

            return Ok(result);
        }

        [HttpGet("consent/{patientId:guid}")]
        public async Task<IActionResult> GetPatientByConsentAsync(Guid patientId, CancellationToken ct)
        {
            var result = await _patientMapper.GetPatientByConsentAsync(patientId, ct);
            return Ok(result);
        }

        [HttpGet("availabilities/{patientId:guid}")]
        public async Task<IActionResult> GetPatientByAvailabilitiesAsync(Guid patientId, CancellationToken ct)
        {
            var result = await _patientMapper.GetPatientByAvailabilitiesAsync(patientId, ct);
            return Ok(result);
        }

        [HttpGet("waits/{patientId:guid}")]
        public async Task<IActionResult> GetPatientByWaitsAsync(Guid patientId, CancellationToken ct)
        {
            var result = await _patientMapper.GetPatientByWaitsAsync(patientId, ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] PatientViewModel patientViewModel, CancellationToken ct)
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
            await _patientMapper.AddAsync(patientViewModel, ct);
            return CustomResponse(patientViewModel);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] PatientViewModel patientViewModel)
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

            _patientMapper.Update(patientViewModel);
            return CustomResponse(patientViewModel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken ct)
        {
            var current = await _patientMapper.GetByIdAsync(id, ct);

            _patientMapper.Delete(current, ct);
            return NoContent();
        }
    }
}
