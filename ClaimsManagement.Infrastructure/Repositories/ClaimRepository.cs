using ClaimsManagement.Domain.Entities;
using ClaimsManagement.Domain.Interfaces;
using ClaimsManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsManagement.Infrastructure.Repositories
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly ApplicationDbContext _context;

        public ClaimRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Claim> AddAsync(Claim claim)
        {
            _context.Claims.Add(claim);
            await _context.SaveChangesAsync();
            return claim;
        }

        public async Task<Claim?> GetByIdAsync(Guid id)
        {
            return await _context.Claims.FindAsync(id);
        }

        public async Task<List<Claim>> GetByUserIdAsync(Guid userId)
        {
            // Exemple : Adaptez selon votre modèle
            return await _context.Claims
                .Where(c => c.Id == userId) // Changez selon votre logique métier
                .ToListAsync();
        }

        public async Task UpdateAsync(Claim claim)
        {
            _context.Claims.Update(claim);
            await _context.SaveChangesAsync();
        }
    }
}
