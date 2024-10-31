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
                //applying the settings
                string updated = "";
                string[] response = value.Split("\r\n");

                for(int i = 0; i < response.Length; i++) {
                    if (response[i].IndexOf("error") == 0) {
                        if (Properties.Settings.Default.Debugger) {
                            updated += response[i];
                        }
                    } else if (double.TryParse(response[i], out _)){
                        if(Properties.Settings.Default.Digits_Rounded != 0) {
                            updated += Math.Round(double.Parse(response[i]),Properties.Settings.Default.Digits_Rounded).ToString();
                        } else {
                            updated += response[i];
                        }
                    } else  {
                        updated += "";
                    }
                    updated+="\r\n";
                }
                output = updated;
                OnPropertyChanged(nameof(CalcOutput));
             }
        }
    }
}
