using MakeItHighLight.Views;
using MakeItHighLight.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeItHighLight.Models
{
    public class Track
    {



        private string? _path;
        private string? _origpath;
        private string? _length;
        private int _id;
        private string? _title;
        private Tag _tag;
 
        private bool _origextensionwav;
        private string _kbit;

        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public Tag Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
            }
        }

        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }



        public string Origpath
        {
            get
            {
                return _origpath;
            }
            set
            {
                _origpath = value;
            }
        }
        public string Length
        {
            get
            {
                return _length;
            }
            set
            {
                _length = value;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }

        public string Kbit
        {
            get
            {
                return _kbit;
            }
            set
            {
                _kbit = value;
            }
        }


        public bool OrigExtensionWav
        {
            get
            {
                return _origextensionwav;
            }
            set
            {
                _origextensionwav = value;
            }
        }

        public static async Task<Track> CreateTrackFromPath(string inputpath, int counter)
        {

            Track track = new Track();
            track.Origpath = inputpath;
            track.Length = await CreateTimeProberty(inputpath);
            track.Title = Services.ExtensionMethodService.CutStringAfterLastCharOccurance(inputpath, '\\');
            track.Id = counter;
            track.Tag = new Tag(inputpath);

            if (System.IO.Path.GetExtension(inputpath).ToUpperInvariant() == ".MP3")
            {
                track.OrigExtensionWav = false;
            }
            else
                track.OrigExtensionWav = true;

            return track;
        }
        public static async Task<string> CreateTimeProberty(string path)
        {

            TimeSpan time = default;

            if (System.IO.Path.GetExtension(path).ToUpperInvariant() == ".MP3")
                time = await Services.TrackDataService.GetTimeSpanFromPathMP3(path);
            else
                time = Services.TrackDataService.GetTimeSpanFromPathWAV(path);


            string days = time.Days.ToString();
            string hours = time.Hours.ToString();
            string minutes = time.Minutes.ToString();
            string secounds = time.Seconds.ToString();


            if (days.Length < 2)
            {
                days = "0" + days;
            }
            if (hours.Length < 2)
            {
                hours = "0" + hours;
            }
            if (minutes.Length < 2)
            {
                minutes = "0" + minutes;
            }
            if (secounds.Length < 2)
            {
                secounds = "0" + secounds;
            }


            return (hours + ":" + minutes + ":" + secounds);


        }
    






    }
}
