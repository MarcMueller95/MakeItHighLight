using MakeItHighLight.Communicator;
using MakeItHighLight.Models;
using MakeItHighLight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeItHighLight.Commands
{
    public class UpdateSettingsCommand : ViewModelCommandBase
    {
        private readonly SettingsViewModel _viewModel;      
        private readonly Communicater _communicater;
        public UpdateSettingsCommand(SettingsViewModel viewModel, Communicater communicater)
        {
            _viewModel = viewModel;
            _communicater = communicater;
        }    
        public override void Execute(object parameter)
        {
            Settings settings  = (Settings)parameter;
            _communicater.GetSettings(settings);
        }
    }
}
