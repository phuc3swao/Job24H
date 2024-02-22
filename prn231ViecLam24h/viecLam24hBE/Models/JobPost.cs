using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace viecLam24hBE.Models
{
    public class JobPost
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int JobTypeId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string JobDescription { get; set; }
        public string JobLocation { get; set; }
        public bool Status { get; set; }
        public double Salary { get; set; }
        public string Experence { get; set; }
        public string JobName { get; set; }
        public string WorkingForm { get; set; }
        public string Degree { get; set; }
        public int MinAge { get; set; }
        public int MaxAge { get; set; }
        public string Sex { get; set; }
        public int Quantity { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime Deadline { get; set; }

        [ForeignKey("JobTypeId")]
        public virtual JobType? JobType { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        public virtual ICollection<JobApplication>? JobApplications { get; set; }

    }
}
