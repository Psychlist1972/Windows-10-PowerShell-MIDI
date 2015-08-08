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
    public abstract class MidiChannelMessageSenderBase: MidiMessageSenderBase
    {
        [Parameter(Mandatory = true,
                   HelpMessage = "Channel in the range of 0-15, not 1-16.")]
        public byte Channel;


        protected override bool ValidateCommonParameters()
        {
            if (base.ValidateCommonParameters())
            {
                if (Channel >=0 && Channel < 16)
                {
                    return true;
                }
                else
                {
                    throw new ArgumentException("MIDI channel must be between 0 and 15", "Channel");
                }
            }
            else
            {
                return false;
            }

        }

    }
}
