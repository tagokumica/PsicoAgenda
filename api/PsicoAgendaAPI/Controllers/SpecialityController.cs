using Microsoft.AspNetCore.Mvc;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialityController : ControllerBase
    {
        private readonly ISpecialityMapper _specialityMapper;

        public SpecialityController(ISpecialityMapper specialityMapper)
        {
            _specialityMapper = specialityMapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync(CancellationToken ct)
        {
            var result = await _specialityMapper.ListAsync(null, ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var result = await _specialityMapper.GetByIdAsync(id, ct);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] SpecialityViewModel specialityViewModel, CancellationToken ct)
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
            await _specialityMapper.AddAsync(specialityViewModel, ct);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = specialityViewModel.Id }, specialityViewModel);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] SpecialityViewModel specialityViewModel)
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

            _specialityMapper.Update(specialityViewModel);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken ct)
        {
            var current = await _specialityMapper.GetByIdAsync(id, ct);
            if (current is null)
                return NotFound($"Especialidade {id} não encontrada.");

            _specialityMapper.Delete(current, ct);
            return NoContent();
        }
    }
}
