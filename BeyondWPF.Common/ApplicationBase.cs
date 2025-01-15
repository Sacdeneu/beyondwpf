using BeyondWPF.Common.Appearance;
using BeyondWPF.Common.Resources;
using System.Windows;

namespace BeyondWPF.Common
{
    public class ApplicationBase

    {
        private static ApplicationBase? _uiApplication;

        private readonly System.Windows.Application? _application;
        public static Window? MainWindow { get; private set; }

        private ResourceDictionary? _resources;
        public ResourceDictionary Resources
        {
            get
            {
                if (_resources == null || _resources.MergedDictionaries.Count == 0)
                {
                    _resources = new ResourceDictionary();
                    var themesDictionary = new ThemeResourceDictionary();
                    var controlsDictionary = new ControlResourceDictionary();
                    _resources.MergedDictionaries.Add(themesDictionary);
                    _resources.MergedDictionaries.Add(controlsDictionary);
                }
                MainWindow.Resources = _resources;
                return _resources;
            }
            set
            {
                _resources = value;
            }
        }
        /// <summary>
        /// Gets the current application.
        /// </summary>
        public static ApplicationBase Current
        {
            get
            {
                _uiApplication ??= new ApplicationBase(System.Windows.Application.Current);

                return _uiApplication;
            }
        }
        public ApplicationBase(System.Windows.Application application)
        {
            if (application is null)
            {
                return;
            }

            _application = application;
        }
        public static void Initialize(Window mainWindow)
        {
            MainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));

            ApplicationThemeManager.ApplySystemTheme(true);
        }

    }
}
