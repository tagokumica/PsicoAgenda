using Microsoft.AspNetCore.Mvc;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsentController : ControllerBase
    {
        private readonly IConsentMapper _consentMapper;

        public ConsentController(IConsentMapper consentMapper)
        {
            _consentMapper = consentMapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> ListAsync(CancellationToken ct)
        {
            var result = await _consentMapper.ListAsync(null, ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var result = await _consentMapper.GetByIdAsync(id, ct);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ConsentViewModel consentViewModel, CancellationToken ct)
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
            await _consentMapper.AddAsync(consentViewModel, ct);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = consentViewModel.Id }, consentViewModel);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] ConsentViewModel consentViewModel)
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
            _consentMapper.Update(consentViewModel);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken ct)
        {
            var current = await _consentMapper.GetByIdAsync(id, ct);

            _consentMapper.Delete(current, ct);
            return NoContent();
        }
    }
}
