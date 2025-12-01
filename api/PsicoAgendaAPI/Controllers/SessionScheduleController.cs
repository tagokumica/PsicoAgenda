using Microsoft.AspNetCore.Mvc;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionScheduleController : ControllerBase
    {
        private readonly ISessionScheduleMapper _sessionScheduleMapper;

        public SessionScheduleController(ISessionScheduleMapper sessionScheduleMapper)
        {
            _sessionScheduleMapper = sessionScheduleMapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync(CancellationToken ct)
        {
            var result = await _sessionScheduleMapper.ListAsync(null, ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var result = await _sessionScheduleMapper.GetByIdAsync(id, ct);

            return Ok(result);
        }

        [HttpGet("session-notes/{sessionScheduleId:guid}")]
        public async Task<IActionResult> GetSessionScheduleBySessionNotesAsync(Guid sessionScheduleId, CancellationToken ct)
        {
            var result = await _sessionScheduleMapper.GetSessionScheduleBySessionNotesAsync(sessionScheduleId, ct);
            return Ok(result);
        }

        [HttpGet("waits/{sessionScheduleId:guid}")]
        public async Task<IActionResult> GetSessionScheduleByWaitsAsync(Guid sessionScheduleId, CancellationToken ct)
        {
            var result = await _sessionScheduleMapper.GetSessionScheduleByWaitsAsync(sessionScheduleId, ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] SessionScheduleViewModel sessionScheduleViewModel, CancellationToken ct)
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
            await _sessionScheduleMapper.AddAsync(sessionScheduleViewModel, ct);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = sessionScheduleViewModel.Id }, sessionScheduleViewModel);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] SessionScheduleViewModel sessionScheduleViewModel)
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

            _sessionScheduleMapper.Update(sessionScheduleViewModel);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken ct)
        {
            var current = await _sessionScheduleMapper.GetByIdAsync(id, ct);

            _sessionScheduleMapper.Delete(current, ct);
            return NoContent();
        }
    }
}
