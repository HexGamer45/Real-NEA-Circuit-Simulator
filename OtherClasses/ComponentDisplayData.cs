using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses;

namespace Real_NEA_Circuit_Simulator.OtherClasses
{
    public class ComponentDisplayData
    {
        public string Name;
        public float Resistance;
        public float Voltage;
        public bool Active;

        public ComponentDisplayData(Component component)
        {

            this.Name = component.name;
            this.Resistance = component.Resistance;
            if (component is Cell)
            {
                this.Voltage = ((Cell)component).Voltage;
            }
            else
            {
                this.Voltage = 0;
            }
            this.Active = component.Active;
        }
    }
}
