using ClaimsManagement.Domain.Entities;
using ClaimsManagement.Domain.Interfaces;
using ClaimsManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ClaimsManagement.Infrastructure.Repositories
{
    public class InsuredRepository : IInsuredRepository
    {
        private readonly ApplicationDbContext _context;

        public InsuredRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves an insured by their ID.
        /// </summary>
        public async Task<Insured?> GetByIdAsync(int id)
        {
            return await _context.Insureds
                .Include(i => i.Claims) // Include related claims
                .FirstOrDefaultAsync(i => i.Id == id); // Return null if no match is found
        }

        /// <summary>
        /// Retrieves an insured by their email.
        /// </summary>
        public async Task<Insured?> GetByEmailAsync(string email)
        {
            return await _context.Insureds
                .FirstOrDefaultAsync(i => i.Email == email); // Return null if no match is found
        }

        /// <summary>
        /// Adds a new insured to the database.
        /// </summary>
        public async Task AddAsync(Insured insured)
        {
            _context.Insureds.Add(insured);
            await _context.SaveChangesAsync(); // Save changes to the database
        }

        /// <summary>
        /// Checks if an insured exists with the given email.
        /// </summary>
        public async Task<bool> ExistsAsync(string email)
        {
            return await _context.Insureds.AnyAsync(i => i.Email == email); // Check if email exists
        }
    }
}
