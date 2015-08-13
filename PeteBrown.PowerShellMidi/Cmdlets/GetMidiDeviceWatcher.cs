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
    [Cmdlet(VerbsCommon.Get, "MidiDeviceWatcher")]
    public class GetMidiDeviceWatcher : PSCmdlet
    {
        [Parameter(
            Mandatory = true,
            HelpMessage = "The type of device you want to watch: Input or Output")]
        [ValidateSet("InputPort", "OutputPort", IgnoreCase = true)]
        public string DeviceType;

        protected override void ProcessRecord()
        {
            string selector;

            switch (DeviceType.ToLower())
            {
                case "inputport":
                    selector = MidiInPort.GetDeviceSelector();
                    break;

                case "outputport":
                    selector = MidiOutPort.GetDeviceSelector();
                    break;
            }

            var deviceWatcher = DeviceInformation.CreateWatcher(MidiInPort.GetDeviceSelector());

            var midiDeviceWatcher = new MidiDeviceWatcher(deviceWatcher);

            WriteObject(midiDeviceWatcher);
        }


    }
}