using System.Management.Automation;
using Windows.Devices.Midi;


namespace PeteBrown.PowerShellMidi
{
    [Cmdlet(VerbsCommunications.Send, "MidiStopMessage")]
    public class SendMidiStopMessage : MidiMessageSenderBase
    {
        protected override void ProcessRecord()
        {
            if (ValidateCommonParameters())
            {
                var message = new MidiStopMessage();

                Port.RawPort.SendMessage(message);
            }
        }
    }
}
