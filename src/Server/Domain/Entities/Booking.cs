using System;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Booking : IEntity
    {
        public string Id {get; set;}        
        public string UserId {get; set;}
        public string SpaceId {get; set;}
        public string Status {get;set;}
        public string TestType {get;set;}
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

    }
}