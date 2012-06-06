using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avrEmu
{
    abstract class AvrAlu : AvrModule
    {
        protected delegate void Instruction(List<AvrInstrArg> args);

        protected class VI
        {
            public Instruction Exec;
            public List<AvrInstrArgType> Args;

            public VI(Instruction exec, List<AvrInstrArgType> args)
            {
                this.Exec = exec;
                this.Args = args;
            }

            public VI(Instruction exec)
            {
                this.Exec = exec;
                this.Args = new List<AvrInstrArgType>();
            }

            public VI(Instruction exec, AvrInstrArgType arg0)
            {
                this.Exec = exec;
                this.Args = new List<AvrInstrArgType>() { arg0 };
            }

            public VI(Instruction exec, AvrInstrArgType arg0, AvrInstrArgType arg1)
            {
                this.Exec = exec;
                this.Args = new List<AvrInstrArgType>() { arg0, arg1 };
            }

        }

        public AvrController Controller { get; protected set; }

        public ExtByte SREG { get; protected set; }

        protected int PC
        {
            get { return this.Controller.ProgramCounter; }
            set { this.Controller.ProgramCounter = value; }
        }


        protected int CarryAsInt
        {
            get
            {
                return (this.SREG["C"] ? 1 : 0);
            }
        }

        protected Dictionary<string, VI> InstructionSet = new Dictionary<string, VI>();

        public AvrAlu(AvrController controller)
        {
            this.Controller = controller;
            InitSREG();
        }

        protected virtual void InitSREG()
        {
            this.SREG = new ExtByte(0);

            this.SREG.BitNames = new List<string>() { "C", "Z", "N", "V", "S", "H", "T", "I" };

            this.SREG.BitEvents["N"].BitChanged += new ExtByte.BitChangedEventHandler(SREG_BitChanged);
            this.SREG.BitEvents["V"].BitChanged += new ExtByte.BitChangedEventHandler(SREG_BitChanged);

            this.IORegisters.Add("SREG", this.SREG);
        }

        protected virtual void SREG_BitChanged(object sender, BitChangedEventArgs e)
        {
            this.SREG["S"] = this.SREG["N"] ^ this.SREG["V"];
        }

        public virtual void ExecuteInstruction(AvrInstruction instruction)
        {
            if (instruction.Arguments.Count != this.InstructionSet[instruction.Instruction].Args.Count)
                throw new Exception("Invalid number of Arguments for Instruction!");

            for (int i = 0; i < instruction.Arguments.Count; i++)
            {
                if (instruction.Arguments[i].InstructionArgType != this.InstructionSet[instruction.Instruction].Args[i])
                    throw new Exception("Argument #" + i + " is of invalid Type!");
            }

            int oldPC = this.Controller.ProgramCounter;
            this.InstructionSet[instruction.Instruction].Exec(instruction.Arguments);
            if (this.Controller.ProgramCounter == oldPC)
                this.Controller.ProgramCounter++;
        }




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


    }

}
