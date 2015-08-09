using System;

namespace PeteBrown.PowerShellMidi
{
    public class MidiControlChangeMessageReceivedEventArgs : EventArgs
    {
        public byte Channel { get; private set; }
        public byte Controller { get; private set; }
        public byte Value { get; private set; }

        public MidiControlChangeMessageReceivedEventArgs(byte channel, byte controller, byte value)
        {
            Channel = channel;
            Controller = controller;
            Value = value;
        }

    }
}
