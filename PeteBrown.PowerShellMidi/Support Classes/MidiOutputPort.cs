using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Midi;

namespace PeteBrown.PowerShellMidi
{
    public class MidiOutputPort
    {
        private MidiOutPort _port;

        public MidiOutPort RawPort
        {
            get { return _port; }
        }

        public MidiOutputPort(MidiOutPort port)
        {
            _port = port;

        }
    }
}
