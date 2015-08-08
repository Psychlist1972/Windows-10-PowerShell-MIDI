using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Management.Automation;
using Windows.Devices.Midi;

namespace PeteBrown.PowerShellMidi
{
    [Cmdlet(VerbsCommunications.Send, "MidiSystemExclusiveMessage")]
    public class SendMidiSystemExclusiveMessage : MidiMessageSenderBase
    {
        [Parameter(Mandatory = true,
                   HelpMessage = "System Exclusive Data")]
        public byte[] RawData;


        protected override void ProcessRecord()
        {
            if (ValidateCommonParameters())
            {
                if (RawData != null && RawData.Length > 0)
                {
                    // temporary debug stuff
                    for (int i = 0; i < RawData.Length; i++)
                    {
                        WriteDebug(i + " : " + (int)(RawData[i]));
                    }



                    var message = new MidiSystemExclusiveMessage(RawData.AsBuffer());

                    Port.RawPort.SendMessage(message);
                }
                else
                {
                    throw new ArgumentException("No Sysex data provided", "RawData");
                }
            }
        }
    }
}
