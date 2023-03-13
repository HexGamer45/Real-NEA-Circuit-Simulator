using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses
{
    public class LED : Component
    {
        public LED(string name, Circuit circuit) :base(name, circuit)
        {
            this.Resistance = 0f;
        }


    }
}
