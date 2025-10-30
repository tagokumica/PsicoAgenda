using Microsoft.AspNetCore.Mvc;
using PsicoAgendaAPI.AutoMapper.Mapper.Interface;
using PsicoAgendaAPI.ViewModels;

namespace PsicoAgendaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressMapper _addressMapper;

        public AddressController(IAddressMapper addressMapper)
        {
            _addressMapper = addressMapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync(CancellationToken ct)
        {
            var result = await _addressMapper.ListAsync(null, ct);
            return Ok(result);
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var result = await _addressMapper.GetByIdAsync(id, ct);
            return Ok(result);
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<IActionResult> GetAddressesByUserAsync(Guid userId, CancellationToken ct)
        {
            var result = await _addressMapper.GetAddressesByUserAsync(userId, ct);
            return Ok(result);
        }

        [HttpGet("location/{locationId:guid}")]
        public async Task<IActionResult> GetAddressByLocationAsync(Guid locationId, CancellationToken ct)
        {
            var result = await _addressMapper.GetAddressByLocationAsync(locationId, ct);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] AddressViewModel addressViewModel, CancellationToken ct)
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
           
            await _addressMapper.AddAsync(addressViewModel, ct);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = addressViewModel.Id }, addressViewModel);
        }

      
        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] AddressViewModel addressViewModel)
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
            if (addressViewModel.Id != id)
                return BadRequest("Id inválido ou dados inconsistentes.");
            
            _addressMapper.Update(addressViewModel);
            return NoContent();
        }


        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken ct)
        {
            var address = await _addressMapper.GetByIdAsync(id, ct);

            _addressMapper.Delete(address, ct);
            return NoContent();
        }
    }
}
