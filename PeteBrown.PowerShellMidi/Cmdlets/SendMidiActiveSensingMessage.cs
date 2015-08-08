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
    [Cmdlet(VerbsCommunications.Send, "MidiActiveSensingMessage")]
    public class SendMidiActiveSensingMessage : MidiMessageSenderBase
    {
        protected override void ProcessRecord()
        {
            if (ValidateCommonParameters())
            {
                var message = new MidiActiveSensingMessage();

                Port.RawPort.SendMessage(message);
            }
        }
    }
}
