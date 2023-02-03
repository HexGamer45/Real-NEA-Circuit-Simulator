using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_NEA_Circuit_Simulator
{
    internal class Circuit
    {
        public string name {get; private set;}
        public List<Component> ComponentsList {get; private set;}
        public Dictionary<Component,List<Component>> AdjacencyList {get; private set;}
        public Dictionary<Wire, List<Node>> WireToNodes {get; private set;}
        public Circuit(string name) 
        { 
            this.AdjacencyList = new();
            this.WireToNodes = new();
}
