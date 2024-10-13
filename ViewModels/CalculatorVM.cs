using QuickCalc.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickCalc.ViewModels
{
    class CalculatorVM : ViewModelBase {
        private string input = "";
        private string output = "";

        public string CalcInput {
            get { return input; }
            set {
                input = value;
                CalcOutput = Calculation.Solve(input); 
                OnPropertyChanged(nameof(CalcInput));
             }
        }

        public string CalcOutput {
            get { return output; }
            set {
                output = value;
                OnPropertyChanged(nameof(CalcOutput));
             }
        }
    }
}
