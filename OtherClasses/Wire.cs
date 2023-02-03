using Real_NEA_Circuit_Simulator.OtherClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_NEA_Circuit_Simulator
{
    internal class Wire
    {
        public Node[] ConnectedNodes {get; private set;}
        public string name { get; private set; }
        public Wire(string name,Node[] nodes)
        {
            this.name = name;
            this.ConnectedNodes = nodes
        }

    }
}
