using System;
namespace avrEmu
{
    public class AvrInstrArgRegister:AvrInstrArg
    {
        public int Register { get; set; }

        public AvrInstrArgRegister(int registerNr)
        {
            this.InstructionArgType = AvrInstrArgType.WorkingRegister;
            this.Register = registerNr;
        }
    }
}

