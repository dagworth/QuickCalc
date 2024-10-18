using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Text.Json;
using QuickCalc.Models;
using QuickCalc.ViewModels;
using QuickCalc.Views;

namespace QuickCalc
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new CalculatorView();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e) {
            DragMove();
        }

        private void Window_Deactivated(object sender, EventArgs e) {
            Topmost = true;
        }
    }
}
