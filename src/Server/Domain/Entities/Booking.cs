using System;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Booking : IEntity
    {
        public string Id {get; set;}
        public User User {get; set;}
        public Space Space {get; set;}
        public string Status {get;set;}
        public string TestType {get;set;}
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
       

    }
}