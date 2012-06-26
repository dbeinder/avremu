using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avrEmu
{
    //The only code specific to the AtTiny2313
    class AtTiny2313 : AvrController
    {
        private const int CapacitySRAM = 128;

        public AtTiny2313()
            : base()
        {
            this.Ports.Add(new AvrIOPort('A', 3));
            this.Ports.Add(new AvrIOPort('B', 8));
            this.Ports.Add(new AvrIOPort('C', 7));

            this.ALU = new AtTinyAlu(this);
            this.ProgramMemory = new AvrProgramMemoryFlash();
            this.SRAM = new AvrSram(CapacitySRAM);

            this.Constants = new Dictionary<string, string>() 
            {
                { "ramend", "127" }
            };

            Reset();
        }
    }

}
