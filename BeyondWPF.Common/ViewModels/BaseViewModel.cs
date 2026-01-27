using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeyondWPF.Common.ViewModels;

/// <summary>
/// Base class for all ViewModels in the application.
/// </summary>
public abstract partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _title = string.Empty;

    /// <summary>
    /// Command to refresh the data in the ViewModel.
    /// </summary>
    [RelayCommand]
    public virtual void Refresh()
    {
    }

    /// <summary>
    /// Handles errors occurring in the ViewModel.
    /// </summary>
    /// <param name="message">The error message.</param>
    public virtual void HandleError(string message)
    {
    }
}
