using System;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Space : IEntity
    {
        public int Id { get; set; }
        public string LocationName { get; set; }
        public DateTime Date { get; set; }
        public int SpacesCreated {get;set;}
        public bool Closed {get;set;} 
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}