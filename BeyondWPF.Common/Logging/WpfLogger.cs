using Microsoft.Extensions.Logging;
using BeyondWPF.Common.Services;
using BeyondWPF.Common.Models;

namespace BeyondWPF.Common.Logging;

/// <summary>
/// A logger implementation that pipes logs to the <see cref="LogService"/>.
/// </summary>
public class WpfLogger : ILogger
{
    private readonly string _categoryName;
    private readonly LogService _logService;

    public WpfLogger(string categoryName, LogService logService)
    {
        _categoryName = categoryName;
        _logService = logService;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

    public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel) => true;

    public void Log<TState>(
        Microsoft.Extensions.Logging.LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel)) return;

        var message = formatter(state, exception);
        if (exception != null)
        {
            message += $"\n{exception}";
        }

        var level = MapLogLevel(logLevel);
        _logService.Log($"[{_categoryName}] {message}", level);
    }

    private Models.LogLevel MapLogLevel(Microsoft.Extensions.Logging.LogLevel logLevel)
    {
        return logLevel switch
        {
            Microsoft.Extensions.Logging.LogLevel.Trace => Models.LogLevel.Debug,
            Microsoft.Extensions.Logging.LogLevel.Debug => Models.LogLevel.Debug,
            Microsoft.Extensions.Logging.LogLevel.Information => Models.LogLevel.Info,
            Microsoft.Extensions.Logging.LogLevel.Warning => Models.LogLevel.Warning,
            Microsoft.Extensions.Logging.LogLevel.Error => Models.LogLevel.Error,
            Microsoft.Extensions.Logging.LogLevel.Critical => Models.LogLevel.Critical,
            _ => Models.LogLevel.Info
        };
    }
}
