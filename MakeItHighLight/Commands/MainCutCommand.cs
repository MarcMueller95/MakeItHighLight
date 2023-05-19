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
            //ButtonhandlingFinnished();
        }

        private async Task Main()
        {
            Paths paths = null;
            int counter = 0;
            foreach (var item in Mainviewmodel.Tracks)
            {
                if (!Mainviewmodel.IsStopBtnActive)
                {
                     paths = await DoHighlightAndFades(item, Mainviewmodel.Settings);
                    counter++;
                    ProgressbarUpdate(counter);
                }
                await Services.FileAndDirectoryService.DeleteTempData(Mainviewmodel.Settings, paths);
            }
        }

        private async Task<Paths> DoHighlightAndFades(Track item, Settings settings)
        {
            throw new NotImplementedException();
        }

        private void Prepare()
        {

            ButtonhandlingStart();
            ProgressbarPreparation(Mainviewmodel.Tracks.Count);

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

        private void ButtonhandlingStart()
        {
            Mainviewmodel.IsStartBtnVisible = false;
            Mainviewmodel.IsStopBtnVisible = true;
            Mainviewmodel.IsClearBtnVisible = false;
        }
    }
}
