using QuickCalc.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

namespace QuickCalc.Views
{
    /// <summary>
    /// Interaction logic for CalculatorView.xaml
    /// </summary>
    public partial class CalculatorView : UserControl
    {
        public CalculatorView()
        {
            InitializeComponent();
            DataContext = new CalculatorVM();
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            ScrollViewer scroll = (ScrollViewer)sender;
            double current = scroll.VerticalOffset;
            double height = InputBox.FontSize * 1.33;
            int movement = e.Delta;

            if (movement > 0) {
                scroll.ScrollToVerticalOffset(current-height);
            } else if (e.Delta < 0) {
                scroll.ScrollToVerticalOffset(current+height);
            }
        }
    }
}
