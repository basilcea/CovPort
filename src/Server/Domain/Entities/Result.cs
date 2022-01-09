using System;
using Domain.Interfaces;

namespace Domain.Entities
{
    public class Result : IEntity
    {
        public string Id {get; set;}
        public Booking Booking {get;set;}
        public User User {get;set;}
        public string TestType {get;set;}
        public string Status {get;set;}
        public bool Positive {get;set;}
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }

    }
}