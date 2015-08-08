using System.Management.Automation;
using Windows.Devices.Midi;


namespace PeteBrown.PowerShellMidi
{
    [Cmdlet(VerbsCommunications.Send, "MidiSystemResetMessage")]
    public class SendMidiSystemResetMessage : MidiMessageSenderBase
    {
        protected override void ProcessRecord()
        {
            if (ValidateCommonParameters())
            {
                var message = new MidiSystemResetMessage();

                Port.RawPort.SendMessage(message);
            }
        }
    }
}
