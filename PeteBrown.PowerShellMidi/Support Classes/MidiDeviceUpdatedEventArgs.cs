using System;

namespace PeteBrown.PowerShellMidi
{
    public class MidiDeviceUpdatedEventArgs : EventArgs
    {
        public string Id { get; private set; }

        public MidiDeviceUpdatedEventArgs(string id)
        {
            Id = id;
        }

    }
}