using ClaimsManagement.Domain.Entities;
using System.Threading.Tasks;

namespace ClaimsManagement.Domain.Interfaces
{
    public interface IInsuredRepository
    {
        /// <summary>
        /// Retrieves an insured by their ID.
        /// </summary>
        Task<Insured?> GetByIdAsync(int id); // Nullable to handle cases where no insured is found

        /// <summary>
        /// Retrieves an insured by their email.
        /// </summary>
        Task<Insured?> GetByEmailAsync(string email); // Nullable to handle cases where no insured is found

        /// <summary>
        /// Adds a new insured to the database.
        /// </summary>
        Task AddAsync(Insured insured);

        /// <summary>
        /// Checks if an insured exists with the given email.
        /// </summary>
        Task<bool> ExistsAsync(string email);
    }
}
