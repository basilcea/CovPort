using System;

namespace Application.DTO
{
    public class BookingPostRequestBody
    {
        public int UserId { get; set; }
        public int SpaceId { get; set; }
        public string TestType { get; set; }

    }

      public class BookingPatchRequestBody
    {
        public int Id { get; set;}
        public int UserId {get;set;}
        public string Status { get; set; }

    }

    
}