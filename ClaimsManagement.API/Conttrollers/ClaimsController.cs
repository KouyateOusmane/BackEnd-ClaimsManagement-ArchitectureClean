using ClaimsManagement.Application.UseCases;
using ClaimsManagement.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ClaimsManagement.API.Controllers
{
    [ApiController]
    [Route("api/claims")]
    public class ClaimsController : ControllerBase
    {
        private readonly SubmitClaimUseCase _submitClaimUseCase;
        private readonly GetClaimByIdUseCase _getClaimByIdUseCase;

        public ClaimsController(SubmitClaimUseCase submitClaimUseCase, GetClaimByIdUseCase getClaimByIdUseCase)
        {
            _submitClaimUseCase = submitClaimUseCase;
            _getClaimByIdUseCase = getClaimByIdUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> SubmitClaim([FromBody] SubmitClaimRequest request)
        {
            if (request.IncidentDate > DateTime.UtcNow)
                return BadRequest(new { Error = "Incident date cannot be in the future." });

            try
            {
                var result = await _submitClaimUseCase.ExecuteAsync(request.InsuredId, request.ClaimType, request.IncidentDescription, request.IncidentDate, request.EstimatedAmount, request.DocumentUrls);
                return CreatedAtAction(nameof(GetClaimById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetClaimById(int id)
        {
            var result = await _getClaimByIdUseCase.ExecuteAsync(id);
            if (result == null)
                return NotFound(new { Error = "Claim not found." });

            return Ok(result);
        }
    }

    public class SubmitClaimRequest
    {
        public int InsuredId { get; set; }
        public string ClaimType { get; set; } = string.Empty;
        public string IncidentDescription { get; set; } = string.Empty;
        public DateTime IncidentDate { get; set; }
        public float EstimatedAmount { get; set; }
        public List<string> DocumentUrls { get; set; } = new();
    }
}
