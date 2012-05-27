using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avrEmu
{
    public abstract class AvrModule
    {
        public Dictionary<string, ExtByte> IORegisters = new Dictionary<string, ExtByte>();

        public virtual void ClockTick()
        {
        }

        public virtual void Reset()
        {
        }
    }
}
