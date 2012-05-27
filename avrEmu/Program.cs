using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avrEmu
{
	class Program
	{
		static List<AvrInstruction> instrs = new List<AvrInstruction> () 
		{
			new AvrInstruction("ldi r14, 120"),
			new AvrInstruction("ldi r15, 150"),
			new AvrInstruction ("add r14, r15")
		};
		
		static int pc = 0;
		
		static void Main (string[] args)
		{
			
			ExtByte bt = new ExtByte (8);
			bt.BitNumbers ["jkl"] = 6;
			
			AvrController atny = new AtTiny2313 ();
			
			AvrPMFormLink fakeFlash = atny.ProgramMemory as AvrPMFormLink;
			fakeFlash.FetchInstruction += delegate(object sender, FetchInstructionEventArgs e) {
				e.Instruction = instrs [pc];
				pc = (pc+1) % instrs.Count;
			};
			
			
			for (;;)
				atny.ClockTick ();
			
			Console.ReadKey ();
		}
	}
}
