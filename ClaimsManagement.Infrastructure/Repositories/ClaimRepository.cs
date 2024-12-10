using ClaimsManagement.Domain.Entities;
using ClaimsManagement.Domain.Interfaces;
using ClaimsManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClaimsManagement.Infrastructure.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly ApplicationDbContext _context;

        public ClaimRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new claim to the database.
        /// </summary>
        public async Task<Claim> AddAsync(Claim claim)
        {
            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();
            return claim;
        }

        /// <summary>
        /// Gets a claim by its ID.
        /// </summary>
        public async Task<Claim?> GetByIdAsync(int id)
        {
            return await _context.Claims.FindAsync(id);
        }

        /// <summary>
        /// Gets all claims submitted by a specific user (insured).
        /// </summary>
        public async Task<List<Claim>> GetByUserIdAsync(int userId)
        {
            return await _context.Claims
                .Where(c => c.InsuredId == userId) // Adjusted to use InsuredId
                .ToListAsync();
        }

        /// <summary>
        /// Updates an existing claim.
        /// </summary>
        public async Task UpdateAsync(Claim claim)
        {
            _context.Claims.Update(claim);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Get all claims for insured.
        /// </summary>
        public async Task<List<Claim>> GetByInsuredIdAsync(int insuredId)
        {
            return await _context.Claims
                .Where(c => c.InsuredId == insuredId) // Filtre par l'ID de l'assuré
                .ToListAsync();
        }
    }
}
