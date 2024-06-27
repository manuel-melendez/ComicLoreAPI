namespace ComicLoreApi.Models
{
    public class SupeWithPowersDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Alias { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Origin { get; set; }
        public IEnumerable<string> Powers { get; set; }
    }
}
