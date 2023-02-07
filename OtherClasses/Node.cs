using Real_NEA_Circuit_Simulator.OtherClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_NEA_Circuit_Simulator
{
    internal class Node
    {
        public Component ConnectedComponent {get; private set;}
        public List<Wire> ConnectedWires { get; private set;}
        public string name { get; private set; }
        public Node(string name,Component component)
        {
            this.name = name;
            this.ConnectedComponent = component;
            this.ConnectedWires = new List<Wire>();
        }

        public void AddWire(Wire wire)
        {
            this.ConnectedWires.Add(wire); 
        }

        public void AddWires(List<Wire> wires) 
        {
            foreach (Wire wire in wires) 
            {
                this.AddWire(wire);
            }
        }



    }
}
