namespace LearnHubBackendDotNet.Exceptions
{
    public class UnauthorizedException: Exception
    {
        public UnauthorizedException(string message="Unauthorized Access") : base(message) { }

    }
}
