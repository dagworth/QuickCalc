using System.Diagnostics;
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
                    //make the history save after closing settings
                    CalculatorView new_calc_window = new CalculatorView();
                    mainWindow.MainContent.Content = new_calc_window;
                    new_calc_window.getInputBox.Loaded += (sender, e) => {
                        new_calc_window.getInputBox.Text = Properties.Settings.Default.CurrentText;
                    };
                }
            }
        }
    }
}
