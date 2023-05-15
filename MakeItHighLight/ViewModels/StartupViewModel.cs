using MakeItHighLight.Communicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeItHighLight.ViewModels
{
    public class StartupViewModel : ViewModelBase
    {
        private readonly Communicater _communicator;

        public StartupViewModel(Communicater communicater)
        {
            _communicator = communicater;
        }
    }
}
