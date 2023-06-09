﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MakeItHighLight.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {      
        public event PropertyChangedEventHandler PropertyChanged;
        internal void OnPropertyChanged([CallerMemberName] string propertyname = "")
        {
            this.PropertyChanged?.Invoke(this,
                      new PropertyChangedEventArgs(propertyname));
        }
    }
}
