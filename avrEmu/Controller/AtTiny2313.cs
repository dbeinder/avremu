using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avrEmu
{
    class AtTiny2313 : AvrController
    {
        private const int WorkingRegisterCount = 32;
        private const int CapacitySRAM = 128;

        public AtTiny2313()
        {
            this.WorkingRegisters = new ExtByte[WorkingRegisterCount];
            for (int i = 0; i < this.WorkingRegisters.Length; i++)
                this.WorkingRegisters[i] = new ExtByte(0);

            this.ALU = new AtTinyAlu(this);
            this.ProgramMemory = new AvrProgramMemoryFlash();
            this.SRAM = new AvrSram(CapacitySRAM);
            this.Modules.Add(this.ALU);
            this.Modules.Add(this.ProgramMemory);
            this.Modules.Add(this.SRAM);

            this.Constants = new Dictionary<string, string>() 
            {
                { "ramend", "127" }
            };

            Reset();
        }


    }

}
