using MakeItHighLight.Communicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeItHighLight.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly Communicater _communicator;



        public OverviewViewModel OverviewViewModel { get; }
        public SettingsViewModel SettingsViewModel { get; }
        public ImportViewModel ImportViewModel { get; }
        public HelpAndAboutViewModel HelpAndAboutViewModel { get; }
        public StartupViewModel Startup { get; }





        public MainViewModel(Communicater communicator,
                             OverviewViewModel overviewViewModel,
                             SettingsViewModel settingsViewModel,
                             ImportViewModel importViewModel,
                             HelpAndAboutViewModel helpAndAboutViewModel,
                             StartupViewModel startupViewModel)
        {

            _communicator= communicator;
            OverviewViewModel = overviewViewModel;
            SettingsViewModel = settingsViewModel;
            ImportViewModel = importViewModel;
            HelpAndAboutViewModel = helpAndAboutViewModel;          
            Startup = startupViewModel;
        }
    }
}
