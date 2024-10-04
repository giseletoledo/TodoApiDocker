namespace TodoApi.Models
{
    public class Note
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
