using System;
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
		
        static void Main(string[] args)
        {
            List<string> preprolist = new List<string>();
            Thread th = new Thread(new ThreadStart(delegate()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            )
            );
            th.Start();

            Console.ReadKey();
            Preprocessor prepro = new Preprocessor();
           preprolist=prepro.PreProcess("Q+((132+10)*10+(20-15))/5\n\r           asdf         daf 3*5+4       ;asdf\n\r.def temp = r16 \n\r         \n\rldi temp, HIGH(RAMEND)            ; HIGH-Byte der obersten RAM-Adresse\n\rout SPH, temp \n\rldi temp, LOW(RAMEND)             ; LOW-Byte der obersten RAM-Adresse\n\rout SPL, temp\n\r\n\rrcall sub1                        ; sub1 aufrufen\n\r\n\rloop:    rjmp loop \n\r\n\r\n\rsub1: \n\r                                          ; hier könnten ein paar Befehle stehen\n\r       rcall sub2                        ; sub2 aufrufen \n\r                                         ; hier könnten auch Befehle stehen\n\r       ret                               ; wieder zurück \n\r\n\rsub2:\n\r                                           ; hier stehen normalerweise die Befehle,\n\r                                           ; die in sub2 ausgeführt werden sollen\n\r         ret                               ; wieder zurück ");
           //preprolist = prepro.PreProcess("          asdf  daf 3*5+4       ;asdf");
           for (int i = 0; i < preprolist.Count; i++)
           {
               Console.WriteLine(preprolist[i]);
           }
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
