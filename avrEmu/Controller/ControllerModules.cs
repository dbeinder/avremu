using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace avrEmu.Controller
{
   public class ControllerModules : IEnumerable<AvrModule>
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
}
