using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sanford.Multimedia.Midi;
using DPA_Musicsheets.MusicObjects;

namespace DPA_Musicsheets
{
    class MidiToObject
    {
        TrackObjectBuilder trackBuilder = new TrackObjectBuilder();

        public MidiToObject(String path)
        {
            var sequence = new Sequence();
            sequence.Load(path);

            List<Track> tracks = new List<Track>();

            for (int i = 0; i < sequence.Count; i++)
            {
                tracks.Add(sequence[i]);
            }

            foreach (Track i in tracks)
            {
                string trackName = "Error";
                string timeSignature = "Error";
                string tempo = "Error";
                List<Tuple<ChannelMessage, MidiEvent>> notes = new List<Tuple<ChannelMessage, MidiEvent>>();

                #region
                foreach (MidiEvent midiEvent in i.Iterator())
                {
                    // ChannelMessages zijn de inhoudelijke messages.
                    if (midiEvent.MidiMessage.MessageType == MessageType.Channel)
                    {
                        var channelMessage = midiEvent.MidiMessage as ChannelMessage;
                        
                        if (channelMessage.Command == ChannelCommand.NoteOn || channelMessage.Command == ChannelCommand.NoteOff)
                        {
                            notes.Add(new Tuple<ChannelMessage, MidiEvent>(channelMessage, midiEvent));
                        }
                    }
                    // Meta zegt iets over de track zelf.
                    if (midiEvent.MidiMessage.MessageType == MessageType.Meta)
                    {
                        var metaMessage = midiEvent.MidiMessage as MetaMessage;
                        if (metaMessage.MetaType == MetaType.TrackName)
                        {
                            trackName = GetMetaString(metaMessage);
                        }
                        if (metaMessage.MetaType == MetaType.Tempo)
                        {
                            tempo = GetMetaString(metaMessage);
                        }
                        if (metaMessage.MetaType == MetaType.TimeSignature)
                        {
                            timeSignature = GetMetaString(metaMessage);
                        }
                    }
                }
                #endregion
                
                
                trackBuilder.buildTrack(trackName, timeSignature, tempo, notes);
           
            }
        }

        private static string GetMetaString(MetaMessage metaMessage)
        {
            byte[] bytes = metaMessage.GetBytes();
            switch (metaMessage.MetaType)
            {
                case MetaType.Tempo:
                    // Bitshifting is nodig om het tempo in BPM te be
                    int tempo = (bytes[0] & 0xff) << 16 | (bytes[1] & 0xff) << 8 | (bytes[2] & 0xff);
                    int bpm = 60000000 / tempo;
                    return metaMessage.MetaType + ": " + bpm;
                //case MetaType.SmpteOffset:
                //    break;
                case MetaType.TimeSignature:                               //kwart = 1 / 0.25 = 4
                    return metaMessage.MetaType + ": (" + bytes[0] + " / " + 1 / Math.Pow(bytes[1], -2) + ") ";
                //case MetaType.KeySignature:
                //    break;
                //case MetaType.ProprietaryEvent:
                //    break;
                case MetaType.TrackName:
                    return metaMessage.MetaType + ": " + Encoding.Default.GetString(metaMessage.GetBytes());
                default:
                    return metaMessage.MetaType + ": " + Encoding.Default.GetString(metaMessage.GetBytes());
            }
        }
    }
}
