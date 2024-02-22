using System.ComponentModel.DataAnnotations;

namespace viecLam24hBE.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
