using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avrEmu
{
    abstract class AvrAlu : AvrModule
    {
        public AvrController Controller { get; protected set; }

        public AvrAlu(AvrController controller)
        {
            this.Controller = controller;
            InitSREG();
        }

        public virtual void ExecuteInstruction(AvrInstruction instruction)
        {
            if (instruction.Arguments.Count != this.InstructionSet[instruction.Instruction].Args.Count)
                throw new Exception("Invalid number of Arguments for Instruction!");

            for (int i = 0; i < instruction.Arguments.Count; i++)
            {
                if (instruction.Arguments[i].InstructionArgType != this.InstructionSet[instruction.Instruction].Args[i])
                    throw new Exception("Argument #" + (i + 1) + " is of invalid Type!");
            }

            int oldPC = this.Controller.ProgramCounter;
            this.InstructionSet[instruction.Instruction].Exec(instruction.Arguments);
            if (!this.InstructionSet[instruction.Instruction].ModifiesPC)
                this.Controller.ProgramCounter++;
        }

        public override void Reset()
        {
            this.SREG.Value = 0;
            base.Reset();
        }

        #region Instruction Set

        protected delegate void Instruction(List<AvrInstrArg> args);

        protected class VI
        {
            public Instruction Exec;
            public List<AvrInstrArgType> Args;
            public bool ModifiesPC;

            public VI(Instruction exec, List<AvrInstrArgType> args)
            {
                this.Exec = exec;
                this.Args = args;
                this.ModifiesPC = false;
            }

            public VI(Instruction exec)
            {
                this.Exec = exec;
                this.Args = new List<AvrInstrArgType>();
                this.ModifiesPC = false;
            }

            public VI(Instruction exec, bool modPC)
            {
                this.Exec = exec;
                this.Args = new List<AvrInstrArgType>();
                this.ModifiesPC = modPC;
            }

            public VI(Instruction exec, AvrInstrArgType arg0)
            {
                this.Exec = exec;
                this.Args = new List<AvrInstrArgType>() { arg0 };
                this.ModifiesPC = false;
            }

            public VI(Instruction exec, AvrInstrArgType arg0, bool modPC)
            {
                this.Exec = exec;
                this.Args = new List<AvrInstrArgType>() { arg0 };
                this.ModifiesPC = modPC;
            }

            public VI(Instruction exec, AvrInstrArgType arg0, AvrInstrArgType arg1)
            {
                this.Exec = exec;
                this.Args = new List<AvrInstrArgType>() { arg0, arg1 };
                this.ModifiesPC = false;
            }

            public VI(Instruction exec, AvrInstrArgType arg0, AvrInstrArgType arg1, bool modPC)
            {
                this.Exec = exec;
                this.Args = new List<AvrInstrArgType>() { arg0, arg1 };
                this.ModifiesPC = modPC;
            }

        }

        protected Dictionary<string, VI> InstructionSet = new Dictionary<string, VI>();

        #endregion

        #region Status Register

        public ExtByte SREG { get; protected set; }

        protected virtual void InitSREG()
        {
            this.SREG = new ExtByte(0);

            this.SREG.BitNames = new List<string>() { "C", "Z", "N", "V", "S", "H", "T", "I" };

            this.SREG.BitEvents["N"].BitChanged += new BitChangedEventHandler(SREG_BitChanged);
            this.SREG.BitEvents["V"].BitChanged += new BitChangedEventHandler(SREG_BitChanged);

            this.IORegisters.Add("SREG", this.SREG);
        }

        protected virtual void SREG_BitChanged(object sender, BitChangedEventArgs e)
        {
            this.SREG["S"] = this.SREG["N"] ^ this.SREG["V"];
        }

        #region Status Flag Helpers

        [Flags]
        protected enum SregFlags
        {
            None = 0,
            C = 1,
            Z = 2,
            N = 4,
            V = 8,
            //H = 16, //no idea for an universal way to detect

            //CZNVH = SregFlags.C | SregFlags.Z | SregFlags.N | SregFlags.V | SregFlags.H,
            CZNV = SregFlags.C | SregFlags.Z | SregFlags.N | SregFlags.V,
            ZNV = SregFlags.Z | SregFlags.N | SregFlags.V
        }

        protected byte SetFlags(int result, SregFlags flags)
        {
            byte newValue = (byte)result;

            if ((flags & SregFlags.C) == SregFlags.C)
                this.SREG["C"] = (result != newValue);

            if ((flags & SregFlags.Z) == SregFlags.Z)
                this.SREG["Z"] = (newValue == 0);

            if ((flags & SregFlags.N) == SregFlags.N)
                this.SREG["N"] = ((new ExtByte(newValue))[7] == true);

            if ((flags & SregFlags.V) == SregFlags.V)
                this.SREG["V"] = (result > 127) || (result < -128);

            return newValue;
        }

        protected UInt16 SetFlags16(int result, SregFlags flags)
        { //untested
            UInt16 newValue = (UInt16)result;

            if ((flags & SregFlags.C) == SregFlags.C)
                this.SREG["C"] = (result != newValue);

            if ((flags & SregFlags.Z) == SregFlags.Z)
                this.SREG["Z"] = (newValue == 0);

            if ((flags & SregFlags.N) == SregFlags.N)
                this.SREG["N"] = (newValue & (1 << 15)) > 0;

            if ((flags & SregFlags.V) == SregFlags.V)
                this.SREG["V"] = (result > 127) || (result < -128);

            return newValue;
        }

        #endregion

        #endregion

        #region Shortcuts

        public ushort X
        {
            get { return GetWord(26); }
            set { StoreWord(26, value); }
        }

        public ushort Y
        {
            get { return GetWord(28); }
            set { StoreWord(28, value); }
        }

        public ushort Z
        {
            get { return GetWord(30); }
            set { StoreWord(30, value); }
        }

        protected int CarryAsInt
        {
            get
            {
                return (this.SREG["C"] ? 1 : 0);
            }
        }

        protected int PC
        {
            get { return this.Controller.ProgramCounter; }
            set { this.Controller.ProgramCounter = value; }
        }

        #endregion

        #region Helper

        protected ushort GetWord(int lowReg)
        {
            return WordHelper.FromBytes(this.Controller.WorkingRegisters[lowReg], this.Controller.WorkingRegisters[lowReg + 1]);
        }

        protected void StoreWord(int lowReg, ushort value)
        {
            this.Controller.WorkingRegisters[lowReg].Value = WordHelper.GetLowByte(value).Value;
            this.Controller.WorkingRegisters[lowReg + 1].Value = WordHelper.GetHighByte(value).Value;
        }

        #endregion
    }

}
