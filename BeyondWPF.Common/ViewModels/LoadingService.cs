using CommunityToolkit.Mvvm.ComponentModel;

namespace BeyondWPF.Common.ViewModels;

/// <summary>
/// Service responsible for managing the global loading state of the application.
/// </summary>
public partial class LoadingService : ObservableObject
{
    private static readonly Lazy<LoadingService> _instance = new(() => new LoadingService());

    /// <summary>
    /// Gets the singleton instance of the <see cref="LoadingService"/>.
    /// </summary>
    public static LoadingService Instance => _instance.Value;

    private int _loaderCount;

    [ObservableProperty]
    private bool _isLoading;

    [ObservableProperty]
    private string? _statusMessage;

    /// <summary>
    /// Increments the loader counter. If the counter was 0, <see cref="IsLoading"/> is set to true.
    /// </summary>
    /// <param name="message">Optional message to display.</param>
    public void ShowLoader(string? message = null)
    {
        Interlocked.Increment(ref _loaderCount);
        if (!string.IsNullOrEmpty(message))
        {
            StatusMessage = message;
        }
        IsLoading = true;
    }

    /// <summary>
    /// Decrements the loader counter. If the counter reaches 0, <see cref="IsLoading"/> is set to false.
    /// </summary>
    public void HideLoader()
    {
        if (Interlocked.Decrement(ref _loaderCount) <= 0)
        {
            IsLoading = false;
            StatusMessage = null;
        }
    }

    /// <summary>
    /// Wraps a task with the global loader.
    /// </summary>
    /// <param name="task">The task to wrap.</param>
    /// <returns>A task representing the operation.</returns>
    public async Task WrapAsync(Task task)
    {
        ShowLoader();
        try
        {
            await task;
        }
        finally
        {
            HideLoader();
        }
    }

    /// <summary>
    /// Wraps a task returning a result with the global loader.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="task">The task to wrap.</param>
    /// <returns>A task representing the operation and containing the result.</returns>
    public async Task<T> WrapAsync<T>(Task<T> task)
    {
        ShowLoader();
        try
        {
            return await task;
        }
        finally
        {
            HideLoader();
        }
    }
}
