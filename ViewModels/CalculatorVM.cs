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
        private string output = "";
        public string CalcInput {
            set {
                CalcOutput = Calculation.Solve(value); 
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
