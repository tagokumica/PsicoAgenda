using Microsoft.AspNetCore.Mvc;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaitController : ControllerBase
    {
        private readonly IWaitMapper _waitMapper;

        public WaitController(IWaitMapper waitMapper)
        {
            _waitMapper = waitMapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync(CancellationToken ct)
        {
            var result = await _waitMapper.ListAsync(null, ct);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var result = await _waitMapper.GetByIdAsync(id, ct);

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] WaitViewModel waitViewModel, CancellationToken ct)
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
            await _waitMapper.AddAsync(waitViewModel, ct);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = waitViewModel.Id }, waitViewModel);
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] WaitViewModel waitViewModel)
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

            _waitMapper.Update(waitViewModel);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken ct)
        {
            var current = await _waitMapper.GetByIdAsync(id, ct);

            _waitMapper.Delete(current, ct);
            return NoContent();
        }
    }
}
