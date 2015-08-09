using System;

namespace PeteBrown.PowerShellMidi
{
    public class MidiNoteOnMessageReceivedEventArgs : EventArgs
    {
        public byte Channel { get; private set; }
        public byte Note { get; private set; }
        public byte Velocity { get; private set; }

        public MidiNoteOnMessageReceivedEventArgs(byte channel, byte note, byte velocity)
        {
            Channel = channel;
            Note = note;
            Velocity = velocity;
        }

    }
}
