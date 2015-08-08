using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Midi;

namespace PeteBrown.PowerShellMidi
{
    public delegate void MidiInputMessageReceivedEventHandler (object sender, MidiInputMessageReceivedEventArgs args);

    public class MidiInputPort
    {
        private MidiInPort _port;

        public MidiInputPort(MidiInPort port)
        {
            _port = port;

            _port.MessageReceived += OnMidiMessageReceived;
        }

        private void OnMidiMessageReceived(MidiInPort sender, MidiMessageReceivedEventArgs args)
        {
            if (MessageReceived != null)
                MessageReceived(this, new MidiInputMessageReceivedEventArgs(args.Message));
        }

        public event MidiInputMessageReceivedEventHandler MessageReceived;



    }
}
