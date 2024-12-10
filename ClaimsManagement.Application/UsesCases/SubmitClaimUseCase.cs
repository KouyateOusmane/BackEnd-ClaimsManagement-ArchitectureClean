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

        public async Task<Claim> ExecuteAsync(int insuredId, string claimType, string incidentDescription, DateTime incidentDate, float estimatedAmount, List<string> documentUrls)
        {
            var claim = new Claim
            {
                InsuredId = insuredId,
                ClaimType = claimType,
                IncidentDescription = incidentDescription,
                IncidentDate = incidentDate,
                EstimatedAmount = estimatedAmount,
                DocumentUrls = documentUrls,
                Status = ClaimStatus.Submitted
            };

            return await _claimRepository.AddAsync(claim);
        }
    }
}
