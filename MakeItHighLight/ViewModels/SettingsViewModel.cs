using MakeItHighLight.Commands;
using MakeItHighLight.Communicator;
using MakeItHighLight.Models;
using MakeItHighLight.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MakeItHighLight.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly Communicater _communicator;
        private string _destinationFolderPersistent;
        private bool _shutdown;
        private bool _genres;
        private bool _fadeOut;
        private int _fadeInSecondsOut;
        private bool _fadeIn;
        private int _fadeInSecondsIn;
        private bool _FadeInTxtEnabled;
        private bool _FadeOutTxtEnabled;
        private bool _genrerepl;
        private string _genrereplstr = "";
        private bool _genrerepltxtenabled;
        private bool _savedboolTxt;
        public ICommand SettingSaveCommand { get; }
        public ICommand SettingDestinationFolderPersistentCommand { get; }
        public ICommand SettingShutdownBoolCommand { get; }
        public ICommand SettingGenreBoolCommand { get; }
        public ICommand SettingFadeOutBoolCommand { get; }
        public ICommand SettingFadeInBoolCommand { get; }
        public ICommand SettingUpdateSettingCommand { get; }
        public bool GenreRepl
        {
            get => _genrerepl;
            set
            {
                _genrerepl = value;
                SavedText = false;
                OnPropertyChanged();
                OnPropertyChanged(nameof(GenreReplTxtEnabled));
                OnPropertyChanged(nameof(SavedText));
            }
        }
        public string GenreReplStr
        {
            get => _genrereplstr;
            set
            {
                _genrereplstr = value;
                SavedText = false;
                OnPropertyChanged(nameof(SavedText));
                OnPropertyChanged();
            }
        }
        public bool GenreReplTxtEnabled
        {
            get => GenreRepl;
            set
            {
                _genrerepltxtenabled = value;
                SavedText = false;
                OnPropertyChanged(nameof(SavedText));
                OnPropertyChanged();
            }
        }
        public bool SavedText
        {
            get => _savedboolTxt;
            set
            {
                _savedboolTxt = value;
                OnPropertyChanged();
            }
        }
        public bool Shutdown
        {
            get => _shutdown;
            set
            {
                _shutdown = value;
                SavedText = false;
                OnPropertyChanged(nameof(SavedText));
                OnPropertyChanged();
            }
        }
        public bool Genres
        {
            get => _genres;
            set
            {
                _genres = value;
                SavedText = false;
                OnPropertyChanged(nameof(SavedText));
                OnPropertyChanged();
            }
        }
        public bool FadeOut
        {
            get => _fadeOut;
            set
            {
                _fadeOut = value;
                SavedText = false;
                OnPropertyChanged(nameof(SavedText));
                OnPropertyChanged();
                OnPropertyChanged(nameof(FadeOutTxtEnabled));
            }
        }
        public string FadeInSecondsOut
        {
            get => _fadeInSecondsOut.ToString();
            set
            {
                if (!Int32.TryParse(value, out _fadeInSecondsOut))
                {
                    SavedText = false;
                    OnPropertyChanged(nameof(SavedText));
                    FadeInSecondsOut = "0";
                }
                SavedText = false;
                OnPropertyChanged(nameof(SavedText));
                OnPropertyChanged();
            }
        }
        public bool FadeIn
        {
            get => _fadeIn;
            set
            {
                _fadeIn = value;
                SavedText = false;
                OnPropertyChanged(nameof(SavedText));
                OnPropertyChanged();
                OnPropertyChanged(nameof(FadeInTxtEnabled));
            }
        }
        public string FadeInSecondsIn
        {
            get => _fadeInSecondsIn.ToString();
            set
            {
                if (!Int32.TryParse(value, out _fadeInSecondsIn))
                {
                    SavedText = false;
                    OnPropertyChanged(nameof(SavedText));
                    FadeInSecondsIn = "0";
                }
                SavedText = false;
                OnPropertyChanged(nameof(SavedText));
                OnPropertyChanged();
            }
        }
        public string DestinationFolderPersistent
        {
            get => _destinationFolderPersistent;
            set
            {
                SavedText = false;
                OnPropertyChanged(nameof(SavedText));
                if (value != null)
                {
                    _destinationFolderPersistent = value;
                    OnPropertyChanged();
                }
                else
                {
                    _destinationFolderPersistent = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                }
            }
        }
        public bool FadeInTxtEnabled
        {
            get => FadeIn;
            set
            {
                _FadeInTxtEnabled = value;
                SavedText = false;
                OnPropertyChanged(nameof(SavedText));
                OnPropertyChanged();
            }
        }
        public bool FadeOutTxtEnabled
        {
            get => FadeOut;
            set
            {
                _FadeOutTxtEnabled = value;
                SavedText = false;
                OnPropertyChanged(nameof(SavedText));
                OnPropertyChanged();
            }
        }
        public SettingsViewModel(Communicater communicater)
        {
            _communicator = communicater;
            SettingSaveCommand = new ViewModelCommandBase(ExecuteSettingSaveCommand);
            SettingDestinationFolderPersistentCommand = new ViewModelCommandBase(ExecuteSettingDestinationFolderPersistentCommand);
            SettingUpdateSettingCommand = new UpdateSettingsCommand(this, communicater);
            ReadJsonSettingToProperties();
        }
        private async void ExecuteSettingSaveCommand(object obj)
        {
            SavedText = true;
            Settings settings = await Services.MappingService.MapFromSettingVMToSettings(this);
            await JsonService.SaveJsonFile(settings);
            await Application.Current.Dispatcher.BeginInvoke(
                           DispatcherPriority.Background,
            new Action(()
                           => SettingUpdateSettingCommand.Execute(settings)));
        }
        private async void ReadJsonSettingToProperties()
        {
            var settings = Services.JsonService.ReadJsonFile();
            if (settings == null)
            {
                settings = new Settings();
                settings.GenreRepl = false;
                settings.GenreReplStr = "";
                settings.Shutdown = false;
                settings.Genres = false;
                settings.FadeOut = false;
                settings.FadeinSecondsIn = "0";
                settings.FadeIn = false;
                settings.FadeinSecondsIn = "0";
                await Services.JsonService.SaveJsonFile(settings);
            }
            DestinationFolderPersistent = settings.DestinationFolderPersistent;
            Shutdown = settings.Shutdown;
            Genres = settings.Genres;
            FadeOut = settings.FadeOut;
            FadeInSecondsOut = settings.FadeinSecondsIn;
            FadeIn = settings.FadeIn;
            FadeInSecondsIn = settings.FadeinSecondsIn;
            GenreRepl = settings.GenreRepl;
            GenreReplStr = settings.GenreReplStr;
        }
        private void ExecuteSettingDestinationFolderPersistentCommand(object obj)
        {
            var fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            if (!String.IsNullOrEmpty(fbd.SelectedPath))
                DestinationFolderPersistent = fbd.SelectedPath;
            else
                DestinationFolderPersistent = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }
    }
}
