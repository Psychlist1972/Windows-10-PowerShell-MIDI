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
    [Cmdlet(VerbsCommunications.Send, "MidiContinueMessage")]
    public class SendMidiContinueMessage : MidiMessageSenderBase
    {
        protected override void ProcessRecord()
        {
            if (ValidateCommonParameters())
            {
                var message = new MidiContinueMessage();

                Port.RawPort.SendMessage(message);
            }
        }
    }
}
