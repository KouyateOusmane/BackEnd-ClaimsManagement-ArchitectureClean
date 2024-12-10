using ClaimsManagement.Domain.Entities;
using ClaimsManagement.Domain.Interfaces;

namespace ClaimsManagement.Application.UseCases
{
    public class SubmitClaimUseCase
    {
        private readonly IClaimRepository _claimRepository;

        public SubmitClaimUseCase(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task<Claim> ExecuteAsync(Claim claim)
        {
            try
            {
                Console.WriteLine($"Received IncidentDate: {claim.IncidentDate}");
                Console.WriteLine($"Current DateTime.UtcNow: {DateTime.UtcNow}");

                if (claim.IncidentDate > DateTime.UtcNow)
                    throw new ArgumentException("Incident date cannot be in the future.");

                // Continuez avec le traitement
                claim.Status = ClaimStatus.Submitted;
                return await _claimRepository.AddAsync(claim);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SubmitClaimUseCase: {ex.Message}");
                throw;
            }
        }
    }
}
