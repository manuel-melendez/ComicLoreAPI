using System.ComponentModel.DataAnnotations;

namespace ComicLoreApi.Entities
{
    public class Supe
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string Alias { get; set; }
        public DateTime DateOfBirth { get; set; }
        [StringLength(50)]
        public string Origin { get; set; }
        // Navigation property for many-to-many relationship with powers
        public ICollection<Power> Powers { get; set; } = new List<Power>();
    }
}
