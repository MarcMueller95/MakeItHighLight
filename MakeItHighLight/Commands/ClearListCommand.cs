using MakeItHighLight.Communicator;
using MakeItHighLight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeItHighLight.Commands
{
    public class ClearListCommand : ViewModelCommandBase
    {

        private readonly MainViewModel _viewModel;
        private readonly Communicater _communicater;

        public ClearListCommand(MainViewModel viewModel, Communicater communicater)
        {
            _viewModel = viewModel;
            _communicater = communicater;

        }

        public override void Execute(object parameter)
        {
            _viewModel.IsProgressDone = false;
            _viewModel.ProgressbarMax = 0;
            _viewModel.ProgressbarValue = 0;
            _communicater.SetClearBtn();


        }
    }
}
