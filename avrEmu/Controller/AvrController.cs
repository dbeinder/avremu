using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avrEmu
{
    abstract class AvrController
    {
        public int ProgramCounter { get; set; }

        protected class ControllerModules : IEnumerable<AvrModule>
        {
            protected AvrController controller;
            protected List<AvrModule> modules = new List<AvrModule>();

            public ControllerModules(AvrController controller)
            {
                this.controller = controller;
            }

            public AvrModule this[int index]
            {
                get
                {
                    return modules[index];
                }

                set
                {
                    foreach (string regName in value.IORegisters.Keys)
                        this.controller.PeripheralRegisters.Remove(regName);

                    modules[index] = value;

                    foreach (KeyValuePair<string, ExtByte> register in value.IORegisters)
                        this.controller.PeripheralRegisters.Add(register.Key, register.Value);
                }
            }

            public void Add(AvrModule module)
            {
                modules.Add(module);

                foreach (KeyValuePair<string, ExtByte> register in module.IORegisters)
                    this.controller.PeripheralRegisters.Add(register.Key, register.Value);
            }

            public void Clear()
            {
                foreach (AvrModule module in this)
                    this.Remove(module);
            }

            public int Count
            {
                get { return modules.Count; }
            }

            public bool Remove(AvrModule module)
            {
                foreach (string regName in module.IORegisters.Keys)
                    this.controller.PeripheralRegisters.Remove(regName);

                return modules.Remove(module);
            }

            public IEnumerator<AvrModule> GetEnumerator()
            {
                return modules.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return modules.GetEnumerator();
            }
        }
        protected ControllerModules Modules;

        #region Standard Modules

        private AvrAlu alu;
        public AvrAlu ALU
        {
            get { return alu; }

            set
            {
                if (alu != null)
                    Modules.Remove(alu);
                alu = value;
                Modules.Add(value);
            }
        }

        private AvrProgramMemory programMemory;
        public AvrProgramMemory ProgramMemory
        {
            get { return programMemory; }

            set
            {
                if (programMemory != null)
                    Modules.Remove(programMemory);
                programMemory = value;
                Modules.Add(value);

            }
        }

        private AvrSram sram;
        public AvrSram SRAM
        {
            get { return sram; }

            set
            {
                if (sram != null)
                    Modules.Remove(sram);
                sram = value;
                Modules.Add(value);

            }
        }

        public class ControllerPorts : IEnumerable<KeyValuePair<char, AvrIOPort>>
        {
            protected AvrController controller;
            protected Dictionary<char, AvrIOPort> ports = new Dictionary<char, AvrIOPort>();

            public ControllerPorts(AvrController controller)
            {
                this.controller = controller;
            }

            AvrIOPort this[char name]
            {
                get { return ports[name]; }
                set
                {
                    controller.Modules.Remove(ports[name]);
                    ports[name] = value;
                    controller.Modules.Add(value);
                }
            }

            public IEnumerator<KeyValuePair<char, AvrIOPort>> GetEnumerator()
            {
                return ports.GetEnumerator();
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return ports.GetEnumerator();
            }

            public void Add(char key, AvrIOPort value)
            {
                controller.Modules.Add(value);
                ports.Add(key, value);
            }

            public ICollection<char> Keys
            {
                get { return ports.Keys; }
            }

            public bool Remove(char key)
            {
                controller.Modules.Remove(ports[key]);
                return ports.Remove(key);
            }

            public ICollection<AvrIOPort> Values
            {
                get { return ports.Values; }
            }

            public void Clear()
            {
                foreach (char key in ports.Keys)
                    Remove(key);
            }
        }
        public ControllerPorts Ports;

        #endregion

        public ExtByte[] WorkingRegisters { get; protected set; }

        public Dictionary<string, ExtByte> PeripheralRegisters { get; protected set; }

        //Controller-specific constants for the proprocessor, eg: RAMEND
        public Dictionary<string, string> Constants { get; protected set; }


        public AvrController()
        {
            this.PeripheralRegisters = new Dictionary<string, ExtByte>();
            this.Modules = new ControllerModules(this);
            this.Ports = new ControllerPorts(this);
        }

        public virtual void ClockTick()
        {
            foreach (AvrModule module in this.Modules)
                module.ClockTick();

            this.ALU.ExecuteInstruction(this.ProgramMemory.GetInstruction(this.ProgramCounter));
        }

        public virtual void Reset()
        {
            this.ProgramCounter = 0;
            ResetModules();
            ResetWorkingRegisters();
        }


        #region Init Helpers

        #endregion

        #region Reset Helpers

        protected virtual void ResetModules()
        {
            foreach (AvrModule module in this.Modules)
                module.Reset();
        }

        protected virtual void ResetWorkingRegisters()
        {
            for (int i = 0; i < this.WorkingRegisters.Length; i++)
            {
                this.WorkingRegisters[i].Value = 0;
            }
        }

        #endregion
    }
}
