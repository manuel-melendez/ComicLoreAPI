using System.ComponentModel.DataAnnotations;

namespace ComicLoreApi.Entities
{
    public class Power
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public Tier PowerTier { get; set; }
        // Navigation property for many-to-many relationship with supes
        public ICollection<Supe> Supes { get; set; } = new List<Supe>();
    }

    public enum Tier
    {
        S,
        A,
        B,
        C,
        D,
        E,
        F
    }
}
