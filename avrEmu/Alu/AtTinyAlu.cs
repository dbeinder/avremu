using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avrEmu
{
    class AtTinyAlu : AvrAlu
    {
        public ExtByte StackPointer { get; protected set; }

        public AtTinyAlu(AvrController controller)
            : base(controller)
        {
            this.IORegisters.Add("SP", this.StackPointer);

            this.InstructionSet = new Dictionary<string, VI>() {
                //Arithmetics and Logic 
                { "add", new VI(this.Add, AvrInstrArgType.WorkingRegister, AvrInstrArgType.WorkingRegister) }, 
                { "adc", new VI(this.Adc, AvrInstrArgType.WorkingRegister, AvrInstrArgType.WorkingRegister) },
                { "adiw", new VI(this.Adiw, AvrInstrArgType.WorkingRegister, AvrInstrArgType.NumericConstant) },
                {"sub", new VI(this.Sub, AvrInstrArgType.WorkingRegister,AvrInstrArgType.WorkingRegister)},
                {"subi",new VI(this.Subi,AvrInstrArgType.WorkingRegister,AvrInstrArgType.NumericConstant)},
                {"sbc",new VI(this.Sbc,AvrInstrArgType.WorkingRegister,AvrInstrArgType.WorkingRegister)},
                {"sbci",new VI(this.Sbci,AvrInstrArgType.WorkingRegister,AvrInstrArgType.NumericConstant)},
                {"and",new VI(this.And,AvrInstrArgType.WorkingRegister,AvrInstrArgType.WorkingRegister)},
                {"andi",new VI(this.Andi,AvrInstrArgType.WorkingRegister,AvrInstrArgType.NumericConstant)},
                {"or",new VI(this.Or,AvrInstrArgType.WorkingRegister,AvrInstrArgType.WorkingRegister)},
                {"ori",new VI(this.Ori,AvrInstrArgType.WorkingRegister,AvrInstrArgType.NumericConstant)},
                {"eor",new VI(this.Eor, AvrInstrArgType.WorkingRegister,AvrInstrArgType.WorkingRegister)},
                {"com",new VI(this.Com,AvrInstrArgType.WorkingRegister)},
                {"neg",new VI(this.Neg,AvrInstrArgType.WorkingRegister)},
                {"sbr",new VI(this.Sbr,AvrInstrArgType.WorkingRegister,AvrInstrArgType.NumericConstant)},
                {"cbr",new VI(this.Cbr,AvrInstrArgType.WorkingRegister,AvrInstrArgType.NumericConstant)},
                {"inc",new VI(this.Inc,AvrInstrArgType.WorkingRegister)},
                {"dec",new VI(this.Dec,AvrInstrArgType.WorkingRegister)},
                {"tst",new VI(this.Tst,AvrInstrArgType.WorkingRegister)},
                {"clr",new VI(this.Clr,AvrInstrArgType.WorkingRegister)},
                {"ser",new VI(this.Ser,AvrInstrArgType.WorkingRegister)},

                //Branch
                { "rjmp", new VI(this.Adiw, AvrInstrArgType.NumericConstant) },
                { "ijmp", new VI(this.Ijmp) },
                { "rcall", new VI(this.Rcall, AvrInstrArgType.NumericConstant) },
                { "icall", new VI(this.Icall) },
                { "ret", new VI(this.Ret) },
                { "reti", new VI(this.Reti) },

                //Bit and Bit-Test

                //Data Transfer
                { "mov", new VI(this.Mov, AvrInstrArgType.WorkingRegister, AvrInstrArgType.WorkingRegister) },
                { "movw", new VI(this.Movw, AvrInstrArgType.WorkingRegister, AvrInstrArgType.WorkingRegister) },
                { "ldi", new VI(this.Ldi, AvrInstrArgType.WorkingRegister, AvrInstrArgType.NumericConstant) },
                { "in", new VI(this.In, AvrInstrArgType.WorkingRegister, AvrInstrArgType.IORegister) },
                { "out", new VI(this.Out, AvrInstrArgType.IORegister, AvrInstrArgType.WorkingRegister) },
                 
                //MCU Control
                { "nop", new VI(this.Nop) }
            };

        }

        #region Internal Helpers

        protected void PushToStack(ExtByte value)
        {
            this.Controller.SRAM.Memory[this.StackPointer.Value].Value = value.Value;
            this.StackPointer.Value++;
        }

        protected ExtByte PopFromStack()
        {
            this.StackPointer.Value--;
            return new ExtByte(this.Controller.SRAM.Memory[this.StackPointer.Value].Value);
        }

        #endregion

        #region Arithmetics and Logic

        protected void Add(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            ExtByte rr = this.Controller.WorkingRegisters[(args[1] as AvrInstrArgRegister).Register];
            rd.Value = (byte)(rd.Value + rr.Value);


            this.SREG["H"] = ((rd.Value & 0x0f) + (rr.Value & 0x0f)) > 15;
            rd.Value = SetFlags(rd.Value + rr.Value, SregFlags.CZNV);
        }

        protected void Adc(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            ExtByte rr = this.Controller.WorkingRegisters[(args[1] as AvrInstrArgRegister).Register];

            this.SREG["H"] = ((rd.Value & 0x0f) + (rr.Value & 0x0f) + this.CarryAsInt) > 15;
            rd.Value = SetFlags(
                rd.Value + rr.Value + this.CarryAsInt,
                SregFlags.CZNV
            );
        }

        protected void Adiw(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            ExtByte rdHigh = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register + 1];
            int k = (args[1] as AvrInstrArgConst).Constant;

            if (k < 0 || k > 63)
                throw new Exception("Invalid Value for Immediate summand");

            ushort regVal = WordHelper.FromBytes(rd, rdHigh);

            regVal = SetFlags16(regVal + k, SregFlags.CZNV);

            rd.Value = WordHelper.GetLowByte(regVal).Value;
            rdHigh.Value = WordHelper.GetHighByte(regVal).Value;
        }

        protected void Sub(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            ExtByte rr = this.Controller.WorkingRegisters[(args[1] as AvrInstrArgRegister).Register];
            ExtByte res = new ExtByte((byte)(rd.Value - rr.Value));

            this.SREG["H"] = (!rd[3] && rr[3] || rr[3] && res[3] || res[3] && !rd[3]);
            rd.Value = SetFlags(rd.Value - rr.Value, SregFlags.CZNV);
        }

        protected void Subi(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            int k = (args[1] as AvrInstrArgConst).Constant;
            ExtByte kex = new ExtByte((byte)k);
            ExtByte res = new ExtByte((byte)(rd.Value - k));

            this.SREG["H"] = (!rd[3] && kex[3] || kex[3] && res[3] || res[3] && !rd[3]);
            rd.Value = SetFlags(rd.Value - k, SregFlags.CZNV);
        }

        protected void Sbc(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            ExtByte rr = this.Controller.WorkingRegisters[(args[1] as AvrInstrArgRegister).Register];
            ExtByte res = new ExtByte((byte)(rd.Value - rr.Value - this.CarryAsInt));

            this.SREG["H"] = (!rd[3] && rr[3] || rr[3] && res[3] || res[3] && !rd[3]);
            rd.Value = SetFlags(rd.Value - rr.Value - this.CarryAsInt, SregFlags.CZNV);
        }

        protected void Sbci(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            int k = (args[1] as AvrInstrArgConst).Constant;
            ExtByte kex = new ExtByte((byte)k);
            ExtByte res = new ExtByte((byte)(rd.Value - k - this.CarryAsInt));

            this.SREG["H"] = (!rd[3] && kex[3] || kex[3] && res[3] || res[3] && !rd[3]);
            rd.Value = SetFlags(rd.Value - k - this.CarryAsInt, SregFlags.CZNV);
        }

        protected void And(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            ExtByte rr = this.Controller.WorkingRegisters[(args[1] as AvrInstrArgRegister).Register];

            rd.Value = SetFlags(rd.Value & rr.Value, SregFlags.CZNV);
        }

        protected void Andi(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            int k = (args[1] as AvrInstrArgConst).Constant;

            rd.Value = SetFlags(rd.Value & k, SregFlags.CZNV);
        }

        protected void Or(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            ExtByte rr = this.Controller.WorkingRegisters[(args[1] as AvrInstrArgRegister).Register];

            rd.Value = SetFlags(rd.Value | rr.Value, SregFlags.CZNV);
        }

        protected void Ori(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            int k = (args[1] as AvrInstrArgConst).Constant;

            rd.Value = SetFlags(rd.Value | k, SregFlags.CZNV);
        }

        protected void Eor(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            ExtByte rr = this.Controller.WorkingRegisters[(args[1] as AvrInstrArgRegister).Register];

            rd.Value = SetFlags(rd.Value ^ rr.Value, SregFlags.CZNV);
        }

        protected void Com(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            int hex = Convert.ToInt32(0xff);

            rd.Value = SetFlags(hex - rd.Value, SregFlags.CZNV);
        }

        protected void Neg(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            int hex = Convert.ToInt32(0x00);
            ExtByte res = new ExtByte((byte)(hex - rd.Value));

            this.SREG["H"] = (res[3] || rd[3]);

            rd.Value = SetFlags(hex - rd.Value, SregFlags.CZNV);
        }

        protected void Sbr(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            int k = (args[1] as AvrInstrArgConst).Constant;

            rd.Value = SetFlags(rd.Value | k, SregFlags.CZNV);
        }

        protected void Cbr(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            int k = (args[1] as AvrInstrArgConst).Constant;

            rd.Value = SetFlags(rd.Value & (0xff - k), SregFlags.CZNV);
        }

        protected void Inc(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];

            rd.Value = SetFlags(rd.Value + 1, SregFlags.CZNV);
        }

        protected void Dec(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];

            rd.Value = SetFlags(rd.Value - 1, SregFlags.CZNV);
        }

        protected void Tst(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];

            rd.Value = SetFlags(rd.Value & rd.Value, SregFlags.CZNV);
        }

        protected void Clr(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];

            rd.Value = SetFlags(rd.Value ^ rd.Value, SregFlags.CZNV);
        }

        protected void Ser(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];

            rd.Value = SetFlags(Convert.ToInt32(0xff), SregFlags.None);
        }
        #endregion

        #region Branch

        protected void Rjmp(List<AvrInstrArg> args)
        {
            int k = (args[0] as AvrInstrArgConst).Constant;

            this.PC += k + 1;
        }

        protected void Ijmp(List<AvrInstrArg> args)
        {
            this.PC = this.Z;
        }

        protected void Rcall(List<AvrInstrArg> args)
        {
            int k = (args[0] as AvrInstrArgConst).Constant;

            PushToStack(WordHelper.GetHighByte((ushort)this.PC));
            PushToStack(WordHelper.GetLowByte((ushort)this.PC));

            this.PC = k + 1;
        }

        protected void Icall(List<AvrInstrArg> args)
        {
            PushToStack(WordHelper.GetHighByte((ushort)this.PC));
            PushToStack(WordHelper.GetLowByte((ushort)this.PC));

            this.PC = this.Z;
        }

        protected void Ret(List<AvrInstrArg> args)
        {
            this.PC = WordHelper.FromBytes(PopFromStack(), PopFromStack());
        }

        protected void Reti(List<AvrInstrArg> args)
        {
            this.SREG["I"] = true;
            this.PC = WordHelper.FromBytes(PopFromStack(), PopFromStack());
        }


        #endregion

        #region Bit and Bit-Test

        #endregion

        #region Data Transfer

        protected void Mov(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            ExtByte rr = this.Controller.WorkingRegisters[(args[1] as AvrInstrArgRegister).Register];

            rd.Value = rr.Value;
        }

        protected void Movw(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            ExtByte rdHigh = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register + 1];
            ExtByte rr = this.Controller.WorkingRegisters[(args[1] as AvrInstrArgRegister).Register];
            ExtByte rrHigh = this.Controller.WorkingRegisters[(args[1] as AvrInstrArgRegister).Register + 1];

            rd.Value = rr.Value;
            rdHigh.Value = rrHigh.Value;
        }

        protected void Ldi(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            int k = (args[1] as AvrInstrArgConst).Constant;

            rd.Value = (byte)k;
        }

        protected void In(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            ExtByte ior = this.Controller.PeripheralRegisters[(args[1] as AvrInstrArgIOReg).IORegister];

            rd.Value = ior.Value;
        }

        protected void Out(List<AvrInstrArg> args)
        {
            ExtByte ior = this.Controller.PeripheralRegisters[(args[0] as AvrInstrArgIOReg).IORegister];
            ExtByte rd = this.Controller.WorkingRegisters[(args[1] as AvrInstrArgRegister).Register];

            ior.Value = rd.Value;
        }
        #endregion

        #region MCU Control

        protected void Nop(List<AvrInstrArg> args)
        {
            //No Operation
        }

        #endregion
    }

}
