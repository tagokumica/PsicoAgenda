using Microsoft.AspNetCore.Mvc;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SessionNoteController : ControllerBase
    {
        private readonly ISessionNoteMapper _sessionNoteMapper;

        public SessionNoteController(ISessionNoteMapper sessionNoteMapper)
        {
            _sessionNoteMapper = sessionNoteMapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync(CancellationToken ct)
        {
            var result = await _sessionNoteMapper.ListAsync(null, ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var result = await _sessionNoteMapper.GetByIdAsync(id, ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] SessionNoteViewModel sessionNoteViewModel, CancellationToken ct)
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
            await _sessionNoteMapper.AddAsync(sessionNoteViewModel, ct);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = sessionNoteViewModel.Id }, sessionNoteViewModel);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] SessionNoteViewModel sessionNoteViewModel)
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

            _sessionNoteMapper.Update(sessionNoteViewModel);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken ct)
        {
            var current = await _sessionNoteMapper.GetByIdAsync(id, ct);

            _sessionNoteMapper.Delete(current, ct);
            return NoContent();
        }
    }
}
