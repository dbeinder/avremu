using System;

namespace avrEmu
{
    public abstract class AvrProgramMemory:AvrModule
    {
        public abstract AvrInstruction GetInstruction(int at);
    }
}

