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
