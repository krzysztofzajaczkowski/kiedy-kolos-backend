namespace KiedyKolos.Core.Result
{
    public enum ErrorType
    {
        Unknown = 400,
        NotValid = 400,
        NotAuthenticated = 401,
        NotAuthorized = 403,
        NotFound = 404
    }
}