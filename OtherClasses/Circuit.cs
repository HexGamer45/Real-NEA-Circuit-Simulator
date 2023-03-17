﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xaml;

namespace Real_NEA_Circuit_Simulator
{
    public class Circuit
    {
        public Canvas MainCanvas { get; private set; }
        public string name { get; private set; }
        public List<Component> ComponentsList { get; private set; }
        public Dictionary<Component, List<Component>> AdjacencyList { get; private set; }
        public Dictionary<Wire, List<Node>> WireToNodes { get; private set; }
        public Circuit(string name, Canvas canvas)
        {
            this.MainCanvas = canvas;
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
            Wire wire = new(component1.name + "-" + component2.name + "-wire", nodes, this);
            nodes[0].AddWire(wire);
            nodes[1].AddWire(wire);
            this.AdjacencyList[component1].Add(component2);
            this.AdjacencyList[component2].Add(component1);
            this.WireToNodes[wire].Add(nodes[0]);
            this.WireToNodes[wire].Add(nodes[1]);
        }

        private bool IsCyclic(Dictionary<Component, List<Component>> graph, Component start, Component component, HashSet<Component> visited, Component? prev)
        {
            visited.Add(component);
            bool isCyclic = false;
            foreach (Component neighbour in graph[component])
            {
                if (!visited.Contains(neighbour))
                {
                    if (this.IsCyclic(graph, start, neighbour, visited, component))
                    {
                        isCyclic = true;
                    }
                }
                if (neighbour == start)
                {
                    int conns = 0;
                    foreach (Node startNode in start.ConnectedNodes)
                    {
                        foreach (Wire wire in startNode.ConnectedWires)
                        {
                            foreach (Node secondNode in wire.ConnectedNodes)
                            {
                                if (neighbour.ConnectedNodes.Contains(secondNode))
                                {
                                    conns++;
                                }
                            }
                        }
                    }
                    if (conns < 2)
                    {
                        return false;
                    }
                    return true;
                }
            }
            if (isCyclic)
            {
                return true;
            }
            return false;
        }


        public Dictionary<Component, List<Component>> RemoveNonCircuitComponents()
        {
            Dictionary<Component, List<Component>> circuit = new(this.AdjacencyList);
            List<Component> keylist = new(circuit.Keys);
            foreach (Component component in keylist)
            {
                if (circuit.Keys.Count == 2)
                {
                    int wireCount = 0;
                    foreach (Node node in circuit.Keys.ElementAt(0).ConnectedNodes)
                    {
                        foreach (Wire wire in node.ConnectedWires)
                        {
                            wireCount++;
                            foreach (Node node2 in wire.ConnectedNodes)
                            {
                                if (node != node2 && !circuit.ContainsKey(node2.ConnectedComponent))
                                {
                                    wireCount--;
                                }
                            }
                        }
                    }
                    if (wireCount == 2)
                    {
                        return circuit;
                    }
                    circuit.Clear();
                    return circuit;
                }
                if (!this.IsCyclic(circuit, component, component, new HashSet<Component>(), null))
                {
                    foreach (Component otherComponent in keylist)
                    {
                        if (otherComponent != component)
                        {
                            if (circuit.ContainsKey(otherComponent) && this.AdjacencyList[otherComponent].Contains(component))
                            {
                                circuit[otherComponent].Remove(component);
                            }
                        }
                    }
                    circuit.Remove(component);
                }
            }
            return circuit;

        }
    }
}