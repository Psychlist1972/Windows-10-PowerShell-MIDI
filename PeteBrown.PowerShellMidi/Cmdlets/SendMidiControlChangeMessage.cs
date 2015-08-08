using System.Management.Automation;
using Windows.Devices.Midi;

namespace PeteBrown.PowerShellMidi
{
    [Cmdlet(VerbsCommunications.Send, "MidiControlChangeMessage")]
    public class SendMidiControlChangeMessage : MidiChannelMessageSenderBase
    {
        [Parameter(Mandatory = true,
                   HelpMessage = "Controller number (the 'CC')")]
        public byte Controller;

        [Parameter(Mandatory = true,
                   HelpMessage = "Controller value")]
        public byte Value;

        protected override void ProcessRecord()
        {
            if (ValidateCommonParameters())
            {
                var message = new MidiControlChangeMessage(Channel, Controller, Value);

                Port.RawPort.SendMessage(message);
            }
        }
    }
}
