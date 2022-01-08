using System;

namespace Domain.Entities
{
    public class Spaces
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public DateTime Date { get; set; }
        public bool Available { get; set; }
        public string CreatedAt { get; set; }
        public string UpdatedAt { get; set; }
    }
}