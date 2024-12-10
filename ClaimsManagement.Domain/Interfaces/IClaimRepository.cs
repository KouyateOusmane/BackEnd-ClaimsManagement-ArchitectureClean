using ClaimsManagement.Domain.Entities;

namespace ClaimsManagement.Domain.Interfaces
{
    public interface IClaimRepository
    {
        Task<Claim> AddAsync(Claim claim);
        Task<Claim?> GetByIdAsync(Guid id);
        Task<List<Claim>> GetByUserIdAsync(Guid userId);
        Task UpdateAsync(Claim claim);
    }
}
