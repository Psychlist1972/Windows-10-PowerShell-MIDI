using System.Management.Automation;
using Windows.Devices.Midi;

namespace PeteBrown.PowerShellMidi
{
    [Cmdlet(VerbsCommunications.Send, "MidiProgramChangeMessage")]
    public class SendMidiProgramChangeMessage : MidiChannelMessageSenderBase
    {
        [Parameter(Mandatory = true,
                   HelpMessage = "Program number to send")]
        public byte Program;


        protected override void ProcessRecord()
        {
            if (ValidateCommonParameters())
            {
                var message = new MidiProgramChangeMessage(Channel, Program);

                Port.RawPort.SendMessage(message);
            }
        }
    }
}
