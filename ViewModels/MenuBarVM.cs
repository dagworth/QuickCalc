﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuickCalc.ViewModels {
    internal class MenuBarVM : ViewModelBase {
        public ICommand CloseApp {  get; }
        public ICommand OpenSettings {  get; }

        public MenuBarVM() {
            //CloseApp = new RelayCommand(CloseApplication);
            //OpenSettings = new RelayCommand(OpenSettingsView);
            
        }
    }
}