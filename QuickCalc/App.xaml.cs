using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Windows;

namespace QuickCalc {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        protected override void OnStartup(StartupEventArgs e) {
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e) {
            if (!QuickCalc.Properties.Settings.Default.SaveText) QuickCalc.Properties.Settings.Default.CurrentText = "";
            QuickCalc.Properties.Settings.Default.Save();
            base.OnExit(e);
        }
    }

}
