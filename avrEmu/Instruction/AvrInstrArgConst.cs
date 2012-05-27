using System;
namespace avrEmu
{
    public class AvrInstrArgConst:AvrInstrArg
    {
        public int Constant { get; set; }

        public AvrInstrArgConst(int constant)
        {
            this.InstructionArgType = AvrInstrArgType.NumericConstant;
            this.Constant = constant;
        }
    }
}

