using System;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Location : IEntity
    {
        public string Id {get; set;}
        public string Name {get; set;}
        public DateTime DateCreated { get; set; }
        
    }
}