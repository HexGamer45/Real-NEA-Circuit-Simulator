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
        public List<Node> ConnectedNodes {get; private set;}
        public string name { get; private set; }
        public Wire(string name,List<Node> nodes)
        {
            this.name = name;
            this.ConnectedNodes = nodes;

        }

        public void AddNode(Node node)
        {
            this.ConnectedNodes.Add(node);
        }

        public void AddNodes(List<Node> nodes)
        {
            foreach (Node node in nodes)
            {
                this.AddNode(node);
            }
        }

    }
}
