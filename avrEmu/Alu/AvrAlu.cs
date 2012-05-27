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
        
        protected Dictionary<string, VI> InstructionSet = new Dictionary<string, VI>();
        
        public AvrAlu(AvrController controller)
        {
            this.Controller = controller;
            InitSREG();
        }
        
        protected virtual void InitSREG()
        {
            this.SREG = new ExtByte(0);
            
            this.SREG.BitNames = new List<string>()
                { "C", "Z", "N", "V", "S", "H", "T", "I" };
            
            this.SREG.BitEvents ["N"].BitChanged += new ExtByte.BitChangedEventHandler(SREG_BitChanged);
            this.SREG.BitEvents ["V"].BitChanged += new ExtByte.BitChangedEventHandler(SREG_BitChanged);
            
            this.IORegisters.Add("SREG", this.SREG);
        }

        protected virtual void SREG_BitChanged(object sender, BitChangedEventArgs e)
        {
            this.SREG ["S"] = this.SREG ["N"] ^ this.SREG ["V"];
        }
        
        public virtual void ExecuteInstruction(AvrInstruction instruction)
        {
            if (instruction.Arguments.Count != this.InstructionSet [instruction.Instruction].Args.Count)
                throw new Exception("Invalid number of Arguments for Instruction!");
                    
            for (int i = 0; i < instruction.Arguments.Count; i++)
            {
                if (instruction.Arguments [i].InstructionArgType != this.InstructionSet [instruction.Instruction].Args [i])
                    throw new Exception("Argument #" + i + " is of invalid Type!");
            }
            
            this.InstructionSet [instruction.Instruction].Exec(instruction.Arguments);
        }
            
        protected byte CutAndSetCarry(int number)
        {
            byte newValue = (byte)number;
            
            this.SREG ["C"] = (number != newValue);
            
            return newValue;
        }

        protected void SetZeroNegativeFlag(byte bt)
        {
            this.SREG ["Z"] = (bt == 0);
            this.SREG ["N"] = ((new ExtByte(bt)) [7] == true);
        }
    }

}
