using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;
using Windows.Devices.Midi;
using Windows.Devices.Enumeration;

namespace PeteBrown.PowerShellMidi
{

    [Cmdlet(VerbsCommon.Get, "MidiOutputDeviceInformation")]
    public class GetMidiOutputDeviceInformation : AsyncPSCmdlet
    {
        private List<MidiDeviceInformation> _devices;

        private async Task LoadInputDevicesAsync()
        {
            WriteDebug("Entering LoadInputDevicesAsync");

            _devices = new List<MidiDeviceInformation>();

            var selector = MidiOutPort.GetDeviceSelector();
            WriteDebug("Selector = " + selector);

            var devices = await DeviceInformation.FindAllAsync(selector);
            WriteDebug("Devices count = " + devices.Count);

            foreach (DeviceInformation info in devices)
            {
                WriteDebug("Loading device information into collection " + info.Id);

                var midiDevice = new MidiDeviceInformation();

                midiDevice.Id = info.Id;
                midiDevice.IsDefault = info.IsDefault;
                midiDevice.IsEnabled = info.IsEnabled;
                midiDevice.Name = info.Name;

                _devices.Add(midiDevice);
            }

            WriteDebug("Exiting LoadInputDevicesAsync");

        }


        protected override async Task ProcessRecordAsync()
        {
            WriteDebug("Entering ProcessRecord");

            await LoadInputDevicesAsync();

            if (_devices != null)
            {
                foreach (MidiDeviceInformation device in _devices)
                {
                    WriteObject(device);
                    WriteDebug(device.Id);
                }
            }
            WriteDebug("Exiting ProcessRecord");

        }

    }
}