using System;
using System.Collections.Generic;

namespace ClaimsManagement.Domain.Entities
{
    public class Claim
    {
        public int Id { get; set; }
        public int InsuredId { get; set; } // Foreign key to the Insured entity
        public Insured Insured { get; set; } = null!; // Initialized to avoid nullability warnings
        public string ClaimType { get; set; } = string.Empty; // Type of claim
        public string IncidentDescription { get; set; } = string.Empty; // Description of the incident
        public DateTime IncidentDate { get; set; } // Date of the incident
        public float EstimatedAmount { get; set; } // Estimated claim amount
        public List<string> DocumentUrls { get; set; } = new(); // URLs of associated documents
        public ClaimStatus Status { get; set; } = ClaimStatus.Submitted; // Initial claim status
    }

    public enum ClaimStatus
    {
        Submitted,
        UnderReview,
        Approved,
        Rejected
    }
}