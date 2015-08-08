using System.Management.Automation;
using Windows.Devices.Midi;

namespace PeteBrown.PowerShellMidi
{
    [Cmdlet(VerbsCommunications.Send, "MidiPitchBendChangeMessage")]
    public class SendMidiPitchBendChangeMessage : MidiChannelMessageSenderBase
    {
        [Parameter(Mandatory = true,
                   HelpMessage = "Bend value")]
        public ushort Bend;

        protected override void ProcessRecord()
        {
            if (ValidateCommonParameters())
            {
                var message = new MidiPitchBendChangeMessage(Channel, Bend);

                Port.RawPort.SendMessage(message);
            }
        }
    }
}
