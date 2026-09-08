namespace AirbnbClone.Application.Common.Abstractions;

public interface ILoggerAdapter<T>
{
    void LogInformation(string message, params object[] args);
    void LogWarning(string message, params object[] args);
    void LogError(string ex, string message, params object[] args);
}