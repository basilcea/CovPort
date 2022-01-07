namespace Domain.Entities
{
    public class Booking
    {
        public string Id {get; set;}
        public int UserId {get; set;}
        public int SpaceId {get; set;}
        public string Status {get;set;}
        public string TestType {get;set;}
        public DateTime CreatedAt{get; set;}
        public DateTime UpdatedAt{get;set;}

    }
}