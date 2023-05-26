using MakeItHighLight.Communicator;
using MakeItHighLight.Models;
using MakeItHighLight.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeItHighLight.Commands
{
    public class UpdateMainTrackFullCommand : ViewModelCommandBase
    {
        private readonly MainViewModel _viewModel3;
        private readonly Communicater _communicater;
        public UpdateMainTrackFullCommand(MainViewModel viewModel, Communicater communicater)
        {
            _viewModel3 = viewModel;
            _communicater = communicater;
        }
        public override void Execute(object songs)
        {
            ObservableCollection<Track> s = (ObservableCollection<Track>)songs;
            _communicater.AddTrackListCom(s);
        }
    }
}
