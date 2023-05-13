using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeItHighLight.Services
{
    class ExtensionMethodService
    {




        public static string ConvertlistToStringWithSpaces(List<string> list)
        {
            string str = "";
            foreach (var item in list)
            {
                str = str + item + " ";
            }
            return str;
        }

        public static string CutStringAfterLastCharOccurance(string input, char pivot)
        {
            return input.Split(pivot).Last();
        }

        public static string CapIt(string input)
        {

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            input = input.ToLower();
            return textInfo.ToTitleCase(input);

        }






    }
}
