using MakeItHighLight.Communicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeItHighLight.ViewModels
{
    internal class StartupViewModel
    {
        private readonly Communicater _communicator;

        public StartupViewModel(Communicater communicater)
        {
            _communicator = communicater;
        }
    }
}
