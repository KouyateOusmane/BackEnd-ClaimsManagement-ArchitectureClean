using ClaimsManagement.Domain.Entities;

namespace ClaimsManagement.Domain.Interfaces
{
    public interface IClaimRepository
    {
        /// <summary>
        /// Adds a new claim.
        /// </summary>
        Task<Claim> AddAsync(Claim claim);

        /// <summary>
        /// Retrieves a claim by its ID.
        /// </summary>
        Task<Claim?> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves all claims submitted by a specific user (insured).
        /// </summary>
        Task<List<Claim>> GetByUserIdAsync(int userId);

        /// <summary>
        /// Updates an existing claim.
        /// </summary>
        Task UpdateAsync(Claim claim);

        /// <summary>
        /// Get all claims for insured.
        /// </summary>
        Task<List<Claim>> GetByInsuredIdAsync(int insuredId);

        /// <summary>
        /// Change claim status.
        /// </summary>
        Task UpdateStatusAsync(int claimId, int newStatus);
    }
}
