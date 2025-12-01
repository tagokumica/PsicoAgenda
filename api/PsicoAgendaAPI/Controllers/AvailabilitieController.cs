using Microsoft.AspNetCore.Mvc;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilitieController : ControllerBase
    {
        private readonly IAvailabilitieMapper _availabilitieMapper;

        public AvailabilitieController(IAvailabilitieMapper availabilitieMapper)
        {
            _availabilitieMapper = availabilitieMapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync(CancellationToken ct)
        {
            var result = await _availabilitieMapper.ListAsync(null, ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var result = await _availabilitieMapper.GetByIdAsync(id, ct);

            return Ok(result);
        }

        [HttpGet("booked")]
        public async Task<IActionResult> IsBookedAsync(CancellationToken ct)
        {
            var result = await _availabilitieMapper.IsBookedAsync(ct);
            return Ok(result);
        }

        [HttpGet("not-booked")]
        public async Task<IActionResult> IsNotBookedAsync(CancellationToken ct)
        {
            var result = await _availabilitieMapper.IsNotBookedAsync(ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AvailabilitieViewModel availabilitieViewModel, CancellationToken ct)
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
            await _availabilitieMapper.AddAsync(availabilitieViewModel, ct);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = availabilitieViewModel.Id }, availabilitieViewModel);
        }


        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] AvailabilitieViewModel availabilitieViewModel)
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

            _availabilitieMapper.Update(availabilitieViewModel);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken ct)
        {
            var current = await _availabilitieMapper.GetByIdAsync(id, ct);

            _availabilitieMapper.Delete(current, ct);
            return NoContent();
        }
    }
}
