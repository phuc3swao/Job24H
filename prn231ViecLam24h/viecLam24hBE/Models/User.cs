using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace viecLam24hBE.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UserName { get; set; }
        public bool Active { get; set; }
        public int RoleId { get; set; }
        public string? Sex { get; set; }
        public DateTime? Dob { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public string? Phone { get; set; }
        public string? CompanyName { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }
        public virtual ICollection<JobPost>? JobPosts { get; set; }
        public virtual ICollection<ApplicantProfile>? ApplicantProfiles { get; set; }

    }
}
