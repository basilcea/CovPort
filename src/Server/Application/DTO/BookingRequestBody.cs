using System;

namespace Application.DTO
{
    public class BookingPostRequestBody
    {
        public string UserId { get; set; }
        public string SpaceId { get; set; }
        public string Status { get; set; }
        public string TestType { get; set; }

    }

    public class BookingPatchRequestBody
    {
        public string Id { get; set; }
        public string UserId {get;set;}
        public string Status { get; set; }

    }
}