using System.Management.Automation;
using Windows.Devices.Midi;

namespace PeteBrown.PowerShellMidi
{
    [Cmdlet(VerbsCommunications.Send, "MidiNoteOffMessage")]
    public class SendMidiNoteOffMessage : MidiChannelMessageSenderBase
    {
        [Parameter(Mandatory = true,
                   HelpMessage = "Note to send")]
        public byte Note;

        [Parameter(Mandatory = true,
                   HelpMessage = "Velocity of the note")]
        public byte Velocity;

        protected override void ProcessRecord()
        {
            if (ValidateCommonParameters())
            {
                var message = new MidiNoteOffMessage(Channel, Note, Velocity);

                Port.RawPort.SendMessage(message);
            }
        }
    }
}
