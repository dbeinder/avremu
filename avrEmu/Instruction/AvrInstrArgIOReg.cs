using System;
namespace avrEmu
{
    public class AvrInstrArgIOReg:AvrInstrArg
    {
        public string IORegister { get; set; }

        public AvrInstrArgIOReg (string ioReg)
		{
			this.InstructionArgType = AvrInstrArgType.IORegister;
            this.IORegister = ioReg;
        }
    }
}

