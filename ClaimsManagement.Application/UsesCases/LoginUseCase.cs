using ClaimsManagement.Domain.Entities;
using ClaimsManagement.Domain.Interfaces;

namespace ClaimsManagement.Application.UseCases
{
    public class LoginUseCase
    {
        private readonly IInsuredRepository _insuredRepository;

        public LoginUseCase(IInsuredRepository insuredRepository)
        {
            _insuredRepository = insuredRepository;
        }

        public async Task<Insured> ExecuteAsync(string email, string password)
        {
            var insured = await _insuredRepository.GetByEmailAsync(email);
            if (insured == null)
                throw new Exception("Email not found.");

            if (insured.Password != password) // Implement proper password hashing
                throw new Exception("Invalid password.");

            return insured;
        }
    }
}
