using System.Management.Automation;
using Windows.Devices.Midi;

namespace PeteBrown.PowerShellMidi
{
    [Cmdlet(VerbsCommunications.Send, "MidiSongSelectMessage")]
    public class SendMidiSongSelectMessage : MidiMessageSenderBase
    {
        [Parameter(Mandatory = true,
                   HelpMessage = "Song to select")]
        public byte Song;


        protected override void ProcessRecord()
        {
            if (ValidateCommonParameters())
            {
                var message = new MidiSongSelectMessage(Song);

                Port.RawPort.SendMessage(message);
            }
        }
    }
}
