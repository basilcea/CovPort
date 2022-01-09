using Domain.Entities;

namespace Domain.Aggregates
{
    public class ResultSummary
    {
        public string Location {get;set;}
        public string BookingCapacity {get;set;}
        public string Bookings {get;set;}
        public string Test {get;set;}
        public string PositiveCases {get;set;}
        public string NegativeCases {get;set;}
        public string AwaitingResult {get;set;}
    }
}