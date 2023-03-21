using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses
{
    public class Battery : Cell
    {
        public Battery(string name, Circuit circuit) : base(name, circuit)
        {
            this.Voltage = 6f;
        }

    }
}
