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

        /// <summary>
        /// Executes the use case to retrieve a claim by its ID.
        /// </summary>
        public async Task<Claim> ExecuteAsync(int id)
        {
            if (id <= 0) throw new ArgumentException("Claim ID must be greater than zero.");

            var claim = await _claimRepository.GetByIdAsync(id);
            if (claim == null) throw new KeyNotFoundException("Claim not found.");

            return claim;
        }
    }
}
