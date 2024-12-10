using ClaimsManagement.Domain.Entities;
using ClaimsManagement.Domain.Interfaces;

namespace ClaimsManagement.Application.UseCases
{
    public class GetClaimsByInsuredIdUseCase
    {
        private readonly IClaimRepository _claimRepository;

        public GetClaimsByInsuredIdUseCase(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task<List<Claim>> ExecuteAsync(int insuredId)
        {
            return await _claimRepository.GetByInsuredIdAsync(insuredId);
        }
    }
}
