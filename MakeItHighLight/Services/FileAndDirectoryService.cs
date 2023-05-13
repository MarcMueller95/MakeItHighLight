using System.Collections.Generic;
using System.IO;

namespace MakeItHighLight.Services
{
    internal class FileAndDirectoryService
    {
        public static readonly string s_StarDotStar = "*.*";
        public static readonly string s_DotCapWav = ".WAV";
        public static readonly string s_DotCapMp3 = ".MP3";



        public static readonly string s_CutFolder = "Cut\\";
        public static readonly string s_UncutFolder = "Uncut\\";
        public static readonly string s_FadeInFolder = "Fade In\\";
        public static readonly string s_FadeOutFolder = "Fade Out\\";
        public static readonly string s_FadeInandOutFolder = "FadeIn & Out\\";
        public static readonly string s_UnfadedFolder = "Unfaded\\";


        public static List<string> SortFilestoMP3andWav(System.Windows.DragEventArgs e)
        {
            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop))
            {

                var files = (string[])e.Data.GetData(System.Windows.DataFormats.FileDrop);
                var importList = new List<string>();
                foreach (string items in files)
                {
                    if (Directory.Exists(items))
                    {
                        foreach (var f in Directory.GetFiles(items, s_StarDotStar, SearchOption.AllDirectories))
                            if (System.IO.Path.GetExtension(f).ToUpperInvariant() == s_DotCapWav || System.IO.Path.GetExtension(f).ToUpperInvariant() == s_DotCapMp3)
                            {
                                importList.Add(f);
                            }
                    }
                }
                foreach (var items in files)
                    if (System.IO.Path.GetExtension(items).ToUpperInvariant() == s_DotCapWav || System.IO.Path.GetExtension(items).ToUpperInvariant() == s_DotCapMp3)
                    {
                        importList.Add(items);
                    }

                return importList;
            }

            return null;


        }




    }
}