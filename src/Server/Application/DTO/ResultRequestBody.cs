namespace Application.DTO
{
    public class ResultPostRequestBody
    {
        public int BookingId {get;set;}
        public int RequesterId {get;set;}
        public int UserId {get;set;}
        public string TestType {get;set;}
        public string TestLocation {get;set;}
        public string Status {get;set;} = "PENDING";
    }

    public class ResultPatchRequestBody
    {
        public int Id {get;set;}
        public int RequesterId {get;set;}
        public string Status {get; set;}
        public string Positive {get;set;}
    
    }
}