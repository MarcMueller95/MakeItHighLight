using MakeItHighLight.Communicator;
using MakeItHighLight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeItHighLight.Commands
{
    public class DropTrackCommand : ViewModelCommandBase
    {

        private readonly OverviewViewModel _viewModel;
        private readonly ImportViewModel _viewModel2;
        private readonly Communicater _communicater;

        public DropTrackCommand(OverviewViewModel viewModel, Communicater communicater)
        {
            _viewModel = viewModel;
            _communicater = communicater;
        }

        public DropTrackCommand(ImportViewModel viewModel, Communicater communicater)
        {
            _viewModel2 = viewModel;
            _communicater = communicater;
        }

        public override void Execute(object parameter)
        {
            int i = (int)parameter;
            _communicater.TrackDropProcess(i);


        }

    }
}
