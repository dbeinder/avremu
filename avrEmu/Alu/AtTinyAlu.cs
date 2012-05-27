using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avrEmu
{
	class AtTinyAlu : AvrAlu
	{
		public AtTinyAlu (AvrController controller)
            : base(controller)
		{
			this.InstructionSet = new Dictionary<string, VI> () {
				{ "add", new VI(this.Add, AvrInstrArgType.WorkingRegister, AvrInstrArgType.WorkingRegister) },	
				{ "ldi", new VI(this.Ldi, AvrInstrArgType.WorkingRegister, AvrInstrArgType.NumericConstant) },
				{ "nop", new VI(this.Nop) }
			};
		
		}
       
		#region Arithmetics and Logic 
		
		protected void Add (List<AvrInstrArg> args)
		{
			ExtByte rd = this.Controller.WorkingRegisters [(args [0] as AvrInstrArgRegister).Register];
			ExtByte rr = this.Controller.WorkingRegisters [(args [1] as AvrInstrArgRegister).Register];
			
			rd.Value = CutAndSetCarry (rd.Value + rr.Value);
			SetZeroNegativeFlag (rd.Value);
		}
		
		
	
		#endregion

		#region Branch
		
		#endregion
		
		#region Bit and Bit-Test
		
		#endregion
		
		#region Data Transfer
		
		protected void Ldi (List<AvrInstrArg> args)
		{
			ExtByte rd = this.Controller.WorkingRegisters [(args [0] as AvrInstrArgRegister).Register];
			int k = (args [1] as AvrInstrArgConst).Constant;
			
			rd.Value = (byte)k;
		}
		#endregion
		
	
		#region MCU Control
		
		protected void Nop(List<AvrInstrArg> args)
		{
			//No Operation
		}
		
		#endregion
	}

}
