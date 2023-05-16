using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeItHighLight.Models
{
    public class Settings
    {




        public string DestinationFolderPersistent { get; set; }

        public bool Shutdown { get; set; } = false;

        public bool Genres { get; set; } = false;

        public bool FadeOut { get; set; } = false;

        public string FadeinSecondsOut { get; set; } = "";

        public bool FadeIn { get; set; } = false;

        public string FadeinSecondsIn { get; set; } = "";

        public bool GenreRepl { get; set; } = false;

        public string GenreReplStr { get; set; } = string.Empty;



        public Settings()
        {
            DestinationFolderPersistent = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }


    }
}
