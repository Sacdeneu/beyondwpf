# BeyondWPF 🚀

**BeyondWPF** is a modern WPF UI framework designed to simplify the creation of beautiful and responsive desktop applications. It provides a set of theme-aware controls, a flexible theming system, and a robust MVVM architecture foundation.

## Features ✨

*   **Modern Styling**: A fresh look for standard WPF controls (Button, TextBox, ComboBox, ScrollBar, ListView, etc.).
*   **Theming Engine**: Built-in `ThemeService` for easy switching between Light/Dark modes and custom Accent colors.
*   **Resources System**: A comprehensive set of dynamic brushes (`ControlDarkBrush`, `ControlLightBrush`, `SystemAccentBrush`, etc.) for building consistent UIs.
*   **MVVM Ready**: Includes core abstractions (`IDialogService`, `INavigationService`) and helpers (`RelayCommand`, `ViewModelBase`) based on generic host and DI.
*   **Modal Dialogs**: A built-in modal dialog system that overlays the main window.

## Project Structure 📂

*   **BeyondWPF.Core**: Core interfaces, enums, and base classes.
*   **BeyondWPF.Common**: Implementations of core services (ThemeService, DialogService).
*   **BeyondWPF.Controls**: The UI library containing XAML styles and custom controls.
*   **BeyondWPF.BaseApplication**: A demo application showcasing all available controls and features.

## Getting Started 🛠️

### Prerequisites

*   .NET 8.0 SDK or later.
*   Visual Studio 2022.

### Installation

1.  Clone the repository.
2.  Open `BeyondWPF.sln`.
3.  Set `BeyondWPF.BaseApplication` as the startup project.
4.  Build and Run!

### Usage Example

To apply the theme in your own application:

1.  Add a reference to `BeyondWPF.Controls`.
2.  Merge the styles in your `App.xaml`:
    ```xml
    <ResourceDictionary.MergedDictionaries>
        <ResourceDictionary Source="pack://application:,,,/BeyondWPF.Controls;component/Themes/Generic.xaml"/>
    </ResourceDictionary.MergedDictionaries>
    ```
3.  Initialize the `ThemeService` in your `Program.cs` or `App.xaml.cs`:
    ```csharp
    var themeService = _host.Services.GetRequiredService<IThemeService>();
    themeService.ApplyTheme(SystemTheme.Dark);
    themeService.ApplySystemAccent(true);
    ```

## Contributing 🤝

Contributions are welcome! Please feel free to submit a Pull Request.

## License 📄

MIT License.
