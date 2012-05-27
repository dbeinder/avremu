using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avrEmu
{
    abstract class AvrController
    {
        public AvrAlu ALU { get; protected set; }

        public AvrProgramMemory ProgramMemory { get; protected set; }

        public AvrSram SRAM { get; protected set; }

        public int ProgramCounter { get; set; }

        public ExtByte[] WorkingRegisters { get; protected set; }

        public Dictionary<string, ExtByte> PeripheralRegisters { get; protected set; }

        protected List<AvrModule> Modules = new List<AvrModule>();
        
        protected virtual void Reset()
        {
            this.ProgramCounter = 0;
            this.PeripheralRegisters = new Dictionary<string, ExtByte>();
            ResetModules();
            LoadIORegisters();
            ResetWorkingRegisters();
        }
        
        protected virtual void ResetModules()
        {
            foreach (AvrModule module in this.Modules)
                module.Reset();
        }
        
        protected virtual void ResetWorkingRegisters()
        {
            for (int i = 0; i < this.WorkingRegisters.Length; i++)
            {
                this.WorkingRegisters [i] = new ExtByte(0);
            }
        }
        
        protected virtual void LoadIORegisters()
        {
            foreach (AvrModule module in this.Modules)
            {
                foreach (KeyValuePair<string, ExtByte> register in module.IORegisters)
                {
                    this.PeripheralRegisters.Add(register.Key, register.Value);
                }
            }
        }

        public virtual void ClockTick()
        {
            foreach (AvrModule module in this.Modules)
                module.ClockTick();

            this.ALU.ExecuteInstruction(this.ProgramMemory.GetInstruction(this.ProgramCounter));
        }
    }

}
