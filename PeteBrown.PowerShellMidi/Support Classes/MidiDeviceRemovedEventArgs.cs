using System;

namespace PeteBrown.PowerShellMidi
{
    public class MidiDeviceRemovedEventArgs : EventArgs
    {
        public string Id { get; private set; }

        public MidiDeviceRemovedEventArgs(string id)
        {
            Id = id;
        }

    }
}