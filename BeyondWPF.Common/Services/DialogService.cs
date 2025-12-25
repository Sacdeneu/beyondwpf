using System.ComponentModel;
using System.Runtime.CompilerServices;
using BeyondWPF.Core.Abstractions;

namespace BeyondWPF.Common.Services;

/// <summary>
/// Service responsible for managing the display of modal dialogs.
/// </summary>
public class DialogService : IDialogService
{
    private object? _currentView;
    private bool _isOpen;

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <inheritdoc />
    public object? CurrentView
    {
        get => _currentView;
        private set
        {
            if (_currentView != value)
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }
    }

    /// <inheritdoc />
    public bool IsOpen
    {
        get => _isOpen;
        private set
        {
            if (_isOpen != value)
            {
                _isOpen = value;
                OnPropertyChanged();
            }
        }
    }

    /// <inheritdoc />
    public void Show(object viewModel)
    {
        CurrentView = viewModel;
        IsOpen = true;
    }

    /// <inheritdoc />
    public void Close()
    {
        IsOpen = false;
        CurrentView = null;
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
