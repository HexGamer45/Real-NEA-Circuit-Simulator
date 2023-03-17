using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses
{
    public class Cell : Component
    {
        public float Voltage { get; private set; }
        public Cell(string name, Circuit circuit) : base(name, circuit)
        {
            this.Resistance = 0f;
            this.Voltage = 3f;
        }
    }
}
