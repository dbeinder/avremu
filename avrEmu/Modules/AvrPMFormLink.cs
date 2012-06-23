using System;

namespace avrEmu
{
    public class FetchInstructionEventArgs : EventArgs
    {
        public int InstructionNr;
        public AvrInstruction Instruction;

        public FetchInstructionEventArgs(int instrNr)
        {
            this.InstructionNr = instrNr;
        }
    }

    public class AvrPMFormLink : AvrProgramMemory
    {
        public delegate void FetchInstructionEventHandler(object sender, FetchInstructionEventArgs e);

        public event FetchInstructionEventHandler FetchInstruction;

        public AvrPMFormLink()
        {
        }

        public override AvrInstruction GetInstruction(int at)
        {
            FetchInstructionEventArgs e = new FetchInstructionEventArgs(at);
            FetchInstruction(this, e);
            return e.Instruction;
        }
    }
}

