using System;
namespace avrEmu
{
    public class AvrIOPort:AvrModule
    {
        public ExtByte DDR = new ExtByte(0);
        public ExtByte PORT = new ExtByte(0);
        public ExtByte PIN = new ExtByte(0);

        public AvrIOPort(char name, int pinCount)
        {
            this.IORegisters.Add("DDR" + name, this.DDR);
            this.IORegisters.Add("PORT" + name, this.PORT);
            this.IORegisters.Add("PIN" + name, this.PIN);

            this.DDR.ByteChanged += new ExtByte.ByteChangedEventHandler(DDR_ByteChanged);
            this.DDR.BitEvents[4].BitChanged += new ExtByte.BitChangedEventHandler(AvrIOPort_BitChanged);
        }

        void AvrIOPort_BitChanged(object sender, BitChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void DDR_ByteChanged(object sender, ByteChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public override void Reset()
        {
        }
    }

    public class AvrIOPin : AvrModule
    {
        public AvrIOPin(int index)
        {

        }
    }
}

