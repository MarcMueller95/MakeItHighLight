using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeItHighLight.Services
{
    public class PathsService
    {
        internal static string PrepareArtistList(List<string> artistlist)
        {
            int artistcount = artistlist.Count();     
            int counter = 0;
            string final = "";
            string artistsconfigurated = "";
            foreach (var items in artistlist)
            {
                try
                {
                    if (items.Last() == ' ')
                    {
                        artistsconfigurated = items.Remove(items.Length - 1);
                    }
                    else
                    {
                        artistsconfigurated = items;
                    }
                }
                catch
                {
                    artistsconfigurated = items;
                }
                try
                {
                    if (counter == artistcount)
                    {
                        if (final.Contains("&"))
                            final = final.Replace("&", ",");
                        final = Services.ExtensionMethodService.CapIt(final + " & " + artistsconfigurated);
                    }
                    else
                    {
                        if (counter == 0)
                        {
                            final = final + artistsconfigurated;

                        }
                        else if ((counter + 1) <= artistcount)
                        {
                            final = final + ", " + artistsconfigurated;
                        }
                    }
                    counter++;
                }
                catch
                {
                    final = artistsconfigurated;
                }
            }
            return final;
        }
        internal static string ReplaceForbiddenChars(string final)
        {
            while (final.Contains("?"))
            {
                final = final.Replace("?", "");
            }
            while (final.Contains("!"))
            {
                final = final.Replace("!", "");
            }
            while (final.Contains("/"))
            {
                final = final.Replace("/", "");
            }
            while (final.Contains("*"))
            {
                final = final.Replace("*", "-");
            }
            while (final.Contains("<"))
            {
                final = final.Replace("<", "");
            }
            while (final.Contains("|"))
            {
                final = final.Replace("|", ",");
            }
            while (final.Contains(">"))
            {
                final = final.Replace(">", "");
            }
            char rep = (char)39;
            while (final.Contains('"'))
            {
                final = final.Replace('"', rep);
            }
            while (final.Contains(":"))
            {
                final = final.Replace(":", ",");
            }
            return final;
        }
    }
}
