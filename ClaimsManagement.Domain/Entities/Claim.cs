namespace ClaimsManagement.Domain.Entities
{
    public class Claim
    {
        public Guid Id { get; set; }
        public string? ClaimType { get; set; } // Type de réclamation (automobile, habitation)
        public string IncidentDescription { get; set; } = string.Empty; // Description obligatoire
        public DateTime IncidentDate { get; set; }
        public float EstimatedAmount { get; set; }
        public List<string> DocumentUrls { get; set; } = new(); // Liste de documents
        public ClaimStatus Status { get; set; } = ClaimStatus.Submitted; // Statut initial
    }

    public enum ClaimStatus
    {
        Submitted,
        UnderReview,
        Approved,
        Rejected
    }
}
