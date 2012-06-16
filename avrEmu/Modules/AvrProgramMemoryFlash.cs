using System;
namespace avrEmu
{
    public class AvrProgramMemoryFlash:AvrProgramMemory
    {
        public AvrProgramMemoryFlash()
        {
        }

        public override AvrInstruction GetInstruction(int at)
        {
            throw new NotImplementedException("Flash-Memory is not yet implemented, use PMFormLink instead!");
        }
    }
}

