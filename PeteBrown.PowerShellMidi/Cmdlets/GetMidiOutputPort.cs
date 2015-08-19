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
    [Cmdlet(VerbsCommon.Get, "MidiOutputPort")]
    public class GetMidiOutputPort : AsyncPSCmdlet
    {
        [Parameter(
            Mandatory = true,
            HelpMessage = "The Id of the device from the MidiDeviceInformation object")]
        public string Id;

        protected override async Task ProcessRecordAsync()
        {
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var port = await MidiOutPort.FromIdAsync(Id);

                if (port != null)
                    WriteDebug("Acquired output port: " + port.DeviceId);
                else
                    throw new ArgumentException("No output port available with that Id. You can get the Id through the MidiDeviceInformation returned from Get-Midi[Input|Output]DeviceInformation.", "Id"); 

                // powershell has problems with some WinRT/UWP objects, so better to wrap it here
                var outputPort = new MidiOutputPort(port);

                WriteObject(outputPort);
            }
            else
            {
                throw new ArgumentException("Parameter required. You can get the Id through the MidiDeviceInformation returned from Get-Midi[Input|Output]DeviceInformation.", "Id");
            }
        }


    }
}
