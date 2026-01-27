using BeyondWPF.Common.ViewModels;
using CommunityToolkit.Mvvm.Input;

namespace BeyondWPF.BaseApplication.ViewModels;

/// <summary>
/// Example ViewModel demonstrating asynchronous operation tracking.
/// </summary>
public partial class AsyncExampleViewModel : AsyncViewModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncExampleViewModel"/> class.
    /// </summary>
    public AsyncExampleViewModel()
    {
        Title = "Async Example";
    }

    /// <summary>
    /// Simulates a long-running task.
    /// </summary>
    [RelayCommand]
    private async Task SimulateTaskAsync()
    {
        await PerformAsync(Task.Delay(2000));
    }

    /// <summary>
    /// Simulates multiple concurrent long-running tasks.
    /// </summary>
    [RelayCommand]
    private async Task SimulateMultipleTasksAsync()
    {
        var task1 = PerformAsync(Task.Delay(3000));
        var task2 = PerformAsync(Task.Delay(1000));
        var task3 = PerformAsync(Task.Delay(5000), showGlobalLoader: true, statusMessage: "Synchronisation des données en cours...");

        await Task.WhenAll(task1, task2, task3);
    }
}
