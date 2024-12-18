using ClaimsManagement.Domain.Interfaces;

namespace ClaimsManagement.Application.UsesCases
{
    public class UpdateClaimStatusUseCase
    {
        private readonly IClaimRepository _claimRepository;

        public UpdateClaimStatusUseCase(IClaimRepository claimRepository)
        {
            _claimRepository = claimRepository;
        }

        public async Task ExecuteAsync(int claimId, int newStatus)
        {
            var claim = await _claimRepository.GetByIdAsync(claimId);

            if (claim == null)
                throw new ArgumentException("Réclamation introuvable");

            if (newStatus < 0 || newStatus > 3)
                throw new ArgumentException("Statut invalide");

            await _claimRepository.UpdateStatusAsync(claimId, newStatus);
        }
    }
}
