using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avrEmu
{
    class Program
    {
		static string[] instrs = new string[]
		{
			"ldi r14, 120",
			"ldi r15, 5",
			"add r14, r15"
		};
		
        static void Main (string[] args)
		{
			AvrController atny = new AtTiny2313 ();
			
			AvrPMFormLink fakeFlash = atny.ProgramMemory as AvrPMFormLink;
			fakeFlash.FetchInstruction += delegate(object sender, FetchInstructionEventArgs e) {
				e.Instruction = new AvrInstruction(instrs[e.InstructionNr]);
            };

            for (;;)
                atny.ClockTick();
			
            Console.ReadKey();
        }
    }
}
