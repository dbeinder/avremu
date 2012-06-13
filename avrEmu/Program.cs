﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

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
            //(new MainForm()).
            Thread th = new Thread(new ThreadStart(delegate()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
                
            }));
            th.Start();

            Preprocessor prepro = new Preprocessor();
            prepro.PreProcess("Q+((132+10)*10+(20-15))/5");
			AvrController atny = new AtTiny2313 ();
			
			AvrPMFormLink fakeFlash = atny.ProgramMemory as AvrPMFormLink;
			fakeFlash.FetchInstruction += delegate(object sender, FetchInstructionEventArgs e) {
                int iNr = e.InstructionNr % instrs.Length;
				e.Instruction = new AvrInstruction(instrs[iNr]);
            };

            DateTime start = DateTime.Now.Add(new TimeSpan(0,0,10));
            int cnt = 0;

            for (; ; )
            {
                cnt++;
                atny.ClockTick();
                if(DateTime.Now > start)
                    break;
            }
            Console.WriteLine((double)cnt/10.0);
            Console.ReadKey();
        }
    }
}
