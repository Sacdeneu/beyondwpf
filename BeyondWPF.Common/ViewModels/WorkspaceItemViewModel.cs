using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeyondWPF.Common.ViewModels;

/// <summary>
/// Represents an item in a workspace, such as a tab or a closable panel.
/// </summary>
public abstract partial class WorkspaceItemViewModel : AsyncViewModel
{
    /// <summary>
    /// Gets or sets the icon for the workspace item.
    /// </summary>
    [ObservableProperty]
    private object? _icon;

    /// <summary>
    /// Event raised when the workspace item requested to be closed.
    /// </summary>
    public event EventHandler? RequestClose;

    /// <summary>
    /// Command to close the workspace item.
    /// </summary>
    [RelayCommand]
    protected virtual void Close()
    {
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
