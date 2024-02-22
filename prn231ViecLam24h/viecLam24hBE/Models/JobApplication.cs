using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace viecLam24hBE.Models
{
    public class JobApplication
    {
        [Key]
        public int Id { get; set; }
        public int? ApplicantId { get; set; }
        public int? JobPostId { get; set; }
        public bool? Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? ProfileName { get; set; }
        public bool? IsSave { get; set; }

        [ForeignKey("JobPostId")]
        public virtual JobPost? JobPost { get; set; }

        [ForeignKey("ApplicantId")]
        public virtual ApplicantProfile? ApplicantProfile { get; set; }
    }
}
