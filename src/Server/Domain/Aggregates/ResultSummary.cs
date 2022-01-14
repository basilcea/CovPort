using Domain.Entities;

namespace Domain.Aggregates
{
    public class ResultSummary
    {
        public string LocationName {get;set;}
        public int BookingCapacity {get;set;}
        public int Bookings {get;set;}
        public int Tests {get;set;}
        public int PositiveCases {get;set;}
        public int NegativeCases {get;set;}
        public int AwaitingResult {get;set;}
    }
}