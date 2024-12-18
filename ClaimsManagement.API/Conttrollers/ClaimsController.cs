using ClaimsManagement.Application.UseCases;
using ClaimsManagement.Application.UsesCases;
using Microsoft.AspNetCore.Mvc;

namespace ClaimsManagement.API.Controllers
{
    [ApiController]
    [Route("api/claims")]
    public class ClaimsController : ControllerBase
    {
        private readonly SubmitClaimUseCase _submitClaimUseCase;
        private readonly GetClaimByIdUseCase _getClaimByIdUseCase;
        private readonly UpdateClaimUseCase _updateClaimUseCase;
        private readonly UpdateClaimStatusUseCase _updateClaimStatusUseCase;

        public ClaimsController(SubmitClaimUseCase submitClaimUseCase, GetClaimByIdUseCase getClaimByIdUseCase, UpdateClaimUseCase updateClaimUseCase, UpdateClaimStatusUseCase updateClaimStatusUseCase)
        {
            _submitClaimUseCase = submitClaimUseCase;
            _getClaimByIdUseCase = getClaimByIdUseCase;
            _updateClaimUseCase = updateClaimUseCase;
            _updateClaimStatusUseCase = updateClaimStatusUseCase;
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

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateClaim(int id, [FromBody] SubmitClaimRequest request)
        {
            try
            {
                await _updateClaimUseCase.ExecuteAsync(
                    id,
                    request.InsuredId,
                    request.ClaimType,
                    request.IncidentDescription,
                    request.IncidentDate,
                    request.EstimatedAmount,
                    request.DocumentUrls
                );

                return Ok(new { Message = "Réclamation mise à jour avec succès" });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { Message = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpPatch("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateClaimStatusRequest request)
        {
            try
            {
                await _updateClaimStatusUseCase.ExecuteAsync(id, request.Status);
                return Ok(new { Message = "Statut de la réclamation mis à jour avec succès." });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
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

    public class UpdateClaimStatusRequest
    {
        public int Status { get; set; }
    }
}
