using System;
using System.Collections.Generic;

namespace avrEmu
{
    public class AvrInstruction
    {
        public string Instruction { get; set; }

        public List<AvrInstrArg> Arguments  { get; set; }

        public AvrInstruction(string instruction, List<AvrInstrArg> args)
        {
            this.Instruction = instruction;
            this.Arguments = args;
        }

        public AvrInstruction (string asmString)
		{
			this.Arguments = new List<AvrInstrArg> ();
            ParseFromString(asmString);
        }

        private void ParseFromString(string instrStr)
        {
            string[] instrParts = instrStr.Split(
                new char[] {' ', ','},
                StringSplitOptions.RemoveEmptyEntries
            );

            this.Instruction = instrParts [0];

            for (int i = 1; i < instrParts.Length; i++)
                this.Arguments.Add(ParseArgFromString(instrParts [i]));
        }

        private AvrInstrArg ParseArgFromString(string argStr)
        {
            int parseOut;
            if (int.TryParse(argStr, out parseOut))
            {//numeric constant
                return new AvrInstrArgConst(parseOut);
            } else if (argStr [0] == 'r' && int.TryParse(
                argStr.Substring(1),
                out parseOut
            ))
            {//simple working register, eg: 'r16'
                return new AvrInstrArgRegister(parseOut);
            } else
            {//more computations neccessary
                string trimmed = argStr.Trim(new char[]
                {
                    '-',
                    '+',
                    '0',
                    '1',
                    '2',
                    '3',
                    '4',
                    '5',
                    '6',
                    '7',
                    '8',
                    '9'
                }
                );

                if (trimmed == "X" || trimmed == "Y" || trimmed == "Z")
                {
                    if (argStr.IndexOf('-') != -1)
                    {
                        return new AvrInstrArg16BReg(
                            trimmed [0],
                            AvrInstrArg16BType.PreDecrement
                        );
                    } else if (argStr.IndexOf('+') != -1)
                    {//it's postincrement
                        if (argStr.Length == 2)
                        {
                            return new AvrInstrArg16BReg(
                                trimmed [0],
                                avrEmu.AvrInstrArg16BType.PostIncrement
                            );
                        } else
                        {//so there's an offset
                            if (int.TryParse(
                                trimmed.Substring(2),
                                out parseOut
                            ))
                            {
                                return new AvrInstrArg16BReg(
                                    trimmed [0],
                                    avrEmu.AvrInstrArg16BType.Offset,
                                    parseOut
                                );
                            } else
                            {
                                //fail
                                throw new Exception("Invalid Offset!");
                            }
                        }
                    } else
                    {
                        //what now?
                        throw new Exception("Invalid Argument!");
                    }
                } else
                { // so it's probably a IO-Register
                    return new AvrInstrArgIOReg(argStr);
                }
            }
        }
    }
}

