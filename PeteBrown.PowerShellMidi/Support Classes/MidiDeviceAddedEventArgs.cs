using System;

namespace PeteBrown.PowerShellMidi
{
    public class MidiDeviceAddedEventArgs : EventArgs
    {
        public MidiDeviceInformation DeviceInformation { get; private set; }

        public MidiDeviceAddedEventArgs(MidiDeviceInformation deviceInformation)
        {
            DeviceInformation = deviceInformation;
        }

    }
}