using FontAwesome.Sharp;
using MakeItHighLight.Commands;
using MakeItHighLight.Communicator;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MakeItHighLight.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {


        #region Fields
        private readonly Communicater _communicator;
        private ViewModelBase _currentChildView;
        private string _caption;
        private IconChar _icon;
        #endregion
        #region Properties


        #region Property Childview
        public OverviewViewModel OverviewViewModel { get; }
        public SettingsViewModel SettingsViewModel { get; }
        public ImportViewModel ImportViewModel { get; }
        public HelpAndAboutViewModel HelpAndAboutViewModel { get; }
        public StartupViewModel Startup { get; }

        #endregion
        #region Property CurrentChildView
        public ViewModelBase CurrentChildView
        {
            get { return _currentChildView; }
            set
            {
                _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView));

            }
        }
        public string Caption
        {
            get { return _caption; }
            set
            {
                _caption = value;
                OnPropertyChanged(nameof(Caption));
            }
        }
        public IconChar Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }
        #endregion
        #region Property Commands
        public ICommand ShowOverviewViewCommand { get; }
        public ICommand ShowImportViewCommand { get; }
        public ICommand StartCuttingCommand { get; }
        public ICommand ViewModelCutCommand { get; }
        public ICommand ShowHelpViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }
        #endregion



        #endregion
        #region Constructor



        public MainViewModel(Communicater communicator,
                             OverviewViewModel overviewViewModel,
                             SettingsViewModel settingsViewModel,
                             ImportViewModel importViewModel,
                             HelpAndAboutViewModel helpAndAboutViewModel,
                             StartupViewModel startupViewModel
                            )
        {

            _communicator = communicator;
            OverviewViewModel = overviewViewModel;
            SettingsViewModel = settingsViewModel;
            ImportViewModel = importViewModel;
            HelpAndAboutViewModel = helpAndAboutViewModel;
            Startup = startupViewModel;
            CurrentChildView = Startup;

            ShowOverviewViewCommand = new ViewModelCommandBase(ExecuteShowOverviewViewCommand);
            ShowImportViewCommand = new ViewModelCommandBase(ExecuteShowImportViewCommand);
            ShowHelpViewCommand = new ViewModelCommandBase(ExecuteShowHelpAndAboutViewCommand);
            ShowSettingsViewCommand = new ViewModelCommandBase(ExecuteShowSettingsViewCommand);
        }




        #endregion
        #region Methods



        #region ShowChildView
        private void ExecuteShowHelpAndAboutViewCommand(object obj)
        {
            CurrentChildView = HelpAndAboutViewModel;
            Caption = "Help & About";
            Icon = IconChar.CircleQuestion;
        }

        private void ExecuteShowSettingsViewCommand(object obj)
        {
            CurrentChildView = SettingsViewModel;
            Caption = "Settings";
            Icon = IconChar.ScrewdriverWrench;
        }

        private void ExecuteShowImportViewCommand(object obj)
        {
            CurrentChildView = ImportViewModel;
            Caption = "Import";
            Icon = IconChar.FolderOpen;

        }

        public void ExecuteShowOverviewViewCommand(object obj)
        {
            CurrentChildView = OverviewViewModel;
            Caption = "Overview";
            Icon = IconChar.Home;
        }
        #endregion




        #endregion



    }
}
