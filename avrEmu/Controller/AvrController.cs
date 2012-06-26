using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using avrEmu.Controller;

namespace avrEmu
{
    public abstract class AvrController
    {
        public int ProgramCounter { get; set; }

        public ControllerModules Modules { get; protected set; }

        #region Standard Modules

        private AvrAlu alu;
        public AvrAlu ALU
        {
            get { return alu; }

            set
            {
                if (alu != null)
                    Modules.Remove(alu);
                alu = value;
                Modules.Add(value);
            }
        }

        private AvrProgramMemory programMemory;
        public AvrProgramMemory ProgramMemory
        {
            get { return programMemory; }

            set
            {
                if (programMemory != null)
                    Modules.Remove(programMemory);
                programMemory = value;
                Modules.Add(value);

            }
        }

        private AvrSram sram;
        public AvrSram SRAM
        {
            get { return sram; }

            set
            {
                if (sram != null)
                    Modules.Remove(sram);
                sram = value;
                Modules.Add(value);

            }
        }

        public ControllerPorts Ports;

        #endregion

        protected const int WorkingRegisterCount = 32; //valid for avr family
        public ExtByte[] WorkingRegisters { get; protected set; }

        public Dictionary<string, ExtByte> PeripheralRegisters { get; protected set; }

        //Controller-specific constants for the proprocessor, eg: RAMEND
        public Dictionary<string, string> Constants { get; protected set; }


        public AvrController()
        {
            this.PeripheralRegisters = new Dictionary<string, ExtByte>();
            this.Modules = new ControllerModules(this);
            this.Ports = new ControllerPorts(this);

            this.WorkingRegisters = new ExtByte[WorkingRegisterCount];
            for (int i = 0; i < this.WorkingRegisters.Length; i++)
                this.WorkingRegisters[i] = new ExtByte(0);
        }

        public virtual void ClockTick()
        {
            foreach (AvrModule module in this.Modules)
                module.ClockTick();

            this.ALU.ExecuteInstruction(this.ProgramMemory.GetInstruction(this.ProgramCounter));
        }

        public virtual void Reset()
        {
            this.ProgramCounter = 0;
            ResetModules();
            ResetWorkingRegisters();
        }


        #region Reset Helpers

        protected virtual void ResetModules()
        {
            foreach (AvrModule module in this.Modules)
                module.Reset();
        }

        protected virtual void ResetWorkingRegisters()
        {
            for (int i = 0; i < WorkingRegisterCount; i++)
                this.WorkingRegisters[i].Value = 0;
        }

        #endregion
    }
}
