using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Midi;
using Windows.Devices.Enumeration;


namespace PeteBrown.PowerShellMidi
{
    [Cmdlet(VerbsCommon.Get, "MidiInputPort")]
    public class GetMidiInputPort : AsyncPSCmdlet
    {
        [Parameter(
            Mandatory = true,
            HelpMessage = "The Id of the device from the MidiDeviceInformation object")]
        public string Id;

        protected override async Task ProcessRecordAsync()
        {
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var port = await MidiInPort.FromIdAsync(Id);

                if (port != null)
                    WriteDebug("Acquired input port: " + port.DeviceId);
                else
                    throw new ArgumentException("No input port available with that Id. You can get the Id through the MidiDeviceInformation returned from Get-Midi[Input|Output]DeviceInformation.", "Id");

                // we need to wrap this because PowerShell doesn't understand WinRT/UWP events
                var inputPort = new MidiInputPort(port);

                WriteObject(inputPort);
            }
            else
            {
                throw new ArgumentException("Parameter required. You can get the Id through the MidiDeviceInformation returned from Get-Midi[In|Out]DeviceInformation.", "Id");
            }
        }


    }
}
