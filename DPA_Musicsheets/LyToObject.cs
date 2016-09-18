using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets
{
    class LyToObject
    {
        private string[] lilyPondContents;

        public LyToObject(string path)
        {
       
            lilyPondContents = System.IO.File.ReadAllText(path).Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();

            for(int i = 0; i < lilyPondContents.Length; i++)
            {
                lilyPondContents[i] = lilyPondContents[i].Replace("\r\n", string.Empty);
            }
            readContent();
        }

        private void readContent()
        {
            string tempo = "error";
            string maatsoort = "error";
            string sleutel = "error";


            for(int i = 0; i < lilyPondContents.Length; i++)
            {
                switch (lilyPondContents[i])
                {
                    case "\\tempo":
                        tempo = lilyPondContents[i + 1];
                        break;
                    case "\\time":
                        maatsoort = lilyPondContents[i + 1];
                        break;
                    case "\\repeat":
                        break;
                    case "\\alternative":

                        break;
                    case "\\clef":
                        sleutel = lilyPondContents[i + 1];
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
