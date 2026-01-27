using System.ComponentModel.DataAnnotations;
using BeyondWPF.Common.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BeyondWPF.BaseApplication.ViewModels;

/// <summary>
/// Example ViewModel demonstrating form validation.
/// </summary>
public partial class FormExampleViewModel : FormViewModel
{
    [ObservableProperty]
    [Required]
    [MinLength(3)]
    private string _username = string.Empty;

    [ObservableProperty]
    [Required]
    [EmailAddress]
    private string _email = string.Empty;

    /// <summary>
    /// Initializes a new instance of the <see cref="FormExampleViewModel"/> class.
    /// </summary>
    public FormExampleViewModel()
    {
        Title = "Form Example";
    }

    /// <summary>
    /// Submits the form if valid.
    /// </summary>
    [RelayCommand]
    private void Submit()
    {
        if (ValidateAll())
        {
            // Proceed with submission
        }
    }
}
