using System.Management.Automation;
using Windows.Devices.Midi;

namespace PeteBrown.PowerShellMidi
{
    [Cmdlet(VerbsCommunications.Send, "MidiSongPositionPointerMessage")]
    public class SendMidiSongPositionPointerMessage : MidiMessageSenderBase
    {
        [Parameter(Mandatory = true,
                   HelpMessage = "Song position in beats")]
        public ushort Beats;


        protected override void ProcessRecord()
        {
            if (ValidateCommonParameters())
            {
                var message = new MidiSongPositionPointerMessage(Beats);

                Port.RawPort.SendMessage(message);
            }
        }
    }
}
