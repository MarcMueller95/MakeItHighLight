using MakeItHighLight.Communicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeItHighLight.ViewModels
{
    internal class SettingsViewModel
    {
        private readonly Communicater _communicator;

        public SettingsViewModel(Communicater communicater)
        {
            _communicator = communicater;
        }
    }
}
