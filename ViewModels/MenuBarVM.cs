using System.Windows.Input;
using QuickCalc.Models;
using QuickCalc.Views;

namespace QuickCalc.ViewModels {
    internal class MenuBarVM : ViewModelBase {
        public ICommand CloseApp {  get; }
        public ICommand OpenSettings {  get; }

        public MenuBarVM() {
            CloseApp = new RelayCommand(CloseApplication);
            OpenSettings = new RelayCommand(OpenSettingsView);
        }

        private void CloseApplication(object? parameter) {
            App.Current.Shutdown();
        }

        private void OpenSettingsView(object? parameter) {
            MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
            if (mainWindow != null) {
                if(mainWindow.MainContent.Content is not SettingsView) {
                    mainWindow.MainContent.Content = new SettingsView();
                } else {
                    mainWindow.MainContent.Content = new CalculatorView();
                }
            }
        }
    }
}
