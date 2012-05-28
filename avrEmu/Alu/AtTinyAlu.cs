using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avrEmu
{
    class AtTinyAlu : AvrAlu
    {
        public AtTinyAlu(AvrController controller)
            : base(controller)
        {
            this.InstructionSet = new Dictionary<string, VI>() {
                //Arithmetics and Logic 
                { "add", new VI(this.Add, AvrInstrArgType.WorkingRegister, AvrInstrArgType.WorkingRegister) }, 
                { "adc", new VI(this.Adc, AvrInstrArgType.WorkingRegister, AvrInstrArgType.WorkingRegister) },
                { "adiw", new VI(this.Adiw, AvrInstrArgType.WorkingRegister, AvrInstrArgType.NumericConstant) },

                //Branch

                //Bit and Bit-Test

                //Data Transfer
                { "mov", new VI(this.Mov, AvrInstrArgType.WorkingRegister, AvrInstrArgType.WorkingRegister) },
                { "movw", new VI(this.Movw, AvrInstrArgType.WorkingRegister, AvrInstrArgType.WorkingRegister) },
                { "ldi", new VI(this.Ldi, AvrInstrArgType.WorkingRegister, AvrInstrArgType.NumericConstant) },
                { "in", new VI(this.In, AvrInstrArgType.WorkingRegister, AvrInstrArgType.IORegister) },
                { "out", new VI(this.Out, AvrInstrArgType.IORegister, AvrInstrArgType.WorkingRegister) },
                 
                //MCU Control
                { "nop", new VI(this.Nop) }
            };
        
        }
       
        #region Arithmetics and Logic 
       
        protected void Add(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters [(args [0] as AvrInstrArgRegister).Register];
            ExtByte rr = this.Controller.WorkingRegisters [(args [1] as AvrInstrArgRegister).Register];

            this.SREG ["H"] = ((rd.Value & 0x0f) + (rr.Value & 0x0f)) > 15;
            rd.Value = SetFlags(rd.Value + rr.Value, SregFlags.CZNV);
        }

        protected void Adc(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters [(args [0] as AvrInstrArgRegister).Register];
            ExtByte rr = this.Controller.WorkingRegisters [(args [1] as AvrInstrArgRegister).Register];

            this.SREG ["H"] = ((rd.Value & 0x0f) + (rr.Value & 0x0f) + this.CarryAsInt) > 15;
            rd.Value = SetFlags(
                rd.Value + rr.Value + this.CarryAsInt,
                SregFlags.CZNV
            );
        }

        protected void Adiw(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters [(args [0] as AvrInstrArgRegister).Register];
            int k = (args [1] as AvrInstrArgConst).Constant;


        }
    
        #endregion

        #region Branch
        
        #endregion
        
        #region Bit and Bit-Test
        
        #endregion
        
        #region Data Transfer

        protected void Mov(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters [(args [0] as AvrInstrArgRegister).Register];
            ExtByte rr = this.Controller.WorkingRegisters [(args [1] as AvrInstrArgRegister).Register];
            
            rd.Value = rr.Value;
        }

        protected void Movw(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters [(args [0] as AvrInstrArgRegister).Register];
            ExtByte rdHigh = this.Controller.WorkingRegisters [(args [0] as AvrInstrArgRegister).Register + 1];
            ExtByte rr = this.Controller.WorkingRegisters [(args [1] as AvrInstrArgRegister).Register];
            ExtByte rrHigh = this.Controller.WorkingRegisters [(args [1] as AvrInstrArgRegister).Register + 1];
            
            rd.Value = rr.Value;
            rdHigh.Value = rrHigh.Value;
        }

        protected void Ldi(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters [(args [0] as AvrInstrArgRegister).Register];
            int k = (args [1] as AvrInstrArgConst).Constant;
            
            rd.Value = (byte)k;
        }

        protected void In(List<AvrInstrArg> args)
        {
            ExtByte rd = this.Controller.WorkingRegisters [(args [0] as AvrInstrArgRegister).Register];
            ExtByte ior = this.Controller.PeripheralRegisters [(args [1] as AvrInstrArgIOReg).IORegister]; 

            rd.Value = ior.Value;
        }

        protected void Out(List<AvrInstrArg> args)
        {
            ExtByte ior = this.Controller.PeripheralRegisters [(args [0] as AvrInstrArgIOReg).IORegister]; 
            ExtByte rd = this.Controller.WorkingRegisters [(args [1] as AvrInstrArgRegister).Register];

            ior.Value = rd.Value;
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
