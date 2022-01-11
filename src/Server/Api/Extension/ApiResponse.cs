using System.Collections.Generic;
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
        public ApiResponse(string errorMessage)
        {
            Success = false;
            Error = errorMessage;
        }

         public ApiResponse(bool success, string message, List<string> errors)
        {
            Message = message;
            Success = success;
            Errors = errors;
        }
 
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Message { get; set; }
        public bool Success { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public object Data { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<string> Errors { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
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
