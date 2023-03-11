using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWoozle.Utils
{
    public static class FolderNameBuilder
    {
        public static string BuildFolderName(List<string> chosenArtists)
        {
            string retVal = string.Empty;
            chosenArtists.Sort();
            foreach (string artist in chosenArtists)
            {
                retVal += artist;
            }
            return retVal;
        }
    }
}
