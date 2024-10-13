using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Text.Json;
using QuickCalc.Models;
using QuickCalc.ViewModels;

namespace QuickCalc
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void Window_Deactivated(object sender, EventArgs e) {
            Topmost = true;
        }
    }
}
