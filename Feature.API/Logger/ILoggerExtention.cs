namespace Feature.API.Logger
{
    public interface ILoggerExtention
    {
        void LogError(string message);
        void LogError(string message, Exception exception);
        void LogInformation(string message);
    }
}
