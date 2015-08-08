using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeteBrown.PowerShellMidi
{
    public class MidiDeviceInformation
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public bool IsEnabled { get; set; }
    }

}
