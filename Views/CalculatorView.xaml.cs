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
using QuickCalc.Models;
using System.Reflection;
using System.IO.IsolatedStorage;
using static System.Net.Mime.MediaTypeNames;

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
            InputBox.Text = Properties.Settings.Default.CurrentText;
        }

        //scroll only increments by each line
        //this will also update how far down the resulting output string will be, not yet made
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e) {
            double current = Scroller.VerticalOffset;
            double height = InputBox.FontSize * 1.33;
            int movement = e.Delta;

            if (movement > 0) {
                Scroller.ScrollToVerticalOffset(current-height);
            } else if (e.Delta < 0) {
                Scroller.ScrollToVerticalOffset(current+height);
            }
        }

        //im used to this cus these are the controls for the browsers search bar
        //this also makes it feel more like a text editor
        private void InputBox_PreviewKeyDown(object sender, KeyEventArgs e) {
            int index = InputBox.CaretIndex;
            int line = InputBox.GetLineIndexFromCharacterIndex(index);
            int total_lines = InputBox.LineCount;

            switch (e.Key) {
                case Key.Up: //if we are at the top line, put cursor to the start
                    if (line == 0) {
                        InputBox.CaretIndex = 0;
                        //select line
                        if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) {
                            InputBox.Select(0,InputBox.GetLineText(line).Length);
                            e.Handled = true;
                        }
                    };

                    
                    break;
                case Key.Down: //if we are at the bottom line, put cursor to the end
                    if (total_lines == line+1){
                        InputBox.CaretIndex = InputBox.Text.Length;
                        //select line
                        if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift)) {
                            InputBox.Select(InputBox.Text.Length-InputBox.GetLineText(line).Length,InputBox.Text.Length);
                            e.Handled = true;
                        }
                    }
                    break;
                case Key.C: //copy the response, ctrl-shift-C
                    if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.LeftShift)) {
                        try {
                            Clipboard.SetText(Response.GetLineText(line).Replace("\r\n",""));
                        } catch {
                            MessageBox.Show("ur copy broke again haha");
                        }
                    }
                    break;
            }
        }

        private void InputBox_Loaded(object sender, RoutedEventArgs e) {
            InputBox.Focus();
        }

        //copy on clipboard for whatever line the user copies from the response box
        private async void Response_GotFocus(object sender, RoutedEventArgs e) {
            if(Response.Text.Length == 0) { e.Handled = true; InputBox.Focus(); return; } //basically does nothing if there is no input nor response
            int original = InputBox.CaretIndex;
            await Task.Delay(20); //these delays are literally just for the user to see that the copy and paste worked and which line they copied
            
            try {
                int line = Response.GetLineIndexFromCharacterIndex(Response.CaretIndex);
                string text = Response.GetLineText(line);

                int line_start = Response.GetCharacterIndexFromLineIndex(line);
                Response.Select(line_start,text.Length-2); //highlights the part u copy
                //puts in clipboard
                Clipboard.SetText(text.Replace("\r\n",""));

            } catch { //the clipboard just doesnt want to do its copy and paste thing
                MessageBox.Show("ur copy and paste broke lmao");
            }

            await Task.Delay(20);
            InputBox.Focus(); //put ur cursor back where it was before, idk why u would want it but its an easy feature
            InputBox.CaretIndex = original;
        }

        private void InputBox_TextChanged(object sender, TextChangedEventArgs e) {
            Properties.Settings.Default.CurrentText = InputBox.Text;
        }
    }
}
