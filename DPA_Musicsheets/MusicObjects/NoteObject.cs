﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets
{
    class NoteObject
    {
        public enum KruisMol { None, Kruis, Mol };

        private List<string> toonHoogtes;
        public KruisMol kruisMol { get; set; }

        public int octaaf { get; set; }

        public double type { get; set; }

        public bool rust { get; set; }

        public bool punt { get; set; }

        public string toonHoogte { get; set; }

        public int absoluteTicks { get; set; }

        public bool isMaatStreep { get; set; }

        public NoteObject()
        {
            toonHoogtes = new List<string>();
            toonHoogtes.Add("C");
            toonHoogtes.Add("C#");
            toonHoogtes.Add("D");
            toonHoogtes.Add("D#");
            toonHoogtes.Add("E");
            toonHoogtes.Add("F");
            toonHoogtes.Add("F#");
            toonHoogtes.Add("G");
            toonHoogtes.Add("G#");
            toonHoogtes.Add("A");
            toonHoogtes.Add("A#");
            toonHoogtes.Add("B");
        }

        public void setToonhoogte(int keyCode)
        {
            this.toonHoogte = toonHoogtes[keyCode];
        }

    }
}
