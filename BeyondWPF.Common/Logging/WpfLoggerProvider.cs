using Microsoft.Extensions.Logging;
using BeyondWPF.Common.Services;

namespace BeyondWPF.Common.Logging;

/// <summary>
/// Provider for <see cref="WpfLogger"/>.
/// </summary>
public class WpfLoggerProvider : ILoggerProvider
{
    private readonly LogService _logService;

    public WpfLoggerProvider(LogService logService)
    {
        _logService = logService;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new WpfLogger(categoryName, _logService);
    }

    public void Dispose() { }
}
