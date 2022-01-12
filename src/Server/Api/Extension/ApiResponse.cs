using System.Collections.Generic;
using Newtonsoft.Json;

namespace Api.Extension
{
    public class ApiResponse<T>
    {
        public ApiResponse(object data, string message)
        {
            Data = data;
            Success = true;
            Message = message;
        }

        [JsonConstructor]
        public ApiResponse(  List<string> errorMessage, string message)
        {
            Success = false;
            Errors = errorMessage;
            Message = message;
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

  
    
        public static ApiResponse<T> FromData(object data, string message)
        {
            return new ApiResponse<T>(data, message);
        }

        public static object WithError( string errorMessage, string defaultMessage)
        {
            var errorMessages = new List<string>{errorMessage};

            return new ApiResponse<T>(errorMessages, defaultMessage);
        }

    }
}
