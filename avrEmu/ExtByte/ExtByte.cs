﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avrEmu
{
    #region BitChangedEvent

    public class BitChangedEventArgs : EventArgs
    {
        public ExtByte ChangedByte;
        public int ChangedBitNr;
        public bool NewValue;

        public BitChangedEventArgs(ExtByte bt, int bitNr, bool newValue)
        {
            this.ChangedByte = bt;
            this.ChangedBitNr = bitNr;
            this.NewValue = newValue;
        }
    }

    public delegate void BitChangedEventHandler(object sender, BitChangedEventArgs e);

    #endregion

    #region ByteChangedEvent

    public class ByteChangedEventArgs : EventArgs
    {
        public ExtByte ChangedByte;

        public ByteChangedEventArgs(ExtByte bt)
        {
            this.ChangedByte = bt;
        }
    }

    public delegate void ByteChangedEventHandler(object sender, ByteChangedEventArgs e);

    #endregion

    public class ExtByte : IComparable<ExtByte>, IEquatable<ExtByte>
    {
        public event ByteChangedEventHandler ByteChanged;

        #region BitEvents

        public BitEventsWrapper BitEvents;

        public class BitEvent
        {
            public event BitChangedEventHandler BitChanged;

            public void FireEvents(object sender, BitChangedEventArgs e)
            {
                if (this.BitChanged != null)
                    this.BitChanged(sender, e);
            }
        }

        public class BitEventsWrapper
        {
            private List<BitEvent> eventsClasses = new List<BitEvent>() 
            {
                new BitEvent(), new BitEvent(), new BitEvent(), new BitEvent(),
                new BitEvent(), new BitEvent(), new BitEvent(), new BitEvent()
            };
            private ExtByte extByte;

            public BitEventsWrapper(ExtByte extByte)
            {
                this.extByte = extByte;
            }

            public BitEvent this[int index]
            {
                get
                {
                    if (index < 0 || index > 7)
                        throw new Exception("Invalid BitIndex for ExtByte");

                    return eventsClasses[index];
                }
            }

            public BitEvent this[string bitName]
            {
                get
                {
                    if (this.extByte.BitNumbers.Contains(bitName))
                        return this.eventsClasses[this.extByte.BitNumbers[bitName]];
                    else
                        throw new Exception("BitName not found!");
                }
            }
        }

        #endregion

        #region Bit Names and Numbers

        public List<string> BitNames
        {
            get
            {
                return this.bitNames;
            }
            set
            {
                this.bitNumbers.Clear();

                for (int i = 0; i < 8; i++)
                    this.bitNumbers.Add(value[i], i);

                this.bitNames = value;
            }
        }

        public BitNamesWrapper BitNumbers;

        protected List<string> bitNames = new List<string>();

        protected Dictionary<string, int> bitNumbers = new Dictionary<string, int>();

        public class BitNamesWrapper
        {
            private Dictionary<string, int> source;

            public BitNamesWrapper(Dictionary<string, int> source)
            {
                this.source = source;
            }

            public int this[string bitName]
            {
                get
                {
                    if (!source.ContainsKey(bitName))
                        throw new Exception("BitName not found!");

                    return source[bitName];
                }
            }

            public bool Contains(string bitName)
            {
                return source.ContainsKey(bitName);
            }
        }

        #endregion

        protected byte internalValue;


        public byte Value
        {
            get
            {
                return this.internalValue;
            }
            set
            {
                ExtByte old = new ExtByte(this.Value);
                this.internalValue = value;

                for (int i = 0; i < 8; i++)
                {
                    if (this[i] != old[i])
                        this.BitEvents[i].FireEvents(
                            this,
                            new BitChangedEventArgs(this, i, this[i])
                        );
                }
                if (ByteChanged != null)
                    ByteChanged(this, new ByteChangedEventArgs(this));
            }
        }

        public ExtByte(byte b)
        {
            this.internalValue = b;
            this.BitNumbers = new BitNamesWrapper(this.bitNumbers);
            this.BitEvents = new BitEventsWrapper(this);
        }

        public ExtByte(bool bit0, bool bit1, bool bit2, bool bit3, bool bit4, bool bit5, bool bit6, bool bit7)
        {
            this.internalValue = 0;
            this[0] = bit0;
            this[1] = bit1;
            this[2] = bit2;
            this[3] = bit3;
            this[4] = bit4;
            this[5] = bit5;
            this[6] = bit6;
            this[7] = bit7;
            this.BitNumbers = new BitNamesWrapper(this.bitNumbers);
            this.BitEvents = new BitEventsWrapper(this);
        }

        #region Bit-wise access

        public bool this[int index]
        {
            get
            {
                if (index < 0 || index > 7)
                    throw new Exception("Invalid BitIndex for ExtByte");

                return (this.Value & (byte)(1 << index)) != 0;
            }

            set
            {
                if (index < 0 || index > 7)
                    throw new Exception("Invalid BitIndex for ExtByte");

                if (value)
                    this.Value |= (byte)(1 << index);
                else
                    this.Value &= (byte)~(1 << index);

                this.BitEvents[index].FireEvents(
                        this,
                        new BitChangedEventArgs(this, index, value)
                );

                if (ByteChanged != null)
                    ByteChanged(this, new ByteChangedEventArgs(this));
            }
        }

        public bool this[string key]
        {
            get
            {
                if (this.BitNumbers.Contains(key))
                    return this[this.BitNumbers[key]];
                else
                    throw new Exception("Bit Name not found!");
            }
            set
            {
                if (this.BitNumbers.Contains(key))
                    this[this.BitNumbers[key]] = value;
                else
                    throw new Exception("Bit Name not found!");
            }
        }

        #endregion

        public int CompareTo(ExtByte other)
        {
            return other.Value - this.Value;
        }

        public bool Equals(ExtByte other)
        {
            return this.Value == other.Value;
        }

        public override string ToString()
        {
            string output = "Byte:" + this.internalValue.ToString() + " [";
            for (int i = 0; i < 8; i++)
            {
                if (this.BitNames.Count > i)
                    output += this.BitNames[i];
                else
                    output += "B" + i.ToString();

                output += "=" + (this[i] ? "1, " : "0, ");
            }
            return output.Substring(0, output.Length - 2) + "]";
        }

        #region Convenient but dangerous
        //creates a new obj -> Bit/ByteChangeEvent stop working

        /* public static implicit operator byte(ExtByte bt)
        {
            return bt.Value;
        }

        public static implicit operator ExtByte(byte bt)
        {
            return new ExtByte(bt);
        }*/
        #endregion
    }
}
