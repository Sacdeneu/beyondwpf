using CommunityToolkit.Mvvm.Input;

namespace BeyondWPF.Common.ViewModels;

/// <summary>
/// Base class for dialog ViewModels.
/// </summary>
public abstract partial class DialogBaseViewModel : BaseViewModel
{
    /// <summary>
    /// Gets or sets the result of the dialog.
    /// </summary>
    public bool? DialogResult { get; protected set; }

    /// <summary>
    /// Event raised when the dialog should be closed.
    /// </summary>
    public event EventHandler? RequestClose;

    /// <summary>
    /// Closes the dialog with a positive result.
    /// </summary>
    [RelayCommand]
    protected virtual void DefaultAccept()
    {
        DialogResult = true;
        OnRequestClose();
    }

    /// <summary>
    /// Closes the dialog with a negative result.
    /// </summary>
    [RelayCommand]
    protected virtual void DefaultCancel()
    {
        DialogResult = false;
        OnRequestClose();
    }

    /// <summary>
    /// Invokes the RequestClose event.
    /// </summary>
    protected virtual void OnRequestClose()
    {
        RequestClose?.Invoke(this, EventArgs.Empty);
    }
}
