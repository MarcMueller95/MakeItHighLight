using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TagLib.Matroska;

namespace MakeItHighLight.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public uint Year { get; set; }

        public int Bitrate { get; set; }

        public List<string> Artistlist { get; set; }

        public List<string> Genrelist { get; set; }

        public Tag(string inputPath)
        {

            Artistlist = new List<string>();
            Genrelist = new List<string>();
            try
            {


                var tfile = TagLib.File.Create(inputPath);// Exeptions Riff Header

                if (System.IO.Path.GetExtension(inputPath).ToUpperInvariant() == Services.FileAndDirectoryService.s_DotCapWav)
                {
                    Bitrate = 320;
                }
                else
                {

                    using (var reader = new Mp3FileReader(inputPath))
                    {
                        try
                        {

                            if (reader.XingHeader != null)
                            {
                                Bitrate = reader.XingHeader.Mp3Frame.BitRate / 1000;
                            }
                            else
                            {
                                var file = TagLib.File.Create(inputPath);
                                Bitrate = file.Properties.AudioBitrate;
                            }

                        }
                        catch (Exception e)
                        {
                            var file = TagLib.File.Create(inputPath);
                            Bitrate = file.Properties.AudioBitrate;
                        }



                    }
                }

                if (tfile.Tag.Performers.Count() == 0)
                {

                    if (tfile.Tag.AlbumArtists.Count() != 0)

                        foreach (string str in tfile.Tag.AlbumArtists)
                        {
                            Artistlist.Add(str);
                        }
                    else
                    {
                        if (tfile.Tag.Artists.Count() != 0)
                        {
                            foreach (string str in tfile.Tag.Artists)
                            {
                                Artistlist.Add(str);
                            }
                        }
                        else
                        {
                            Artistlist.Add("Unknown Artist");
                        }
                    }

                }
                if (tfile.Tag.Performers != null)
                    foreach (string str in tfile.Tag.Performers)
                    {
                        Artistlist.Add(str);
                    }
                else
                    Artistlist.Add("Unknown Artist");







                if (tfile.Tag.Year == 0)
                {

                    Year = 9999;
                }
                else
                    Year = tfile.Tag.Year;



                if (tfile.Tag.Genres.Count() > 0)
                {
                    foreach (var item in tfile.Tag.Genres)
                    {
                        Genrelist.Add(item);
                    }
                }
                else
                {
                    Genrelist.Add("_Unknown Genre");
                }
                if (tfile.Tag.Title != null)
                {
                    Title = tfile.Tag.Title;

                }
                else
                {
                    Title = "Unknown Title";
                }
                tfile.Save();
            }
            catch (Exception)
            {
                Artistlist.Add("Unknown Artist");
                Genrelist.Add("_Unknown Genre");

                Year = 9999;
                Title = "Unknown Title";

            }



        }



        public async Task TagsToPath(string inPath)
        {
            using (TagLib.File tlFile = TagLib.File.Create(inPath))
            {

                var outfile = TagLib.File.Create(inPath);
                outfile.Tag.Year = Year;
                outfile.Tag.Performers = new String[1] { Services.ExtensionMethodService.ConvertlistToStringWithSpaces(Artistlist) };
                outfile.Tag.Genres = Genrelist.ToArray();
                outfile.Tag.Title = Title;
                outfile.Save();
            }
        }
    }
}
