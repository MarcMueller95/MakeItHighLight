using MakeItHighLight.Communicator;
using MakeItHighLight.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MakeItHighLight
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {

            Communicater communicater = new Communicater();


            OverviewViewModel overviewViewModel = new OverviewViewModel(communicater);
            SettingsViewModel settingsViewModel = new SettingsViewModel(communicater);
            ImportViewModel importViewModel = new ImportViewModel(communicater);
            HelpAndAboutViewModel helpViewModel = new HelpAndAboutViewModel(communicater);
            StartupViewModel startupViewModel = new StartupViewModel(communicater);
          
            MainViewModel mainViewModel = new MainViewModel(communicater,overviewViewModel, settingsViewModel, importViewModel, helpViewModel, startupViewModel);

            MainView MainView = new MainView()
            {
                DataContext = mainViewModel
            };

            MainView.Show();
        }
    }
}
