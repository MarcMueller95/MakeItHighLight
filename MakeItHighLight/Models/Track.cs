﻿using MakeItHighLight.Views;
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
            get => _id; set => _id = value;            
        }
        public Tag Tag
        {
            get => _tag; set => _tag = value;
            {
                
            }
        }

        public string Path
        {
            get => _path; set =>_path = value;
            
        }



        public string Origpath
        {
            get => _origpath; set => _origpath = value;
        
        }
        public string Length
        {
            get => _length; set => _length = value;
            
        }

        public string Title
        {
            get => _title; set => _title = value;
            
        }

        public string Kbit
        {
            get => _kbit; set => _kbit = value;
        }


        public bool OrigExtensionWav
        {
            get => _origextensionwav; set => _origextensionwav = value;
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
            track.Kbit = track.Tag.Bitrate.ToString();
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
                days = "0" + days;
            if (hours.Length < 2)
                hours = "0" + hours;
            if (minutes.Length < 2)
                minutes = "0" + minutes;
            if (secounds.Length < 2)
                secounds = "0" + secounds;
            return (hours + ":" + minutes + ":" + secounds);
        }
    






    }
}
