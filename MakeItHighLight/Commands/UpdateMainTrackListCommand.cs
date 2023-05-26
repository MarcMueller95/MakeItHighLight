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
   public class UpdateMainTrackListCommand : ViewModelCommandBase
    {
        private readonly OverviewViewModel _viewModel;
        private readonly ImportViewModel _viewModel2;
        private readonly MainViewModel _viewModel3;
        private readonly Communicater _communicater;
        public UpdateMainTrackListCommand(OverviewViewModel viewModel, Communicater communicater)
        {
            _viewModel = viewModel;
            _communicater = communicater;
        }
        public UpdateMainTrackListCommand(ImportViewModel viewModel, Communicater communicater)
        {
            _viewModel2 = viewModel;
            _communicater = communicater;
        }
        public UpdateMainTrackListCommand(MainViewModel viewModel, Communicater communicater)
        {
            _viewModel3 = viewModel;
            _communicater = communicater;
        }
        public override void Execute(object track)
        {
            Track b = (Track)track;
            _communicater.AddTrackCom(b);
        }
    }
}
