using FontAwesome.Sharp;
using MakeItHighLight.Commands;
using MakeItHighLight.Communicator;
using MakeItHighLight.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace MakeItHighLight.ViewModels
{
    public class MainViewModel : ViewModelBase
    {


        #region Fields
        private readonly Communicater _communicator;
        private ViewModelBase _currentChildView;
        private int _progressbarvalue = 0;
        private int _progressbarmax = 0;
        private string _caption;
        private IconChar _icon;
        private ObservableCollection<Track> _tracks = new ObservableCollection<Track>();
        private bool _isProgressDone;
        private Settings _settings;
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

     
        public ObservableCollection<Track> Tracks
        {

            get => _tracks;
            set
            {

                _tracks = value;
                OnPropertyChanged();
                UpdateOverviewCommand.Execute(Tracks);



            }
        }

        public Settings Settings {
            get => _settings;
            set
            {
                _settings = value;
                OnPropertyChanged();          
            }
        }

        public bool IsProgressDone
        {
            get
            {


                return _isProgressDone;

            }
            set
            {
                _isProgressDone = value;

                OnPropertyChanged(nameof(IsProgressDone));


            }
        }


        public int ProgressbarValue
        {
            get { return _progressbarvalue; }
            set
            {
                _progressbarvalue = value;
                OnPropertyChanged(nameof(ProgressbarValue));
            }
        }

        public int ProgressbarMax
        {
            get
            {
                return _progressbarmax;
            }
            set
            {
                _progressbarmax = value;
                OnPropertyChanged(nameof(ProgressbarMax));

            }
        }

        #region Property Commands
        public ICommand ShowOverviewViewCommand { get; }
        public ICommand ShowImportViewCommand { get; }
        public ICommand StartMainFuncCommand { get; }
        public ICommand ViewModelCutCommand { get; }
        public ICommand ShowHelpViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }

       



        public ICommand UpdateOverviewCommand { get; }
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
            StartMainFuncCommand = new MainCutCommand(this);
            UpdateOverviewCommand = new UpdateMainTrackFullCommand(this, communicator);

            _communicator.TrackCom += UpdateTrackList;
            _communicator.DropTrackCom += UpdateProgessMax;
            _communicator.SettingsCom += UpdateSettings;

            this.GetSettings();

        }




        #endregion
        #region Methods
        private void UpdateTrackList(Track track)
        {
            if (track.Id == 1)
            {
                Tracks.Clear();
            }
            Tracks.Add(track);
            ObservableCollection<Track> tracks = new ObservableCollection<Track>();
            foreach (var item in Tracks)
            {
                tracks.Add(item);
            }
            Tracks = tracks;
       

        }
        private void UpdateProgessMax(int i)
        {
            ProgressbarMax = i;
        } 
        private void UpdateSettings(Settings settings)
        {
            Settings = settings;
        }

        public void GetSettings()
        {

            if (Services.JsonService.ReadJsonFile() != null)
                Settings = Services.JsonService.ReadJsonFile();
            else { Settings = new Settings(); }
        }

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
