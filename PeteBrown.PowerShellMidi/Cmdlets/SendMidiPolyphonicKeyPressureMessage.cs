using System.Management.Automation;
using Windows.Devices.Midi;

namespace PeteBrown.PowerShellMidi
{
    [Cmdlet(VerbsCommunications.Send, "MidiPolyphonicKeyPressureMessage")]
    public class SendMidiPolyphonicKeyPressureMessage : MidiChannelMessageSenderBase
    {
        [Parameter(Mandatory = true,
                   HelpMessage = "Note this applies to")]
        public byte Note;

        [Parameter(Mandatory = true,
                   HelpMessage = "Pressure value")]
        public byte Pressure;

        protected override void ProcessRecord()
        {
            if (ValidateCommonParameters())
            {
                var message = new MidiPolyphonicKeyPressureMessage(Channel, Note, Pressure);

                Port.RawPort.SendMessage(message);
            }
        }
    }
}
