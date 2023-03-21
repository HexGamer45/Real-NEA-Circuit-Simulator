using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses;

namespace Real_NEA_Circuit_Simulator.OtherClasses
{
    public class ComponentDisplayData
    {
        public Component Component;
        public string name;
        public float resistance;
        public float voltage;
        public bool active;

        public string Name 
        {
            get
            {
                return name;
            } set
            {
                name = value;
                ComponentDescribing.SetName(name);
            }
        }
        public float Resistance
        {
            get
            {
                return resistance;
            }
            set
            {
                resistance = value;
                ComponentDescribing.SetResistance(resistance);
            }
        }
        public float Voltage
        {
            get
            {
                return voltage;
            }
            set
            {
                voltage = value;
                ComponentDescribing.SetVoltage(voltage);
            }
        }
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
                ComponentDescribing.SetActive(active);
            }
        }

        public Component ComponentDescribing { get; private set; }

        public ComponentDisplayData(Component component)
        {
            this.Component = component;
            name = component.name;
            resistance = component.Resistance;
            if (component is Cell)
            {
                voltage = ((Cell)component).Voltage;
            }
            else
            {
                voltage = 0;
            }
            active = component.Active;
            this.ComponentDescribing = component;
        }
    }
}
