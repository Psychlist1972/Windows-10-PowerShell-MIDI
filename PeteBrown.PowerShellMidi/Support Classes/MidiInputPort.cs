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

    public class MidiInputPort
    {
        public MidiInPort RawPort { get; private set; }

        public bool TranslateZeroVelocityNoteOnMessage { get; set; }

        private SynchronizationContext _context;

        public MidiInputPort(MidiInPort port)
        {
            _context = SynchronizationContext.Current;

            RawPort = port;

            TranslateZeroVelocityNoteOnMessage = true;

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
                        if (NoteOffMessageReceived != null)
                            NoteOffMessageReceived(this, new MidiNoteOffMessageReceivedEventArgs(noteOnMessage.Channel, noteOnMessage.Note, noteOnMessage.Velocity, true));
                        //else
                        //    Console.WriteLine("c#: No subscribers to Note-Off event.");
                    }
                    else
                    {
                        // normal note on message

                        if (NoteOnMessageReceived != null)
                        {
                            try
                            {
                                NoteOnMessageReceived(this, new MidiNoteOnMessageReceivedEventArgs(noteOnMessage.Channel, noteOnMessage.Note, noteOnMessage.Velocity));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.ToString());
                            }
                        }
                        //else
                        //{
                        //    Console.WriteLine("c#: No subscribers to Note-On event.");
                        //}
                    }

                    break;

                case MidiMessageType.NoteOff:
                    var noteOffMessage = args.Message as MidiNoteOffMessage;
                    if (NoteOffMessageReceived != null)
                        NoteOffMessageReceived(this, new MidiNoteOffMessageReceivedEventArgs(noteOffMessage.Channel, noteOffMessage.Note, noteOffMessage.Velocity, false));
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

    }
}
