using System;
namespace avrEmu
{
    public enum AvrInstrArgType
    {
        NumericConstant,
        WorkingRegister,
        Register16Bit,
        IORegister
    }

    public abstract class AvrInstrArg
    {
        public AvrInstrArgType InstructionArgType;
    }
}

