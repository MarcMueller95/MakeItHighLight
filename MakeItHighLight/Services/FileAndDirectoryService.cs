using MakeItHighLight.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public static async Task DeleteTempData(Settings settings, Paths? pathing)
        {
            List<string> paths = new List<string>();
            var temppath = System.IO.Path.GetTempPath() + "M_I_HighLight\\";
            paths.Add(settings.DestinationFolderPersistent);
            paths.Add(temppath);
            paths.Add(temppath + "temp\\");
            paths.Add(temppath + "temp2\\");
            paths.Add(temppath + "temp3\\");
            if (pathing != null)
                paths.Add(pathing.FinalpathDir);

            await DeleteTempThroughPathList(paths);


        }

        public static async Task DeleteTempThroughPathList(List<string> list)
        {
            try
            {

                await Task.Run(() =>
                {
                    foreach (var item in list)
                    {
                        foreach (string sFile in System.IO.Directory.GetFiles(item, "*.wav"))
                        {
                            System.IO.File.Delete(sFile);
                        }

                    }

                });




            }
            catch (Exception)
            {

            }




        }



    }
}