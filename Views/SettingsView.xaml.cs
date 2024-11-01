using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace QuickCalc.Views
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            ConsoleDebug_CheckBox.IsChecked = Properties.Settings.Default.Debugger;
            SaveText_CheckBox.IsChecked = Properties.Settings.Default.SaveText;
            DigitsRounded.Text = Properties.Settings.Default.Digits_Rounded.ToString();
        }

        private void ConsoleDebug_CheckBox_Checked(object sender, RoutedEventArgs e) {
            Properties.Settings.Default.Debugger = true;
        }

        private void ConsoleDebug_CheckBox_Unchecked(object sender, RoutedEventArgs e) {
            Properties.Settings.Default.Debugger = false;
        }

        private void DigitsRounded_PreviewTextInput(object sender, TextCompositionEventArgs e) {
            if (int.TryParse(e.Text, out _)) {
                DigitsRounded.Text = e.Text;
                Properties.Settings.Default.Digits_Rounded = int.Parse(e.Text);
                Keyboard.ClearFocus();
            }
            e.Handled = true;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void SaveText_CheckBox_Checked(object sender, RoutedEventArgs e) {
            Properties.Settings.Default.SaveText = true;
        }

        private void SaveText_CheckBox_Unchecked(object sender, RoutedEventArgs e) {
            Properties.Settings.Default.SaveText = false;
        }
    }
}
