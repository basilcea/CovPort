using System;

namespace Domain.Entities
{
    public class Results
    {
        public string Id {get; set;}
        public string BookingId {get;set;}
        public bool Positive {get;set;}
        public string TestType {get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}

    }
}