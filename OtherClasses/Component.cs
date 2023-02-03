using Real_NEA_Circuit_Simulator.OtherClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_NEA_Circuit_Simulator
{
    internal class Component
    {
        public ComponentInfo InfoObject;
        public string name { get; private set; }
        public static Dictionary<string, Component> ComponentNames = new Dictionary<string, Component>();

        static Component()
        {
            Component LED = new(60f, 4.5f, "LED");
            ComponentNames.Add("LED",LED);
        }
        public Component(float resistance, float working_voltage, string name)
        {
            this.name = name;
            this.InfoObject = new ComponentInfo(resistance, working_voltage, this);
        }

        public Component Clone()
        {
            return new Component(this.InfoObject.Resistance, this.InfoObject.WorkingVoltage, this.name);
        }

    }
}
