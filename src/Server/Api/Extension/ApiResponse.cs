using Newtonsoft.Json;

namespace Api.Extension
{
    public class ApiResponse<T> 
    {
        public ApiResponse(object data)
        {
            Data = data;
            Success = true;
        }

        [JsonConstructor]
        public ApiResponse( string errorMessage)
        {
            Success = false;
            Error = errorMessage;
        }

        public string Message { get; set; }
        public bool Success { get; set; }
        public object Data { get; set; }
        public string Error { get; set; }

        public static ApiResponse<T> FromData(object data)
        {
            return new ApiResponse<T>(data);
        }

        public static object WithError(string errorMessage)
        {
            
            return new ApiResponse<T>(errorMessage);
        }


    }
}
