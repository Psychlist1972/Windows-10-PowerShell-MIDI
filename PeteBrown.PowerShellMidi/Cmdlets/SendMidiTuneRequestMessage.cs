using System.Management.Automation;
using Windows.Devices.Midi;

namespace PeteBrown.PowerShellMidi
{
    [Cmdlet(VerbsCommunications.Send, "MidiTuneRequestMessage")]
    public class SendMidiTuneRequestMessage : MidiMessageSenderBase
    {

        protected override void ProcessRecord()
        {
            if (ValidateCommonParameters())
            {
                var message = new MidiTuneRequestMessage();

                Port.RawPort.SendMessage(message);
            }
        }
    }
}