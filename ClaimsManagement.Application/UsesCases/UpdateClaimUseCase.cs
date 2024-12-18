using ClaimsManagement.Domain.Interfaces;

namespace ClaimsManagement.Application.UsesCases
{
    public class UpdateClaimUseCase
    {
        private readonly IClaimRepository _claimRepository;

        public UpdateClaimUseCase(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task ExecuteAsync(int claimId, int insuredId, string claimType, string incidentDescription, DateTime incidentDate, float estimatedAmount, List<string> documentUrls)
        {
            var existingClaim = await _claimRepository.GetByIdAsync(claimId);

            if (existingClaim == null)
                throw new ArgumentException("Réclamation introuvable");

            if (existingClaim.InsuredId != insuredId)
                throw new UnauthorizedAccessException("Vous n'avez pas accès à cette réclamation.");

            existingClaim.ClaimType = claimType;
            existingClaim.IncidentDescription = incidentDescription;
            existingClaim.IncidentDate = incidentDate;
            existingClaim.EstimatedAmount = estimatedAmount;
            existingClaim.DocumentUrls = documentUrls;

            await _claimRepository.UpdateAsync(existingClaim);
        }
    }
}
