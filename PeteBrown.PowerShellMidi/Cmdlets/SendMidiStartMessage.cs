using System.Management.Automation;
using Windows.Devices.Midi;


namespace PeteBrown.PowerShellMidi
{
    [Cmdlet(VerbsCommunications.Send, "MidiStartMessage")]
    public class SendMidiStartMessage : MidiMessageSenderBase
    {
        protected override void ProcessRecord()
        {
            if (ValidateCommonParameters())
            {
                var message = new MidiStartMessage();

                Port.RawPort.SendMessage(message);
            }
        }
    }
}
