using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace avrEmu
{
    public class AvrIOPort : AvrModule
    {
        private ExtByte DDR = new ExtByte(0);
        private ExtByte PORT = new ExtByte(0);
        private ExtByte PIN = new ExtByte(0);

        public List<AvrIOPin> Pins = new List<AvrIOPin>();


        public AvrIOPort(char name, int pinCount)
        {
            this.IORegisters.Add("DDR" + name, this.DDR);
            this.IORegisters.Add("PORT" + name, this.PORT);
            this.IORegisters.Add("PIN" + name, this.PIN);

            for (int i = 0; i < pinCount; i++)
            {
                Pins.Add(new AvrIOPin(DDR, PORT, PIN, i));
            }
        }

        public override void Reset()
        {
            //alle auf 0
        }
    }

    public class AvrIOPin : AvrModule
    {
        private ExtByte Ddr, Port, Pin;
        private int bitNum;

        public event BitChangedEventHandler PinChanged;

        public AvrIOPin(ExtByte ddr, ExtByte port, ExtByte pin, int bitNum)
        {
            this.Ddr = ddr;
            this.Port = port;
            this.Pin = pin;
            this.bitNum = bitNum;

            ddr.BitEvents[bitNum].BitChanged += new BitChangedEventHandler(AvrIOPin_BitChanged);
            port.BitEvents[bitNum].BitChanged += new BitChangedEventHandler(AvrIOPin_BitChanged);
            pin.BitEvents[bitNum].BitChanged += new BitChangedEventHandler(AvrIOPin_BitChanged);
        }

        void AvrIOPin_BitChanged(object sender, BitChangedEventArgs e)
        {
            if (PinChanged != null)
                PinChanged(this, new BitChangedEventArgs(e.ChangedByte, this.bitNum, e.NewValue));
        }

        public bool IsOutput
        {
            get
            {
                return this.Ddr[bitNum];
            }
        }


        public bool OutputValue
        {
            get
            {
                return this.Port[bitNum];
            }
        }

        public bool InputValue
        {
            get
            {
                return this.Pin[bitNum];
            }

            set
            {
                this.Pin[bitNum] = value;
            }
        }
    }
}

