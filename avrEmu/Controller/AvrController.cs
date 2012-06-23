using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avrEmu
{
    abstract class AvrController
    {
        public int ProgramCounter { get; set; }

        protected List<AvrModule> Modules = new List<AvrModule>();

        #region Standard Modules

        public AvrAlu ALU { get; protected set; }

        public AvrProgramMemory ProgramMemory { get; set; }

        public AvrSram SRAM { get; protected set; } //todo: automated adding to modules

        public Dictionary<char, AvrIOPort> Ports { get; protected set; }

        #endregion

        public ExtByte[] WorkingRegisters { get; protected set; }

        public Dictionary<string, ExtByte> PeripheralRegisters { get; protected set; }

        //Controller-specific constants for the proprocessor, eg: RAMEND
        public Dictionary<string, string> Constants { get; protected set; }


        public virtual void ClockTick()
        {
            foreach (AvrModule module in this.Modules)
                module.ClockTick();

            this.ALU.ExecuteInstruction(this.ProgramMemory.GetInstruction(this.ProgramCounter));
        }

        public virtual void Reset()
        {
            this.ProgramCounter = 0;
            this.PeripheralRegisters = new Dictionary<string, ExtByte>();
            ResetModules();
            LoadIORegisters();
            ResetWorkingRegisters();
        }


        #region Init Helpers

        protected virtual void AddPortsToModules()
        {
            foreach (AvrIOPort port in this.Ports.Values)
                this.Modules.Add(port);
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

        #endregion

        #region Reset Helpers

        protected virtual void ResetModules()
        {
            foreach (AvrModule module in this.Modules)
                module.Reset();
        }

        protected virtual void ResetWorkingRegisters()
        {
            for (int i = 0; i < this.WorkingRegisters.Length; i++)
            {
                this.WorkingRegisters[i].Value = 0;
            }
        }

        #endregion
    }
}
