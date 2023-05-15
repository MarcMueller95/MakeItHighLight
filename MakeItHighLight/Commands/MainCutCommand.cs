using MakeItHighLight.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
         



        }


    }
}
