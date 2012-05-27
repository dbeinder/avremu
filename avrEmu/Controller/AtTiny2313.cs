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

        public AtTiny2313 ()
		{
			this.ALU = new AtTinyAlu (this);
			this.ProgramMemory = new AvrPMFormLink ();
			this.SRAM = new AvrSram (CapacitySRAM);
			this.Modules.Add (this.ALU);
			this.Modules.Add (this.ProgramMemory);
			this.Modules.Add (this.SRAM);

			this.ProgramCounter = 0;
			this.PeripheralRegisters = new Dictionary<string, ExtByte> ();
			this.WorkingRegisters = new ExtByte[WorkingRegisterCount];
			ResetWorkingRegisters ();
            LoadIORegisters();
        }


    }

}
