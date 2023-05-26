using MakeItHighLight.Commands;
using MakeItHighLight.Communicator;
using MakeItHighLight.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace MakeItHighLight.ViewModels
{
    public class ImportViewModel : ViewModelBase
    {
        private ObservableCollection<Track> _tracks = new ObservableCollection<Track>();
        private readonly Communicater _communicator;
        public ICommand DropTrackCommand { get; }
        public ICommand AddTrackCommand { get; }
        public ObservableCollection<Track> Tracks
        {
            get => _tracks;
            set
            {
                _tracks = value;
                OnPropertyChanged();
            }
        }
        public ImportViewModel(Communicater communicater)
        {
            _communicator = communicater;
            DropTrackCommand = new DropTrackCommand(this, communicater);
            AddTrackCommand = new UpdateMainTrackListCommand(this, communicater);
        }
        public async Task ExecuteImportDrop(DragEventArgs e)
        {
            List<string> sortedList;
            ObservableCollection<Track> tracks = new ObservableCollection<Track>();
            int counter = 1;
            await Task.Run(async () =>
            {
                if ((sortedList = Services.FileAndDirectoryService.SortFilestoMP3andWav(e)) != null)
                    foreach (var item in sortedList)
                    { 
                        Track track = await Track.CreateTrackFromPath(item, counter);
                        tracks.Add(track);
                        ObservableCollection<Track> temp = new ObservableCollection<Track>(tracks);
                        this.Tracks = temp;
                        await Application.Current.Dispatcher.BeginInvoke(
                            DispatcherPriority.Background,
                            new Action(()
                            => AddTrackCommand.Execute(track)));
                        DropTrackCommand.Execute(counter);
                        counter++;
                    }
            });
            this.Tracks = tracks;
        }
    }
}
