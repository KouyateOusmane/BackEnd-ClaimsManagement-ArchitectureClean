using ClaimsManagement.Domain.Entities;
using ClaimsManagement.Domain.Interfaces;

namespace ClaimsManagement.Application.UseCases
{
    public class GetClaimByIdUseCase
    {
        private readonly IClaimRepository _claimRepository;

        public GetClaimByIdUseCase(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task<Claim?> ExecuteAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("Claim ID is required.");
            return await _claimRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Claim not found.");
        }
    }
}
