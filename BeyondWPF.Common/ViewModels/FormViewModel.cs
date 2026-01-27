using CommunityToolkit.Mvvm.ComponentModel;

namespace BeyondWPF.Common.ViewModels;

/// <summary>
/// A ViewModel that supports property validation using data annotations.
/// </summary>
public abstract partial class FormViewModel : ObservableValidator
{
    [ObservableProperty]
    private bool _isBusy;

    [ObservableProperty]
    private string _title = string.Empty;

    /// <summary>
    /// Validates all properties.
    /// </summary>
    /// <returns>True if all properties are valid, otherwise false.</returns>
    public bool ValidateAll()
    {
        ValidateAllProperties();
        return !HasErrors;
    }
}
