namespace Application.DTO
{
    public class PingResult
    {
        public PingResult(bool success)
        {
            Success = success;
        }

        public PingResult(string message)
        {
            Message = message;
        }

        public bool Success { get; }
        public string Message { get; }
    }
}