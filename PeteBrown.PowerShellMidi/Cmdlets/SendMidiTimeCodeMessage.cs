using System.Management.Automation;
using Windows.Devices.Midi;

namespace PeteBrown.PowerShellMidi
{
    [Cmdlet(VerbsCommunications.Send, "MidiTimeCodeMessage")]
    public class SendMidiTimeCodeMessage : MidiMessageSenderBase
    {
        [Parameter(Mandatory = true,
                   HelpMessage = "Frame type")]
        public byte FrameType;

        [Parameter(Mandatory = true,
                   HelpMessage = "Values")]
        public byte Values;

        protected override void ProcessRecord()
        {
            if (ValidateCommonParameters())
            {
                var message = new MidiTimeCodeMessage(FrameType, Values);

                Port.RawPort.SendMessage(message);
            }
        }
    }
}
