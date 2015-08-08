using System.Management.Automation;
using Windows.Devices.Midi;

namespace PeteBrown.PowerShellMidi
{
    [Cmdlet(VerbsCommunications.Send, "MidiChannelPressureMessage")]
    public class SendMidiChannelPressureMessage : MidiChannelMessageSenderBase
    {
        [Parameter(Mandatory = true,
                   HelpMessage = "Pressure value")]
        public byte Pressure;

        protected override void ProcessRecord()
        {
            if (ValidateCommonParameters())
            {
                var message = new MidiChannelPressureMessage(Channel, Pressure);

                Port.RawPort.SendMessage(message);
            }
        }
    }
}
