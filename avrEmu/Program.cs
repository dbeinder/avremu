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
        static void Main(string[] args)
        {
            Thread th = new Thread(new ThreadStart(delegate()
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }));
            th.Start();

            //TESTCODE IN HERE 

            Console.ReadKey();

            List<string> preprolist = new List<string>();
            Preprocessor prepro = new Preprocessor();
            preprolist = prepro.PreProcess("Q+((132+10)*10+(20-15))/5\n\r           asdf         daf 3*5+4       ;asdf\n\r.def temp = r16 \n\r         \n\rldi temp, HIGH(RAMEND)            ; HIGH-Byte der obersten RAM-Adresse\n\rout SPH, temp \n\rldi temp, LOW(RAMEND)             ; LOW-Byte der obersten RAM-Adresse\n\rout SPL, temp\n\r\n\rrcall sub1                        ; sub1 aufrufen\n\r\n\rloop:    rjmp loop \n\r\n\r\n\rsub1: \n\r                                          ; hier könnten ein paar Befehle stehen\n\r       rcall sub2                        ; sub2 aufrufen \n\r                                         ; hier könnten auch Befehle stehen\n\r       ret                               ; wieder zurück \n\r\n\rsub2:\n\r                                           ; hier stehen normalerweise die Befehle,\n\r                                           ; die in sub2 ausgeführt werden sollen\n\r         ret                               ; wieder zurück ");


            for (int i = 0; i < preprolist.Count; i++)
            {
                Console.WriteLine(preprolist[i]);
            }


            //END TESTCODE
            Console.ReadKey();
            th.Abort();
        }
    }
}
