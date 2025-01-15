using BeyondWPF.Common;
using System.Windows;

namespace BeyondWPF.BaseApplication
{
    public class Program : ApplicationBase
    {
        public Program(Application application) : base(application)
        {
        }

        [STAThread]
        public static void Main()
        {
            // Créez une instance de l'application WPF
            var wpfApplication = new Application();

            // Créez une instance de BaseApplication en passant l'application WPF
            var application = new ApplicationBase(wpfApplication);

            // Créez la fenêtre principale
            var mainWindow = new MainWindow();

            // Initialisez BaseApplication avec la fenêtre principale
            Initialize(mainWindow);

            // Définissez la fenêtre principale de l'application WPF
            wpfApplication.MainWindow = mainWindow;

            // Affichez la fenêtre principale
            mainWindow.Show();

            // Démarrez la boucle de messages de l'application WPF
            wpfApplication.Run();
        }
    }
}
