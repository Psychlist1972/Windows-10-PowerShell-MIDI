using System;

namespace PeteBrown.PowerShellMidi
{
    public class MidiPitchBendChangeMessageReceivedEventArgs : EventArgs
    {
        public byte Channel { get; private set; }
        public ushort Value { get; private set; }

        public MidiPitchBendChangeMessageReceivedEventArgs(byte channel,  ushort value)
        {
            Channel = channel;
            Value = value;
        }

    }
}