using MakeItHighLight.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeItHighLight.Communicator
{
    public class Communicater
    {

        public event Action<int> DropTrackCom;
        public event Action<Track> TrackCom;
        public event Action<ObservableCollection<Track>> TrackListCom;

        public void TrackDropProcess(int i)
        {
            DropTrackCom?.Invoke(i);
        }

        public void AddTrackCom(Track track)
        {
            TrackCom?.Invoke(track);
        }
        public void AddTrackListCom(ObservableCollection<Track> track)
        {
            TrackListCom?.Invoke(track);
        }

    }
}
