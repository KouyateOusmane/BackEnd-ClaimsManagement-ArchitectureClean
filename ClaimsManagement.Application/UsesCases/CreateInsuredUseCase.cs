using ClaimsManagement.Domain.Entities;
using ClaimsManagement.Domain.Interfaces;

namespace ClaimsManagement.Application.UseCases
{
    public class CreateInsuredUseCase
    {
        private readonly IInsuredRepository _insuredRepository;

        public CreateInsuredUseCase(IInsuredRepository insuredRepository)
        {
            _insuredRepository = insuredRepository;
        }

        public async Task ExecuteAsync(string name, string email, string password, string phone)
        {
            if (await _insuredRepository.ExistsAsync(email))
                throw new Exception("An insured with this email already exists.");

            var insured = new Insured
            {
                Name = name,
                Email = email,
                Password = password, // Ideally, hash the password here
                Phone = phone
            };

            await _insuredRepository.AddAsync(insured);
        }
    }
}
