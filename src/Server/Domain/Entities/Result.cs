using System;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Result : IEntity
    {
        public int Id {get; set;}
        public int BookingId {get;set;}
        public int UserId{get;set;}
        public string TestLocation {get;set;}
        public string TestType {get;set;}
        public string Status {get;set;}
        public bool? Positive {get;set;}
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

    }
}