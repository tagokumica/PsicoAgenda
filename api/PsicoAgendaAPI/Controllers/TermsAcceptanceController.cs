using Microsoft.AspNetCore.Mvc;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermsAcceptanceController : ControllerBase
    {
        private readonly ITermsAcceptanceMapper _termsAcceptanceMapper;

        public TermsAcceptanceController(ITermsAcceptanceMapper termsAcceptanceMapper)
        {
            _termsAcceptanceMapper = termsAcceptanceMapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync(CancellationToken ct)
        {
            var result = await _termsAcceptanceMapper.ListAsync(null, ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var result = await _termsAcceptanceMapper.GetByIdAsync(id, ct);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] TermsAcceptanceViewModel termsAcceptanceViewModel, CancellationToken ct)
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
            await _termsAcceptanceMapper.AddAsync(termsAcceptanceViewModel, ct);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = termsAcceptanceViewModel.Id }, termsAcceptanceViewModel);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] TermsAcceptanceViewModel termsAcceptanceViewModel)
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

            _termsAcceptanceMapper.Update(termsAcceptanceViewModel);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken ct)
        {
            var current = await _termsAcceptanceMapper.GetByIdAsync(id, ct);

            _termsAcceptanceMapper.Delete(current, ct);
            return NoContent();
        }
    }
}
