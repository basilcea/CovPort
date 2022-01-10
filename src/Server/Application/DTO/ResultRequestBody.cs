namespace Application.DTO
{
    public class ResultPostRequestBody
    {
        public string BookingId {get;set;}
        public string UserId {get;set;}
        public string TestType {get;set;}
        public string Status {get;set;} = "PENDING";
    }

    public class ResultPatchRequestBody
    {
        public string Id {get;set;}
        public string Status {get; set;}
        public string Positive {get;set;}
    
    }
}