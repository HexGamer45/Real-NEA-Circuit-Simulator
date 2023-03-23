using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Real_NEA_Circuit_Simulator
{
    public class Circuit
    {
        public Canvas MainCanvas { get; private set; }
        public List<Component> ComponentsList { get; private set; }
        public Dictionary<Component, List<Component>> AdjacencyList { get; private set; }
        public Dictionary<Wire, List<Node>> WireToNodes { get; private set; }
        public Circuit(Canvas canvas)
        {
            this.MainCanvas = canvas;
            this.ComponentsList = new();
            this.AdjacencyList = new();
            this.WireToNodes = new();

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