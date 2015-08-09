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
        public IMidiOutPort RawPort { get; private set; }

        public MidiOutputPort(IMidiOutPort port)
        {
            RawPort = port;
        }
    }
}
