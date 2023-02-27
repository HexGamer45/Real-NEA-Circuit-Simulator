using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses
{
    public class FixedResistor : Component
    {
        public ComponentInfo componentInfo {get; private set;}
        public FixedResistor(string name, Circuit circuit) : base(name, circuit)
        {
            this.componentInfo = new ComponentInfo(50, 5, this);
        }
        
        

    }
}
