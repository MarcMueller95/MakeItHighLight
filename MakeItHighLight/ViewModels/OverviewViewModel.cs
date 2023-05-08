using MakeItHighLight.Communicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeItHighLight.ViewModels
{
    internal class OverviewViewModel : ViewModelBase
    {
        private readonly Communicater _communicator;

        public OverviewViewModel(Communicater communicater)
        {
            _communicator = communicater;
        }
    }
}
