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

        public string timeSignature { get; set; }

        public int tempo { get; set; }
        public List<NoteObject> notes { get; set; }

        public TrackObject()
        {
           
            notes = new List<NoteObject>();
        }
        public void addNote(ChannelMessage message, MidiEvent midiEvent)
        {
            NoteObject note = new NoteObject();
            note.duur = midiEvent.DeltaTicks;
            note.toonHoogte = keyCodeToNodeNumber(message.Data1);
            note.octaaf = message.Data1 / 12;
            note.setToonhoogte(message.Data1 % 12);
            notes.Add(note);

        }

        public string keyCodeToNodeNumber(int keyCode)
        {
            return "";
        }
      
    }
}
