namespace Domain.Entities
{
    public class Space
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public string Date { get; set; }
        public bool Available { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}