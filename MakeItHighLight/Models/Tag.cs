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




        }


        

    }
}
