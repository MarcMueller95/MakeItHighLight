using MakeItHighLight.Models;
using MakeItHighLight.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace MakeItHighLight.Commands
{
    public class MainCutCommand : ViewModelAsyncCommandBase
    {

        private readonly MainViewModel _mainviewmodel;

        public MainViewModel Mainviewmodel { get; set; }

        public MainCutCommand(MainViewModel mainviewmodel)
        {
            _mainviewmodel = mainviewmodel;
            Mainviewmodel = mainviewmodel;
        }

        protected override async Task ExecuteAsync(object parameter)
        {

            Prepare();

            await Main();

            End();



        }

        private void End()
        {
            ProgressbarDone(true);
            ButtonhandlingFinnished();
            Mainviewmodel.IsStopBtnActive = false;
        }

        private async Task Main()
        {
            Paths paths = null;
            int counter = 0;
            foreach (var item in Mainviewmodel.OverviewViewModel.Tracks)
            {
                if (!Mainviewmodel.IsStopBtnActive)
                {
                    await Task.Run(async () =>
                    {
                        paths = await DoHighlightAndFades(item, Mainviewmodel.Settings);
                    });
                    counter++;
                    ProgressbarUpdate(counter);
                }
                await Services.FileAndDirectoryService.DeleteTempData(Mainviewmodel.Settings, paths);
            }
        }

        private async Task<Paths> DoHighlightAndFades(Track item, Settings settings)
        {







            long[] firstAndLastSample = new long[2];
            Paths paths = new Paths(item, settings);
            if (settings.Genres)

                paths.IsGenreDir = true;
            List<string> directories = paths.CreateDirectorieStructureForPath(settings, item.Kbit);
            Services.FileAndDirectoryService.CreateDirectories(directories, paths.Genre, paths);


            // Both
            if (settings.FadeIn && settings.FadeOut)
            {
                if (item.OrigExtensionWav)
                {
                    firstAndLastSample = await Services.TrackDataService.HighLight(item, paths, settings, true);
                }

                else
                {
                    firstAndLastSample = await Services.TrackDataService.HighLight(item, paths, settings, false);

                }



                await Services.TrackDataService.FadeInAndOut(paths.Temppath4 + paths.Title, paths, item, settings);


            }
            else
            {

                // FadeOut
                if (settings.FadeOut && settings.FadeIn == false)
                {
                    if (item.OrigExtensionWav)
                    {
                        firstAndLastSample = await Services.TrackDataService.HighLight(item, paths, settings, true);
                    }

                    else
                    {
                        firstAndLastSample = await Services.TrackDataService.HighLight(item, paths, settings, false);
                        //await Services.TrackDataService.TrimFile()
                    }

                    Services.TrackDataService.FadeOut(paths.Temppath4 + paths.Title, paths, item, settings);
                }
                // FadeIn
                else if (settings.FadeIn && settings.FadeOut == false)
                {
                    if (item.OrigExtensionWav)
                    {
                        firstAndLastSample = await Services.TrackDataService.HighLight(item, paths, settings, true);
                    }

                    else
                    {
                        firstAndLastSample = await Services.TrackDataService.HighLight(item, paths, settings, false);
                    }

                    Services.TrackDataService.FadeIn(paths.Temppath4 + paths.Title, paths, item, settings);
                }
                // unfaded
                else if (item.OrigExtensionWav)
                {
                    firstAndLastSample = await Services.TrackDataService.HighLight(item, paths, settings, true);
                    await Services.TrackDataService.CopyFile(paths.Temppath4 + paths.Title, paths, item);
                }
                else
                {
                    firstAndLastSample = await Services.TrackDataService.HighLight(item, paths, settings, false);
                    await Services.TrackDataService.CopyFile(paths.Temppath4 + paths.Title, paths, item);
                }

            }
            Services.TrackDataService.ConvertToMp3(item, paths);
            await item.Tag.TagsToPath(paths.Finalpath);
            return paths;

        }

        private void Prepare()
        {

            ButtonhandlingStart();
            ProgressbarPreparation(Mainviewmodel.OverviewViewModel.Tracks.Count);

        }

        private void ProgressbarPreparation(int count)
        {
            Mainviewmodel.ProgressbarMax = count;
        }
        private void ProgressbarUpdate(int counter)
        {
            _mainviewmodel.ProgressbarValue = counter;
        }
        private void ButtonhandlingFinnished()
        {
            Mainviewmodel.IsStartBtnVisible = true;
            Mainviewmodel.IsStopBtnVisible = false;
            Mainviewmodel.IsClearBtnVisible = true;
        }
        private void ProgressbarDone(bool b)
        {
            _mainviewmodel.IsProgressDone = b;
        }

        private void ButtonhandlingStart()
        {
            Mainviewmodel.IsStartBtnVisible = false;
            Mainviewmodel.IsStopBtnVisible = true;
            Mainviewmodel.IsClearBtnVisible = false;
        }
    }
}
