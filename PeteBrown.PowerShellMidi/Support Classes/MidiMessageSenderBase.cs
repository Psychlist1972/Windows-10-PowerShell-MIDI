using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Midi;
using Windows.Devices.Enumeration;


namespace PeteBrown.PowerShellMidi
{
    public abstract class MidiMessageSenderBase: PSCmdlet
    {
        [Parameter(Mandatory = true,
                   HelpMessage = "MIDI Port to send the message through")]
        public MidiOutputPort Port;


        protected virtual bool ValidateCommonParameters()
        {
            if (Port != null)
            {
                return true;
            }
            else
            {
                throw new ArgumentException("MIDI Port is required", "Port");
            }

        }

    }
}
