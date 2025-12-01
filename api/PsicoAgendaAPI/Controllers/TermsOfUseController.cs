using Microsoft.AspNetCore.Mvc;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TermsOfUseController : ControllerBase
    {
        private readonly ITermOfUseMapper _termOfUseMapper;

        public TermsOfUseController(ITermOfUseMapper termOfUseMapper)
        {
            _termOfUseMapper = termOfUseMapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync(CancellationToken ct)
        {
            var result = await _termOfUseMapper.ListAsync(null, ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var result = await _termOfUseMapper.GetByIdAsync(id, ct);

            return Ok(result);
        }

        [HttpGet("terms-acceptance/{termsOfUseId:guid}")]
        public async Task<IActionResult> GetTermsOfUseByTermsAcceptanceAsync(Guid termsOfUseId, CancellationToken ct)
        {
            var result = await _termOfUseMapper.GetTermsOfUseByTermsAcceptanceAsync(termsOfUseId, ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] TermsOfUseViewModel termsOfUseViewModel, CancellationToken ct)
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
            await _termOfUseMapper.AddAsync(termsOfUseViewModel, ct);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = termsOfUseViewModel.Id }, termsOfUseViewModel);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] TermsOfUseViewModel termsOfUseViewModel)
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

            _termOfUseMapper.Update(termsOfUseViewModel);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken ct)
        {
            var current = await _termOfUseMapper.GetByIdAsync(id, ct);

            _termOfUseMapper.Delete(current, ct);
            return NoContent();
        }
    }
}
