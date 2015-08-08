using System.Management.Automation;
using Windows.Devices.Midi;


namespace PeteBrown.PowerShellMidi
{
    [Cmdlet(VerbsCommunications.Send, "MidiTimingClockMessage")]
    public class SendMidiTimingClockMessage : MidiMessageSenderBase
    {
        protected override void ProcessRecord()
        {
            if (ValidateCommonParameters())
            {
                var message = new MidiTimingClockMessage();

                Port.RawPort.SendMessage(message);
            }
        }
    }
}
