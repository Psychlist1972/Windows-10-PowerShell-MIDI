using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Midi;

namespace PeteBrown.PowerShellMidi
{
    public delegate void MidiNoteOnMessageReceivedEventHandler(object sender, MidiNoteOnMessageReceivedEventArgs e);
    public delegate void MidiNoteOffMessageReceivedEventHandler(object sender, MidiNoteOffMessageReceivedEventArgs e);
    public delegate void MidiControlChangeMessageReceivedEventHandler(object sender, MidiControlChangeMessageReceivedEventArgs e);
    public delegate void MidiProgramChangeMessageReceivedEventHandler(object sender, MidiProgramChangeMessageReceivedEventArgs e);
    public delegate void MidiPitchBendChangeMessageReceivedEventHandler(object sender, MidiPitchBendChangeMessageReceivedEventArgs e);

    public enum MidiFilterMode
    {
        Inactive,
        Include,
        Exclude
    };

    public class MidiInputPort
    {
        public MidiInPort RawPort { get; private set; }

        public bool TranslateZeroVelocityNoteOnMessage { get; set; }

        public byte[] FilterNotes { get; set; }
        public MidiFilterMode FilterMode { get; set; }

        public MidiInputPort(MidiInPort port)
        {
            RawPort = port;

            TranslateZeroVelocityNoteOnMessage = true;

            FilterMode = MidiFilterMode.Inactive;

            RawPort.MessageReceived += OnMidiMessageReceived;
        }

        private void OnMidiMessageReceived(MidiInPort sender, MidiMessageReceivedEventArgs args)
        {
           // Console.WriteLine("OnMidiMessageReceived: " + args.Message.Type);

            switch (args.Message.Type)
            {
                case MidiMessageType.NoteOn:
                    var noteOnMessage = args.Message as MidiNoteOnMessage;

                    // a zero-velocity note-on message is equivalent to note-off
                    if (noteOnMessage.Velocity == 0 && TranslateZeroVelocityNoteOnMessage)
                    {
                        var ev1 = NoteOffMessageReceived;
                        if (ev1 != null)
                            ev1(this, new MidiNoteOffMessageReceivedEventArgs(noteOnMessage.Channel, noteOnMessage.Note, noteOnMessage.Velocity, true));
                    }
                    else
                    {
                        // normal note on message
                        var ev2 = NoteOnMessageReceived;
                        if (ev2 != null)
                        {
                            try
                            {
                                ev2(this, new MidiNoteOnMessageReceivedEventArgs(noteOnMessage.Channel, noteOnMessage.Note, noteOnMessage.Velocity));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }
                    }

                    break;

                case MidiMessageType.NoteOff:
                    var noteOffMessage = args.Message as MidiNoteOffMessage;
                    var ev3 = NoteOffMessageReceived;
                    if (ev3 != null)
                        ev3(this, new MidiNoteOffMessageReceivedEventArgs(noteOffMessage.Channel, noteOffMessage.Note, noteOffMessage.Velocity, false));
                    break;

                case MidiMessageType.ControlChange:
                    var ccMessage = args.Message as MidiControlChangeMessage;
                    var ev4 = ControlChangeMessageReceived;
                    if (ev4 != null)
                        ev4(this, new MidiControlChangeMessageReceivedEventArgs(ccMessage.Channel, ccMessage.Controller, ccMessage.ControlValue));
                    break;

                case MidiMessageType.ProgramChange:
                    var programMessage = args.Message as MidiProgramChangeMessage;
                    var ev5 = ProgramChangeMessageReceived;
                    if (ev5 != null)
                        ev5(this, new MidiProgramChangeMessageReceivedEventArgs(programMessage.Channel, programMessage.Program));
                    break;

                case MidiMessageType.PitchBendChange:
                    var pitchBendChangeMessage = args.Message as MidiPitchBendChangeMessage;
                    var ev6 = PitchBendChangeMessageReceived;
                    if (ev6 != null)
                        ev6(this, new MidiPitchBendChangeMessageReceivedEventArgs(pitchBendChangeMessage.Channel,  pitchBendChangeMessage.Bend));
                    break;

                // TODO: Add more message types


                default:
                    // message type we don't handle above. Ignore
                    break;
            }

        }

        //private void EventAsyncCallback(IAsyncResult ar)
        //{
            
        //}

        public event MidiNoteOnMessageReceivedEventHandler NoteOnMessageReceived;
        public event MidiNoteOffMessageReceivedEventHandler NoteOffMessageReceived;
        public event MidiControlChangeMessageReceivedEventHandler ControlChangeMessageReceived;
        public event MidiProgramChangeMessageReceivedEventHandler ProgramChangeMessageReceived;
        public event MidiPitchBendChangeMessageReceivedEventHandler PitchBendChangeMessageReceived;
    }
}
