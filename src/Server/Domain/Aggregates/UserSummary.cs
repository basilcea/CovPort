using System.Collections.Generic;
using Domain.Entities;

namespace Domain.Aggregates
{
    public class UserSummary
    {
        public User User {get;set;}
        public IEnumerable<Result> Results {get;set;}
        public IEnumerable<Booking> Bookings {get;set;}
    }
}