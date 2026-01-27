using System.Windows.Controls;
using BeyondWPF.BaseApplication.ViewModels;

namespace BeyondWPF.BaseApplication.Views;

/// <summary>
/// Interaction logic for ViewModelsPage.xaml
/// </summary>
public partial class ViewModelsPage : UserControl
{
    /// <summary>
    /// Gets the async example ViewModel.
    /// </summary>
    public AsyncExampleViewModel AsyncExample { get; }

    /// <summary>
    /// Gets the form example ViewModel.
    /// </summary>
    public FormExampleViewModel FormExample { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ViewModelsPage"/> class.
    /// </summary>
    /// <param name="asyncExample">The async example ViewModel.</param>
    /// <param name="formExample">The form example ViewModel.</param>
    public ViewModelsPage(AsyncExampleViewModel asyncExample, FormExampleViewModel formExample)
    {
        AsyncExample = asyncExample;
        FormExample = formExample;
        
        InitializeComponent();
        DataContext = this;
    }
}
