using System.Runtime.CompilerServices;

namespace BeyondWPF.Common.ViewModels;

/// <summary>
/// A ViewModel that supports tracking asynchronous operations and managing a busy state.
/// </summary>
public abstract partial class AsyncViewModel : BaseViewModel
{
    private int _busyCount;

    /// <summary>
    /// Executes an asynchronous operation and automatically manages the <see cref="BaseViewModel.IsBusy"/> state.
    /// </summary>
    /// <param name="task">The task to execute.</param>
    /// <param name="showGlobalLoader">Whether to also trigger the global loading indicator.</param>
    /// <param name="statusMessage">Optional message to display in the global loader.</param>
    /// <returns>A task representing the operation.</returns>
    protected async Task PerformAsync(Task task, bool showGlobalLoader = false, string? statusMessage = null)
    {
        IncrementBusy();
        if (showGlobalLoader) LoadingService.Instance.ShowLoader(statusMessage);
        try
        {
            await task;
        }
        finally
        {
            if (showGlobalLoader) LoadingService.Instance.HideLoader();
            DecrementBusy();
        }
    }

    /// <summary>
    /// Executes an asynchronous operation that returns a result and automatically manages the <see cref="BaseViewModel.IsBusy"/> state.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="task">The task to execute.</param>
    /// <param name="showGlobalLoader">Whether to also trigger the global loading indicator.</param>
    /// <param name="statusMessage">Optional message to display in the global loader.</param>
    /// <returns>A task representing the operation and containing the result.</returns>
    protected async Task<T> PerformAsync<T>(Task<T> task, bool showGlobalLoader = false, string? statusMessage = null)
    {
        IncrementBusy();
        if (showGlobalLoader) LoadingService.Instance.ShowLoader(statusMessage);
        try
        {
            return await task;
        }
        finally
        {
            if (showGlobalLoader) LoadingService.Instance.HideLoader();
            DecrementBusy();
        }
    }

    /// <summary>
    /// Increments the busy counter. If the counter was 0, <see cref="BaseViewModel.IsBusy"/> is set to true.
    /// </summary>
    protected void IncrementBusy()
    {
        int count = Interlocked.Increment(ref _busyCount);
        if (count == 1)
        {
            IsBusy = true;
            OnPropertyChanged(nameof(IsBusy));
            System.Diagnostics.Debug.WriteLine($"[AsyncVM] IsBusy = {IsBusy} (Count: {count})");
        }
    }

    /// <summary>
    /// Decrements the busy counter. If the counter reaches 0, <see cref="BaseViewModel.IsBusy"/> is set to false.
    /// </summary>
    protected void DecrementBusy()
    {
        int count = Interlocked.Decrement(ref _busyCount);
        if (count <= 0)
        {
            IsBusy = false;
            OnPropertyChanged(nameof(IsBusy));
            System.Diagnostics.Debug.WriteLine($"[AsyncVM] IsBusy = {IsBusy} (Count: {count})");
        }
    }
}
