using System;
namespace avrEmu
{
    public class AvrSram:AvrModule
    {
        public ExtByte[] Memory;

        public AvrSram(int capacity)
        {
            this.Memory = new ExtByte[capacity];
        }

        public override void Reset()
        {
            for (int i = 0; i < this.Memory.Length; i++)
            {
                this.Memory [i].Value = 0;
            }
        }
    }
}

