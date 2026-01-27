using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using BeyondWPF.Common.Models;
using BeyondWPF.Common.Services;
using BeyondWPF.Common.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeyondWPF.BaseApplication.ViewModels;

/// <summary>
/// ViewModel for the Debug Log Console.
/// </summary>
public partial class LogConsoleViewModel : BaseViewModel
{
    private readonly LogService _logService;

    [ObservableProperty]
    private bool _isExpanded;

    [ObservableProperty]
    private LogLevel? _filterLevel;

    /// <summary>
    /// Gets the collection of logs to display, filtered if necessary.
    /// </summary>
    public ICollectionView LogView { get; }

    /// <summary>
    /// Gets the latest log entry from the service.
    /// </summary>
    public LogEntry? LatestLog => _logService.LatestLog;

    public LogConsoleViewModel()
    {
        _logService = LogService.Instance;
        _logService.PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(LogService.LatestLog))
            {
                OnPropertyChanged(nameof(LatestLog));
            }
        };

        // Initialize filtering
        LogView = CollectionViewSource.GetDefaultView(_logService.Logs);
        LogView.Filter = FilterLog;
    }

    private bool FilterLog(object obj)
    {
        if (FilterLevel == null || obj is not LogEntry entry)
            return true;

        return entry.Level == FilterLevel;
    }

    [RelayCommand]
    private void ToggleExpanded()
    {
        IsExpanded = !IsExpanded;
    }

    [RelayCommand]
    private void SetFilter(LogLevel? level)
    {
        FilterLevel = level;
        LogView.Refresh();
    }

    [RelayCommand]
    private void ClearLogs()
    {
        _logService.Clear();
    }

    partial void OnFilterLevelChanged(LogLevel? value)
    {
        LogView.Refresh();
    }
}
