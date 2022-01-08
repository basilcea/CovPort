using System;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Space : IEntity
    {
        public string Id { get; set; }
        public int LocationId { get; set; }
        public DateTime Date { get; set; }
        public bool Available { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}