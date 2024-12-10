using ClaimsManagement.Application.UseCases;
using ClaimsManagement.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ClaimsManagement.API.Controllers
{
    [ApiController]
    [Route("/")]
    public class ClaimsController : ControllerBase
    {
        private readonly SubmitClaimUseCase _submitClaimUseCase;
        private readonly GetClaimByIdUseCase _getClaimByIdUseCase;

        public ClaimsController(SubmitClaimUseCase submitClaimUseCase, GetClaimByIdUseCase getClaimByIdUseCase)
        {
            _submitClaimUseCase = submitClaimUseCase;
            _getClaimByIdUseCase = getClaimByIdUseCase;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("API is working!");
        }

        [HttpPost]
        public async Task<IActionResult> SubmitClaim([FromBody] Claim claim)
        {
            if (claim.IncidentDate > DateTime.UtcNow)
                return BadRequest("Incident date cannot be in the future.");

            var result = await _submitClaimUseCase.ExecuteAsync(claim);
            return CreatedAtAction(nameof(GetClaimById), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClaimById(Guid id)
        {
            var result = await _getClaimByIdUseCase.ExecuteAsync(id);
            return Ok(result);
        }
    }
}
