using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using avrEmu.Controller;

namespace avrEmu.Controller
{
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

        public void Add(AvrIOPort value)
        {
            controller.Modules.Add(value);
            ports.Add(value.PortCharacter, value);
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
}
