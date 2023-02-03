using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_NEA_Circuit_Simulator.OtherClasses
{
    internal class ComponentInfo
    {
        public float Resistance {get; private set;}
        public float WorkingVoltage {get; private set;}
        public bool Active {get; private set;}

        public ComponentInfo(float resistance, float workingVoltage, Component componentDescribing)
        {
            this.Active = false
            this.Resistance = resistance;
            this.WorkingVoltage = workingVoltage;
        }
    }
}