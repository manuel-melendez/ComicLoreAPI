using ComicLoreApi.Entities;
using System.ComponentModel.DataAnnotations;

namespace ComicLoreApi.Models
{
    public class SupeForUpdateDto
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
    }
}
