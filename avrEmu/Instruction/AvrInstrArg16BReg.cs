using System;
namespace avrEmu
{
    public enum AvrInstrArg16BType
    {
        Normal,
        PostIncrement,
        PreDecrement,
        Offset
    }

    public class AvrInstrArg16BReg:AvrInstrArg
    {
        public AvrInstrArg16BType Type;

        public char Register { get; set; }

        public int Offset { get; set; }

        public AvrInstrArg16BReg (char register, AvrInstrArg16BType type)
		{
			this.InstructionArgType = AvrInstrArgType.Register16Bit;
            this.Register = register;
            this.Type = type;
        }

        public AvrInstrArg16BReg (char register, AvrInstrArg16BType type, int offset)
		{
			this.InstructionArgType = AvrInstrArgType.Register16Bit;
            this.Register = register;
            this.Type = type;
            this.Offset = offset;
        }
    }
}

