using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Midi;

namespace PeteBrown.PowerShellMidi
{
    public delegate void MidiNoteOnMessageReceivedEventHandler(object sender, MidiNoteOnMessageReceivedEventArgs args);
    public delegate void MidiNoteOffMessageReceivedEventHandler(object sender, MidiNoteOffMessageReceivedEventArgs args);
    public delegate void MidiControlChangeMessageReceivedEventHandler(object sender, MidiControlChangeMessageReceivedEventArgs args);
    public delegate void MidiProgramChangeMessageReceivedEventHandler(object sender, MidiProgramChangeMessageReceivedEventArgs args);

    public class MidiInputPort
    {
        public MidiInPort RawPort { get; private set; }


        public MidiInputPort(MidiInPort port)
        {
            RawPort = port;

            RawPort.MessageReceived += OnMidiMessageReceived;
        }

        private void OnMidiMessageReceived(MidiInPort sender, MidiMessageReceivedEventArgs args)
        {
            switch (args.Message.Type)
            {
                case MidiMessageType.NoteOn:
                    var noteOnMessage = args.Message as MidiNoteOnMessage;
                    if (NoteOnMessageReceived != null)
                        NoteOnMessageReceived(this, new MidiNoteOnMessageReceivedEventArgs(noteOnMessage.Channel, noteOnMessage.Note, noteOnMessage.Velocity));
                    break;

                case MidiMessageType.NoteOff:
                    var noteOffMessage = args.Message as MidiNoteOffMessage;
                    if (NoteOffMessageReceived != null)
                        NoteOffMessageReceived(this, new MidiNoteOffMessageReceivedEventArgs(noteOffMessage.Channel, noteOffMessage.Note, noteOffMessage.Velocity));
                    break;

                case MidiMessageType.ControlChange:
                    var ccMessage = args.Message as MidiControlChangeMessage;
                    if (ControlChangeMessageReceived != null)
                        ControlChangeMessageReceived(this, new MidiControlChangeMessageReceivedEventArgs(ccMessage.Channel, ccMessage.Controller, ccMessage.ControlValue));
                    break;

                case MidiMessageType.ProgramChange:
                    var programMessage = args.Message as MidiProgramChangeMessage;
                    if (ProgramChangeMessageReceived != null)
                        ProgramChangeMessageReceived(this, new MidiProgramChangeMessageReceivedEventArgs(programMessage.Channel, programMessage.Program));
                    break;

                default:
                    // message type we don't handle above. Ignore
                    break;
            }

        }

        public event MidiNoteOnMessageReceivedEventHandler NoteOnMessageReceived;
        public event MidiNoteOffMessageReceivedEventHandler NoteOffMessageReceived;
        public event MidiControlChangeMessageReceivedEventHandler ControlChangeMessageReceived;
        public event MidiProgramChangeMessageReceivedEventHandler ProgramChangeMessageReceived;

    }
}
