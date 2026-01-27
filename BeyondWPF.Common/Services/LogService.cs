using System.Collections.ObjectModel;
using BeyondWPF.Common.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BeyondWPF.Common.Services;

/// <summary>
/// Service responsible for capturing and providing logs within the application.
/// </summary>
public partial class LogService : ObservableObject
{
    private static readonly Lazy<LogService> _instance = new(() => new LogService());

    /// <summary>
    /// Gets the singleton instance of the <see cref="LogService"/>.
    /// </summary>
    public static LogService Instance => _instance.Value;

    private readonly ObservableCollection<LogEntry> _logs = new();

    /// <summary>
    /// Gets the collection of logged entries.
    /// </summary>
    public ReadOnlyObservableCollection<LogEntry> Logs { get; }

    [ObservableProperty]
    private LogEntry? _latestLog;

    private LogService()
    {
        Logs = new ReadOnlyObservableCollection<LogEntry>(_logs);
    }

    /// <summary>
    /// Adds a log entry.
    /// </summary>
    public void Log(string message, LogLevel level = LogLevel.Info)
    {
        var entry = new LogEntry(DateTime.Now, message, level);
        
        // Ensure UI thread for collection updates
        if (System.Windows.Application.Current?.Dispatcher != null)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                _logs.Add(entry);
                LatestLog = entry;
            });
        }
        else
        {
            _logs.Add(entry);
            LatestLog = entry;
        }
    }

    public void Info(string message) => Log(message, LogLevel.Info);
    public void Debug(string message) => Log(message, LogLevel.Debug);
    public void Warning(string message) => Log(message, LogLevel.Warning);
    public void Error(string message) => Log(message, LogLevel.Error);
    public void Critical(string message) => Log(message, LogLevel.Critical);

    /// <summary>
    /// Clears all logs.
    /// </summary>
    public void Clear()
    {
        _logs.Clear();
        LatestLog = null;
    }
}
