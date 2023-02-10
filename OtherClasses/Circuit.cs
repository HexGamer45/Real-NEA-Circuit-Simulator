﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xaml;

namespace Real_NEA_Circuit_Simulator
{
    internal class Circuit
    {
        public string name { get; private set; }
        public List<Component> ComponentsList { get; private set; }
        public Dictionary<Component, List<Component>> AdjacencyList { get; private set; }
        public Dictionary<Wire, List<Node>> WireToNodes { get; private set; }
        public Circuit(string name)
        {
            this.name = name;
            this.ComponentsList = new();
            this.AdjacencyList = new();
            this.WireToNodes = new();

        }

        public void AddComponent(Component component)
        {
            this.ComponentsList.Add(component);
        }

        public void AddComponents(List<Component> components)
        {
            foreach (Component component in components) 
            {
                Node node0 = new Node(this.name + "0", component);
                Node node1 = new Node(this.name + "1", component);
                List<Node> nodes = new();
                nodes.Add(node0);
                nodes.Add(node1);
                component.AddNodes(nodes);
                this.AddComponent(component);
            }
        }

        public void ConnectComponent(Component component1, Component component2)
        {
            List<Node> nodes = new();
            nodes.Add(component1.ConnectedNodes[1]);
            nodes.Add(component2.ConnectedNodes[0]);
            Wire wire = new(component1.name + "-" + component2.name + "-wire", nodes);
            nodes[0].AddWire(wire);
            nodes[1].AddWire(wire);
            this.AdjacencyList[component1].Add(component2);
            this.AdjacencyList[component2].Add(component1);
            this.WireToNodes[wire].Add(nodes[0]);
            this.WireToNodes[wire].Add(nodes[1]);
        }

        private bool IsCyclic(Dictionary<Component, List<Component>> graph, Component start, Component component, HashSet<Component> visited, Component? prev)
        {
             foreach (Component neighbour in graph[component])
             {
                 if (!visited.Contains(neighbour))
                 {
                     if (this.IsCyclic(graph, start, neighbour, visited, component))
                     {
                         return true;
                     }
                 }
                 else if (neighbour!=prev && neighbour == start)
                 {
                     return true;
                 }
             }
             return false;
        }


        public Circuit RemoveNonCircuitComponents()
        {
            Dictionary<Component, List<Component>> circuit = new(this.AdjacencyList);
            List<Component> keylist = new(circuit.Keys);
            foreach (Component component in keylist)
            {
                if (!this.IsCyclic(circuit, component, component, new HashSet<Component>(), null))
                {
                    foreach (Component otherComponent in keylist)
                    {
                        foreach (Component neighbour in circuit[otherComponent])
                        {
                            if (neighbour == component)
                            {
                                circuit[otherComponent].Remove(neighbour);
                                break;
                            }
                        }
                    }
                }
            }

        }
    }
}