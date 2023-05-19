using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeItHighLight.Models
{
    public class Paths
    {
        public string Genre { get; set; }
        public string Title { get; set; }
        public string Pre { get; set; }
        public string Origpath { get; set; }
        public bool IsGenreDir { get; set; }
        public bool IsGenreRepl { get; set; }
        public string GenreRepl { get; set; }
        public string Finalpath { get; set; }
        public string FinalpathDir { get; set; }
        public string FinalpathDirGenre { get; set; }
        public string Temppath { get; set; }
        public string Temppath2 { get; set; }
        public string Temppath3 { get; set; }
        public string Temppath4 { get; set; }


        public Paths(Track track, Settings settings)
        {
            Finalpath = settings.DestinationFolderPersistent + "\\";
            Origpath = track.Origpath;
            if (settings.GenreRepl)
            {
                track.Tag.Genrelist.Clear();
                track.Tag.Genrelist.Add(settings.GenreReplStr);
            }
            try
            {
                Title = Titlemaker(track);
            }
            catch (Exception)
            {

                Title = track.Title;
            }
            Genre = track.Tag.Genrelist[0];
            Temppath = Path.GetTempPath() + "M_I_HighLight\\";
            Temppath2 = Temppath + "temp\\";
            Temppath3 = Temppath + "temp2\\";
            Temppath4 = Temppath + "temp3\\";
        }
        private string Titlemaker(Track track)
        {
            string final = Services.PathsService.PrepareArtistList(track.Tag.Artistlist);
            final = $"{final} - {Services.ExtensionMethodService.CapIt(track.Tag.Title)} ({track.Tag.Year}) {track.Tag.Genrelist[0]} ({track.Tag.Bitrate} kbit).wav";
            return Services.PathsService.ReplaceForbiddenChars(final);        
        }


    }
}
