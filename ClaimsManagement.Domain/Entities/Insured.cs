using System.Collections.Generic;

namespace ClaimsManagement.Domain.Entities
{
    public class Insured
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public ICollection<Claim> Claims { get; set; } = new List<Claim>();
    }
}
