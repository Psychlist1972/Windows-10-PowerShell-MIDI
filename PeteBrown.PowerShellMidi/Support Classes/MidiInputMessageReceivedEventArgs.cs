using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Midi;

namespace PeteBrown.PowerShellMidi
{
    public class MidiInputMessageReceivedEventArgs : EventArgs
    {
        private IMidiMessage _message;

        public MidiInputMessageReceivedEventArgs(IMidiMessage message)
        {
            _message = message;
        }

    }
}
