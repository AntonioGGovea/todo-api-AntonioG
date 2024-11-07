namespace Todo.Services.Constants;

public static class ErrorCodes
{
    public static class Auth
    {
        public const string DuplicateUser = nameof(DuplicateUser);

        public const string UserDoesNotExist = nameof(UserDoesNotExist);

        public const string UserIsLockedOut = nameof(UserIsLockedOut);

        public const string AuthenticationFailed = nameof(AuthenticationFailed);
    }
}
