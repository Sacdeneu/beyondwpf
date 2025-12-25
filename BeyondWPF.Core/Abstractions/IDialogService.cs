using System.ComponentModel;

namespace BeyondWPF.Core.Abstractions;

/// <summary>
/// Interface for a service that manages modal dialogs.
/// </summary>
public interface IDialogService : INotifyPropertyChanged
{
    /// <summary>
    /// Gets the current content to display in the dialog.
    /// </summary>
    object? CurrentView { get; }

    /// <summary>
    /// Gets a value indicating whether a dialog is currently open.
    /// </summary>
    bool IsOpen { get; }

    /// <summary>
    /// Shows a modal dialog with the specified content.
    /// </summary>
    /// <param name="viewModel">The view model or content to display.</param>
    void Show(object viewModel);

    /// <summary>
    /// Closes the currently open dialog.
    /// </summary>
    void Close();
}
