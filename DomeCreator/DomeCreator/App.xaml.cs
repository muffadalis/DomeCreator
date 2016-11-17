using System.Windows;
using DomeCreator.ViewModel;

namespace DomeCreator
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e) {
            // Create the ViewModel and expose it using the View's DataContext
            MainWindow view = new MainWindow {DataContext = new MainWindowViewModel()};
            view.Show();
        }
    }
}
