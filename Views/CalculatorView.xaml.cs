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

        //scroll only increments by each line
        //this will also update how far down the resulting output string will be
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

            //need to still add the updating results page
        }

        //im used to this cus these are the controls for the browsers search bar
        //this also makes it feel more like a text editor
        private void InputBox_PreviewKeyDown(object sender, KeyEventArgs e) {
            int index = InputBox.CaretIndex;
            int line = InputBox.GetLineIndexFromCharacterIndex(index);
            int total_lines = InputBox.LineCount;

            switch (e.Key) {
                case Key.Up: //if we are at the top line, put cursor to the start
                    if (line == 0) InputBox.CaretIndex = 0;
                    break;
                case Key.Down: //if we are at the bottom line, put cursor to the end
                    if (total_lines == line+1) InputBox.CaretIndex = InputBox.Text.Length;
                    break;
            }
        }

        private void InputBox_Loaded(object sender, RoutedEventArgs e) {
            InputBox.Focus();
        }
    }
}
