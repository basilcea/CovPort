namespace Domain.Services
{
    public static class UserService
    {
        public static bool UserCreatedEntity(string userId, string entityUserId )
        {
            return userId == entityUserId;
        }
    }
}