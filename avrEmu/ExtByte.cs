﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avrEmu
{
	public class ByteChangedEventArgs : EventArgs
	{
		public ExtByte ChangedByte;

		public ByteChangedEventArgs (ExtByte bt)
		{
			this.ChangedByte = bt;
		}
	}

	public class BitChangedEventArgs : EventArgs
	{
		public ExtByte ChangedByte;
		public int ChangedBitNr;
		public bool NewValue;

		public BitChangedEventArgs (ExtByte bt, int bitNr, bool newValue)
		{
			this.ChangedByte = bt;
			this.ChangedBitNr = bitNr;
			this.NewValue = newValue;
		}
	}

	public class ExtByte : IComparable<ExtByte>, IEquatable<ExtByte>
	{
		public delegate void ByteChangedEventHandler (object sender,ByteChangedEventArgs e);

		public delegate void BitChangedEventHandler (object sender,BitChangedEventArgs e);

		public event ByteChangedEventHandler ByteChanged;
		public event BitChangedEventHandler BitChanged;

		public Dictionary<string, int> BitNames = new Dictionary<string, int> ();
		protected byte _value;

		public byte Value {
			get {
				return this._value;
			}
			set {
				ExtByte old = new ExtByte (this.Value);
				this._value = value;

				for (int i = 0; i < 8; i++) {
					if (this [i] != old [i])
					if (BitChanged != null)
						BitChanged (
							this,
							new BitChangedEventArgs (this, i, this [i])
						);
				}
				if (ByteChanged != null)
					ByteChanged (this, new ByteChangedEventArgs (this));
			}
		}

		public ExtByte (byte b)
		{
			this._value = b;
		}

		public ExtByte (bool bit0, bool bit1, bool bit2, bool bit3, bool bit4, bool bit5, bool bit6, bool bit7)
		{
			this._value = 0;
			this [0] = bit0;
			this [1] = bit1;
			this [2] = bit2;
			this [3] = bit3;
			this [4] = bit4;
			this [5] = bit5;
			this [6] = bit6;
			this [7] = bit7;
		}

		public bool this [int index] {
			get {
				if (index < 0 || index > 7)
					throw new Exception ("Invalid BitIndex for ExtByte");

				return (this.Value & (byte)(1 << index)) != 0;
			}

			set {
				if (index < 0 || index > 7)
					throw new Exception ("Invalid BitIndex for ExtByte");

				if (value)
					this.Value |= (byte)(1 << index);
				else
					this.Value &= (byte)~(1 << index);
				
				if (BitChanged != null)
					BitChanged (this, new BitChangedEventArgs (this, index, value));
				
				if (ByteChanged != null)
					ByteChanged (this, new ByteChangedEventArgs (this));
			}
		}

		public bool this [string key] {
			get {
				if (this.BitNames.ContainsKey (key))
					return this [this.BitNames [key]];
				else
					throw new Exception ("Bit Name not found!");
			}
			set {
				if (this.BitNames.ContainsKey (key))
					this [this.BitNames [key]] = value;
				else
					throw new Exception ("Bit Name not found!");
			}
		}

		public int CompareTo (ExtByte other)
		{
			return other.Value - this.Value;
		}

		public bool Equals (ExtByte other)
		{
			return this.Value == other.Value;
		}

		/* public static implicit operator byte(ExtByte bt)
        {
            return bt.Value;
        }

        public static implicit operator ExtByte(byte bt)
        {
            return new ExtByte(bt);
        }*/


	}
}
