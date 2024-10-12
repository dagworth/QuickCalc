using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickCalc.ViewModels
{
    class CalculatorVM : ViewModelBase {
        private string input = "";

        public string CalcInput {
            get { return input; }
            set { input = value; OnPropertyChanged(nameof(input));}}
        }
    }
}
