using ClaimsManagement.Application.UseCases;
using Microsoft.AspNetCore.Mvc;
namespace ClaimsManagement.API.Controllers
{
    [ApiController]
    [Route("api/insureds")]
    public class InsuredController : ControllerBase
    {
        private readonly CreateInsuredUseCase _createInsuredUseCase;
        private readonly LoginUseCase _loginUseCase;
        private readonly GetClaimsByInsuredIdUseCase _getClaimsByInsuredIdUseCase;

        public InsuredController(CreateInsuredUseCase createInsuredUseCase, LoginUseCase loginUseCase, GetClaimsByInsuredIdUseCase getClaimsByInsuredIdUseCase)
        {
            _createInsuredUseCase = createInsuredUseCase;
            _loginUseCase = loginUseCase;
            _getClaimsByInsuredIdUseCase = getClaimsByInsuredIdUseCase;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateInsuredRequest request)
        {
            try
            {
                await _createInsuredUseCase.ExecuteAsync(request.Name, request.Email, request.Password, request.Phone);
                return Ok(new { Message = "Insured registered successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var insured = await _loginUseCase.ExecuteAsync(request.Email, request.Password);
                return Ok(new { Message = "Login successful.", InsuredId = insured.Id });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Error = ex.Message });
            }
        }

        [HttpGet("{insuredId}/claims")]
        public async Task<IActionResult> GetClaims(int insuredId)
        {
            var claims = await _getClaimsByInsuredIdUseCase.ExecuteAsync(insuredId);
            return Ok(claims);
        }
    }

    public class CreateInsuredRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
