using Real_NEA_Circuit_Simulator.OtherClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Real_NEA_Circuit_Simulator
{
    internal class Component
    {
        public List<Node> ConnectedNodes {get; private set;}
        public string name { get; private set; }
        public Component(string name)
        {
            this.name = name;
            this.ConnectedNodes = new List<Node>(){new Node(name+"0", this),new Node(name+"1", this)};
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
