using System.ComponentModel.DataAnnotations;

namespace viecLam24hBE.Models
{
    public class JobType
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public virtual ICollection<JobPost>? JobPosts { get; set; }
    }
}
