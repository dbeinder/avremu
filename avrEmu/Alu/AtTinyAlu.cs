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
                {"cpse", new VI(this.Cpse,AvrInstrArgType.WorkingRegister,AvrInstrArgType.WorkingRegister)},
                {"cp", new VI(this.Cp,AvrInstrArgType.WorkingRegister,AvrInstrArgType.WorkingRegister)},
                {"cpc",new VI(this.Cpc,AvrInstrArgType.WorkingRegister,AvrInstrArgType.WorkingRegister)},
                {"cpi",new VI(this.Cpi,AvrInstrArgType.WorkingRegister,AvrInstrArgType.NumericConstant)},
                {"sbrc",new VI(this.Sbrc,AvrInstrArgType.WorkingRegister,AvrInstrArgType.NumericConstant)},
                {"sbrs",new VI(this.Sbrs,AvrInstrArgType.WorkingRegister,AvrInstrArgType.NumericConstant)},
                {"sbic",new VI(this.Sbic,AvrInstrArgType.IORegister,AvrInstrArgType.NumericConstant)},
                {"sbis",new VI(this.Sbis,AvrInstrArgType.IORegister,AvrInstrArgType.NumericConstant)},
                {"brbs",new VI(this.Brbs,AvrInstrArgType.NumericConstant,AvrInstrArgType.NumericConstant)},
                {"brbc",new VI(this.Brbc,AvrInstrArgType.NumericConstant,AvrInstrArgType.NumericConstant)},
                {"breq",new VI(this.Breq,AvrInstrArgType.NumericConstant)},
                {"brne",new VI(this.Brne,AvrInstrArgType.NumericConstant)},
                {"brcs",new VI(this.Brcs,AvrInstrArgType.NumericConstant)},
                {"brcc",new VI(this.Brcc,AvrInstrArgType.NumericConstant)},
                {"brsh",new VI(this.Brcc,AvrInstrArgType.NumericConstant)},
                {"brlo",new VI(this.Brcs,AvrInstrArgType.NumericConstant)},
                {"brmi",new VI(this.Brmi,AvrInstrArgType.NumericConstant)},
                {"brpl",new VI(this.Brpl,AvrInstrArgType.NumericConstant)},
                {"brge",new VI(this.Brge,AvrInstrArgType.NumericConstant)},
                {"brlt",new VI(this.Brlt,AvrInstrArgType.NumericConstant)},
                {"brhs",new VI(this.Brhs,AvrInstrArgType.NumericConstant)},
                {"brhc",new VI(this.Brhc,AvrInstrArgType.NumericConstant)},
                {"brts",new VI(this.Brts,AvrInstrArgType.NumericConstant)},
                {"brtc",new VI(this.Brtc,AvrInstrArgType.NumericConstant)},
                {"brvs",new VI(this.Brvs,AvrInstrArgType.NumericConstant)},
                {"brvc",new VI(this.Brvc,AvrInstrArgType.NumericConstant)},
                {"brie",new VI(this.Brie,AvrInstrArgType.NumericConstant)},
                {"brid",new VI(this.Brid,AvrInstrArgType.NumericConstant)},


                //Bit and Bit-Test
                {"sbi",new VI(this.Sbi,AvrInstrArgType.IORegister,AvrInstrArgType.NumericConstant)},
                {"cbi",new VI(this.Cbi,AvrInstrArgType.IORegister,AvrInstrArgType.NumericConstant)},
                {"lsl",new VI(this.Lsl,AvrInstrArgType.WorkingRegister)},
                {"lsr",new VI(this.Lsr,AvrInstrArgType.WorkingRegister)},
                {"rol",new VI(this.Rol,AvrInstrArgType.WorkingRegister)},
                {"ror",new VI(this.Ror,AvrInstrArgType.WorkingRegister)},
                {"asr",new VI(this.Asr,AvrInstrArgType.WorkingRegister)},
                {"swap",new VI(this.Swap,AvrInstrArgType.WorkingRegister)},
                {"bset",new VI(this.Bset,AvrInstrArgType.NumericConstant)},
                {"bclr",new VI(this.Bclr,AvrInstrArgType.NumericConstant)},
                {"bst",new VI(this.Bst,AvrInstrArgType.WorkingRegister,AvrInstrArgType.NumericConstant)},
                {"bld",new VI(this.Bld,AvrInstrArgType.WorkingRegister,AvrInstrArgType.NumericConstant)},
                {"sec",new VI(this.Sec)},
                {"clc",new VI(this.Clc)},
                {"sen",new VI(this.Sen)},
                {"cln",new VI(this.Cln)},
                {"sez",new VI(this.Sez)},
                {"clz",new VI(this.Clz)},
                {"sei",new VI(this.Sei)},
                {"cli",new VI(this.Cli)},
                {"ses",new VI(this.Ses)},
                {"cls",new VI(this.Cls)},
                {"sev",new VI(this.Sev)},
                {"clv",new VI(this.Clv)},
                {"set",new VI(this.Set)},
                {"clt",new VI(this.Clt)},
                {"seh",new VI(this.Seh)},
                {"clh",new VI(this.Clh)},

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

            rd.Value = SetFlags(rd.Value & rr.Value, SregFlags.ZNV);
        }

        protected void Andi(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            int k = (args[1] as AvrInstrArgConst).Constant;

            rd.Value = SetFlags(rd.Value & k, SregFlags.ZNV);
        }

        protected void Or(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            ExtByte rr = this.Controller.WorkingRegisters[(args[1] as AvrInstrArgRegister).Register];

            rd.Value = SetFlags(rd.Value | rr.Value, SregFlags.ZNV);
        }

        protected void Ori(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            int k = (args[1] as AvrInstrArgConst).Constant;

            rd.Value = SetFlags(rd.Value | k, SregFlags.ZNV);
        }

        protected void Eor(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            ExtByte rr = this.Controller.WorkingRegisters[(args[1] as AvrInstrArgRegister).Register];

            rd.Value = SetFlags(rd.Value ^ rr.Value, SregFlags.ZNV);
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

            rd.Value = SetFlags(rd.Value | k, SregFlags.ZNV);
        }

        protected void Cbr(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            int k = (args[1] as AvrInstrArgConst).Constant;

            rd.Value = SetFlags(rd.Value & (0xff - k), SregFlags.ZNV);
        }

        protected void Inc(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];

            rd.Value = SetFlags(rd.Value + 1, SregFlags.ZNV);
        }

        protected void Dec(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];

            rd.Value = SetFlags(rd.Value - 1, SregFlags.ZNV);
        }

        protected void Tst(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];

            rd.Value = SetFlags(rd.Value & rd.Value, SregFlags.ZNV);
        }

        protected void Clr(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];

            rd.Value = SetFlags(rd.Value ^ rd.Value, SregFlags.ZNV);
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

            this.PC += k;
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

            this.PC = k;
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

        protected void Cpse(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            ExtByte rr = this.Controller.WorkingRegisters[(args[1] as AvrInstrArgRegister).Register];

            if (rd.Value == rr.Value)
            {
                this.PC += 2;
            }

            else
                this.PC += 1;
        }

        protected void Cp(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            ExtByte rr = this.Controller.WorkingRegisters[(args[1] as AvrInstrArgRegister).Register];

            ExtByte res = new ExtByte((byte)(rd.Value - rr.Value));

            this.SREG["H"] = !rd[3] && rr[3] || rr[3] && res[3] || res[3] && !rd[3];
            rd.Value = SetFlags(rd.Value, SregFlags.CZNV);
        }

        protected void Cpc(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            ExtByte rr = this.Controller.WorkingRegisters[(args[1] as AvrInstrArgRegister).Register];

            ExtByte res = new ExtByte((byte)(rd.Value - rr.Value - this.CarryAsInt));

            this.SREG["H"] = !rd[3] && rr[3] || rr[3] && res[3] || res[3] && !rd[3];
            rd.Value = SetFlags(rd.Value, SregFlags.CZNV);
        }

        protected void Cpi(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            int k = (args[1] as AvrInstrArgConst).Constant;
            ExtByte kex = new ExtByte((byte)k);

            ExtByte res = new ExtByte((byte)(rd.Value - k));

            this.SREG["H"] = !rd[3] && kex[3] || kex[3] && res[3] || res[3] && !rd[3];
            rd.Value = SetFlags(rd.Value, SregFlags.CZNV);
        }

        protected void Sbrc(List<AvrInstrArg> args)
        {
            ExtByte rr = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            int b = (args[1] as AvrInstrArgConst).Constant;

            if (Convert.ToInt32(rr[b]) == 0)
            {
                this.PC += 2;
            }
            else
                this.PC += 1;
        }

        protected void Sbrs(List<AvrInstrArg> args)
        {
            ExtByte rr = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            int b = (args[1] as AvrInstrArgConst).Constant;

            if (Convert.ToInt32(rr[b]) == 1)
            {
                this.PC += 2;
            }
            else
                this.PC += 1;
        }

        protected void Sbic(List<AvrInstrArg> args)
        {
            ExtByte a = this.Controller.PeripheralRegisters[(args[0] as AvrInstrArgIOReg).IORegister];
            int b = (args[1] as AvrInstrArgConst).Constant;

            if (Convert.ToInt32(a[b]) == 0)
            {
                this.PC += 2;
            }
            else
                this.PC += 1;
        }

        protected void Sbis(List<AvrInstrArg> args)
        {
            ExtByte a = this.Controller.PeripheralRegisters[(args[0] as AvrInstrArgIOReg).IORegister];
            int b = (args[1] as AvrInstrArgConst).Constant;

            if (Convert.ToInt32(a[b]) == 1)
            {
                this.PC += 2;
            }
            else
                this.PC += 1;
        }

        protected void Brbs(List<AvrInstrArg> args)
        {
            int s = (args[0] as AvrInstrArgConst).Constant;
            int k = (args[1] as AvrInstrArgConst).Constant;

            if (this.SREG[s] == true)
            {
                this.PC += k + 1;
            }
            else
                this.PC += 1;
        }

        protected void Brbc(List<AvrInstrArg> args)
        {
            int s = (args[0] as AvrInstrArgConst).Constant;
            int k = (args[1] as AvrInstrArgConst).Constant;

            if (this.SREG[s] == false)
            {
                this.PC += k + 1;
            }
            else
                this.PC += 1;
        }

        protected void Breq(List<AvrInstrArg> args)
        {
            int k = (args[0] as AvrInstrArgConst).Constant;

            if (this.SREG["Z"] == true)
            {
                this.PC += k + 1;
            }
            else
                this.PC += 1;
        }

        protected void Brne(List<AvrInstrArg> args)
        {
            int k = (args[0] as AvrInstrArgConst).Constant;

            if (this.SREG["Z"] == false)
            {
                this.PC += k + 1;
            }
            else
                this.PC += 1;
        }

        protected void Brcs(List<AvrInstrArg> args)
        {
            int k = (args[0] as AvrInstrArgConst).Constant;

            if (this.SREG["C"] == true)
            {
                this.PC += k + 1;
            }
            else
                this.PC += 1;
        }

        protected void Brcc(List<AvrInstrArg> args)
        {
            int k = (args[0] as AvrInstrArgConst).Constant;

            if (this.SREG["C"] == false)
            {
                this.PC += k + 1;
            }
            else
                this.PC += 1;
        }

        protected void Brmi(List<AvrInstrArg> args)
        {
            int k = (args[0] as AvrInstrArgConst).Constant;

            if (this.SREG["N"] == true)
            {
                this.PC += k + 1;
            }
            else
                this.PC += 1;
        }

        protected void Brpl(List<AvrInstrArg> args)
        {
            int k = (args[0] as AvrInstrArgConst).Constant;

            if (this.SREG["N"] == false)
            {
                this.PC += k + 1;
            }
            else
                this.PC += 1;
        }

        protected void Brge(List<AvrInstrArg> args)
        {
            int k = (args[0] as AvrInstrArgConst).Constant;

            if (this.SREG["N"] ^ this.SREG["V"] == false)
            {
                this.PC += k + 1;
            }
            else
                this.PC += 1;
        }

        protected void Brlt(List<AvrInstrArg> args)
        {
            int k = (args[0] as AvrInstrArgConst).Constant;

            if (this.SREG["N"] ^ this.SREG["V"] == true)
            {
                this.PC += k + 1;
            }
            else
                this.PC += 1;
        }

        protected void Brhs(List<AvrInstrArg> args)
        {
            int k = (args[0] as AvrInstrArgConst).Constant;

            if (this.SREG["H"] == true)
            {
                this.PC += k + 1;
            }
            else
                this.PC += 1;
        }

        protected void Brhc(List<AvrInstrArg> args)
        {
            int k = (args[0] as AvrInstrArgConst).Constant;

            if (this.SREG["H"] == false)
            {
                this.PC += k + 1;
            }
            else
                this.PC += 1;
        }

        protected void Brts(List<AvrInstrArg> args)
        {
            int k = (args[0] as AvrInstrArgConst).Constant;

            if (this.SREG["T"] == true)
            {
                this.PC += k + 1;
            }
            else
                this.PC += 1;
        }

        protected void Brtc(List<AvrInstrArg> args)
        {
            int k = (args[0] as AvrInstrArgConst).Constant;

            if (this.SREG["T"] == false)
            {
                this.PC += k + 1;
            }
            else
                this.PC += 1;
        }

        protected void Brvs(List<AvrInstrArg> args)
        {
            int k = (args[0] as AvrInstrArgConst).Constant;

            if (this.SREG["V"] == true)
            {
                this.PC += k + 1;
            }
            else
                this.PC += 1;
        }

        protected void Brvc(List<AvrInstrArg> args)
        {
            int k = (args[0] as AvrInstrArgConst).Constant;

            if (this.SREG["V"] == false)
            {
                this.PC += k + 1;
            }
            else
                this.PC += 1;
        }

        protected void Brie(List<AvrInstrArg> args)
        {
            int k = (args[0] as AvrInstrArgConst).Constant;

            if (this.SREG["I"] == true)
            {
                this.PC += k + 1;
            }
            else
                this.PC += 1;
        }

        protected void Brid(List<AvrInstrArg> args)
        {
            int k = (args[0] as AvrInstrArgConst).Constant;

            if (this.SREG["I"] == false)
            {
                this.PC += k + 1;
            }
            else
                this.PC += 1;
        }
        #endregion

        #region Bit and Bit-Test

        protected void Sbi(List<AvrInstrArg> args)
        {
            ExtByte a = this.Controller.PeripheralRegisters[(args[0] as AvrInstrArgIOReg).IORegister];
            int b = (args[1] as AvrInstrArgConst).Constant;

            a[b] = true;
        }

        protected void Cbi(List<AvrInstrArg> args)
        {
            ExtByte a = this.Controller.PeripheralRegisters[(args[0] as AvrInstrArgIOReg).IORegister];
            int b = (args[1] as AvrInstrArgConst).Constant;

            a[b] = false;
        }

        protected void Lsl(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];

            this.SREG["C"] = rd[7];
            this.SREG["H"] = rd[3];

            for (int i = 7; i > 0; i--)
            {
                rd[i] = rd[i - 1];
            }
            rd[0] = false;
            
            rd.Value = SetFlags(rd.Value, SregFlags.ZNV);
        }

        protected void Lsr(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];

            this.SREG["C"] = rd[0];

            for (int i = 0; i < 7; i++)
            {
                rd[i] = rd[i + 1];
            }
            rd[7] = false;

            rd.Value = SetFlags(rd.Value, SregFlags.ZNV);
        }

        protected void Rol(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];

            bool helpbool = this.SREG["C"];
            this.SREG["C"] = rd[7];
            this.SREG["H"] = rd[3];

            for (int i = 7; i > 0; i--)
            {
                rd[i] = rd[i - 1];
            }
            rd[0] = helpbool;

            rd.Value = SetFlags(rd.Value, SregFlags.ZNV);
        }

        protected void Ror(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];

            bool helpbool = this.SREG["C"];
            this.SREG["C"] = rd[0];

            for (int i = 0; i < 7; i++)
            {
                rd[i] = rd[i + 1];
            }
            rd[7] = helpbool;

            rd.Value = SetFlags(rd.Value, SregFlags.ZNV);
        }

        protected void Asr(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];

            bool helpbool = rd[7];
            this.SREG["C"] = rd[0];

            for (int i = 0; i < 7; i++)
            {
                rd[i] = rd[i + 1];
            }
            rd[7] = helpbool;

            rd.Value = SetFlags(rd.Value, SregFlags.ZNV);
        }

        protected void Swap(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            ExtByte res = new ExtByte(rd[3], rd[2], rd[1], rd[0], rd[7], rd[6], rd[5], rd[4]);
            bool helpbool = rd[7];
            this.SREG["C"] = rd[0];

            for (int i = 0; i < 7; i++)
            {
                rd[i] = rd[i + 1];
            }
            rd[7] = helpbool;

            rd.Value = SetFlags(res.Value, SregFlags.None);
        }

        protected void Bset(List<AvrInstrArg> args)
        {
            int s = (args[0] as AvrInstrArgConst).Constant;

            SREG[s]=true;
        }

        protected void Bclr(List<AvrInstrArg> args)
        {
            int s = (args[0] as AvrInstrArgConst).Constant;

            SREG[s] = false;
        }

        protected void Bst(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            int b = (args[0] as AvrInstrArgConst).Constant;

            SREG["T"] = rd[b];
        }

        protected void Bld(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters[(args[0] as AvrInstrArgRegister).Register];
            int b = (args[0] as AvrInstrArgConst).Constant;

            rd[b]=SREG["T"];
            rd.Value = SetFlags(rd.Value, SregFlags.None);
        }

        protected void Sec(List<AvrInstrArg> args)
        {
            SREG["C"]=true;
        }

        protected void Clc(List<AvrInstrArg> args)
        {
            SREG["C"] = false;
        }

        protected void Sen(List<AvrInstrArg> args)
        {
            SREG["N"] = true;
        }

        protected void Cln(List<AvrInstrArg> args)
        {
            SREG["N"] = false;
        }

        protected void Sez(List<AvrInstrArg> args)
        {
            SREG["Z"] = true;
        }

        protected void Clz(List<AvrInstrArg> args)
        {
            SREG["Z"] = false;
        }

        protected void Sei(List<AvrInstrArg> args)
        {
            SREG["I"] = true;
        }

        protected void Cli(List<AvrInstrArg> args)
        {
            SREG["I"] = false;
        }

        protected void Ses(List<AvrInstrArg> args)
        {
            SREG["S"] = true;
        }

        protected void Cls(List<AvrInstrArg> args)
        {
            SREG["S"] = false;
        }

        protected void Sev(List<AvrInstrArg> args)
        {
            SREG["V"] = true;
        }

        protected void Clv(List<AvrInstrArg> args)
        {
            SREG["V"] = false;
        }

        protected void Set(List<AvrInstrArg> args)
        {
            SREG["T"] = true;
        }

        protected void Clt(List<AvrInstrArg> args)
        {
            SREG["T"] = false;
        }

        protected void Seh(List<AvrInstrArg> args)
        {
            SREG["H"] = true;
        }

        protected void Clh(List<AvrInstrArg> args)
        {
            SREG["H"] = false;
        }
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
