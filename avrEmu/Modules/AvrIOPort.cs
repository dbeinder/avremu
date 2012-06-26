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
        public int PinCount { get; protected set; }
        public char PortCharacter { get; protected set; }

        public AvrIOPort(char name, int pinCount)
        {
            this.IORegisters.Add("DDR" + name, this.DDR);
            this.IORegisters.Add("PORT" + name, this.PORT);
            this.IORegisters.Add("PIN" + name, this.PIN);
            this.PortCharacter = name;

            for (int i = 0; i < pinCount; i++)
                Pins.Add(new AvrIOPin(DDR, PORT, PIN, i));

            this.PinCount = pinCount;
        }

        public override void Reset()
        {
            this.DDR.Value = this.PORT.Value = this.PIN.Value = 0;
        }
    }

    public class AvrIOPin
    {
        private ExtByte Ddr, Port, Pin;

        public bool IsOutput
        {
            get
            {
                return this.Ddr[PinNumber];
            }
        }

        public bool OutputValue
        {
            get
            {
                return this.Port[PinNumber];
            }
        }

        public bool InputValue
        {
            get
            {
                return this.Pin[PinNumber];
            }

            set
            {
                this.Pin[PinNumber] = value;
            }
        }

        public int PinNumber;

        public event BitChangedEventHandler PinChanged;


        public AvrIOPin(ExtByte ddr, ExtByte port, ExtByte pin, int bitNum)
        {
            this.Ddr = ddr;
            this.Port = port;
            this.Pin = pin;
            this.PinNumber = bitNum;

            ddr.BitEvents[bitNum].BitChanged += new BitChangedEventHandler(AvrIOPin_BitChanged);
            port.BitEvents[bitNum].BitChanged += new BitChangedEventHandler(AvrIOPin_BitChanged);
            pin.BitEvents[bitNum].BitChanged += new BitChangedEventHandler(AvrIOPin_BitChanged);
        }

        void AvrIOPin_BitChanged(object sender, BitChangedEventArgs e)
        { 
            //relaying event
            if (PinChanged != null)
                PinChanged(this, new BitChangedEventArgs(e.ChangedByte, this.PinNumber, e.NewValue));
        }
    }
}

