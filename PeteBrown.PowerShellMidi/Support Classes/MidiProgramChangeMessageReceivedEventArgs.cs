using System;

namespace PeteBrown.PowerShellMidi
{
    public class MidiProgramChangeMessageReceivedEventArgs : EventArgs
    {
        public byte Channel { get; private set; }
        public byte Program { get; private set; }

        public MidiProgramChangeMessageReceivedEventArgs(byte channel, byte program)
        {
            Channel = channel;
            Program = program;
        }

    }
}
