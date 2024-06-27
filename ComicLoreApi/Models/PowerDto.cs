using ComicLoreApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace ComicLoreApi.Models
{
    public class PowerDto
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        public string PowerTier { get; set; }
    }
}