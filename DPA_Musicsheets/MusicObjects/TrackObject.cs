using Sanford.Multimedia.Midi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA_Musicsheets.MusicObjects
{
    class TrackObject
    {

        public string trackName { get; set; }

        public int[] timeSignature { get; set; }

        public int tempo { get; set; }

        public int ticksPerBeat { get; set; }
        public List<NoteObject> notes { get; set; }

        public TrackObject()
        {
            notes = new List<NoteObject>();
        }
        public void addNote(ChannelMessage message, MidiEvent midiEvent)
        {
          
            NoteObject note = new NoteObject();
            note.absoluteTicks = midiEvent.AbsoluteTicks;
            note.octaaf = message.Data1 / 12;
            note.setToonhoogte(message.Data1 % 12);
            notes.Add(note);
        }
      
        public void setNoteDuur()
        {
            for (int i = 0; i < notes.Count - 1; i++)
            {
                notes[i].type = ((double)notes[i + 1].absoluteTicks - (double)notes[i].absoluteTicks) / (double)ticksPerBeat;
            }
        }
    }
}
