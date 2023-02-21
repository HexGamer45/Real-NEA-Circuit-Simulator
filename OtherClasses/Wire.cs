﻿﻿using Real_NEA_Circuit_Simulator.OtherClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Real_NEA_Circuit_Simulator
{
    public class Wire
    {
        public Line? line { get; private set; }
        public Circuit MainCircuit { get; private set; } 
        public List<Node> ConnectedNodes {get; private set;}
        public string name { get; private set; }
        public Wire(string name,List<Node> nodes, Circuit MainCircuit)
        {
            this.line = null;
            this.MainCircuit = MainCircuit;
            this.name = name;
            this.ConnectedNodes = nodes;

        }

        public void AddNode(Node node)
        {
            this.ConnectedNodes.Add(node);
            node.ConnectedWires.Add(this);
        }

        public void AddNodes(List<Node> nodes)
        {
            foreach (Node node in nodes)
            {
                this.AddNode(node);
            }
        }

        public void RenderWithOneNode()
        {
            Line newLine = new Line();
            newLine.X1 = Canvas.GetLeft(this.ConnectedNodes[0].image) + (int) this.ConnectedNodes[0].image.ActualWidth/2;
            newLine.Y1 = Canvas.GetTop(this.ConnectedNodes[0].image) + (int) this.ConnectedNodes[0].image.ActualHeight / 2;
            newLine.X2 = Mouse.GetPosition(this.MainCircuit.MainCanvas).X;
            newLine.Y2 = Mouse.GetPosition(this.MainCircuit.MainCanvas).Y;
            newLine.StrokeThickness= 3;
            newLine.Stroke = new SolidColorBrush(Colors.Black);
            this.MainCircuit.MainCanvas.Children.Add(newLine);
            this.line = newLine;
            this.MainCircuit.WireToNodes.Add(this, new List<Node>());
            this.MainCircuit.WireToNodes[this].Add(ConnectedNodes[0]);
        }

        public void MoveOneNodeLine(Point position2)
        {
            if (this.line == null) { return; }
            this.line.X2 = position2.X;
            this.line.Y2 = position2.Y;
        }

        public void RemoveLine()
        {
            this.ConnectedNodes[0].ConnectedWires.Remove(this);
            this.MainCircuit.MainCanvas.Children.Remove(this.line);
            this.line = null;
            if (this.MainCircuit.AdjacencyList.ContainsKey(this.ConnectedNodes[0].ConnectedComponent) && this.ConnectedNodes.Count >=2)
            {
                this.MainCircuit.AdjacencyList[this.ConnectedNodes[0].ConnectedComponent].Remove(this.ConnectedNodes[1].ConnectedComponent);
                this.MainCircuit.WireToNodes.Remove(this);
                this.ConnectedNodes.Clear();
            }
            
        }

        public void DeleteThisConnection()
        {
            this.ConnectedNodes[0].ConnectedWires.Remove(this);
            this.ConnectedNodes[1].ConnectedWires.Remove(this);
            this.MainCircuit.MainCanvas.Children.Remove(this.line);
            this.line = null;
            this.MainCircuit.AdjacencyList[this.ConnectedNodes[0].ConnectedComponent].Remove(this.ConnectedNodes[1].ConnectedComponent);
            this.MainCircuit.WireToNodes.Remove(this);
            this.ConnectedNodes.Clear();
        }

        public void ConnectSecondNode(Node node2)
        {
            if (this.line == null) { return; }
            if (node2.ConnectedComponent == this.ConnectedNodes[0].ConnectedComponent || (node2 != node2.ConnectedComponent.ConnectedNodes[0] && this.ConnectedNodes[0] != this.ConnectedNodes[0].ConnectedComponent.ConnectedNodes[0]) || (node2 != node2.ConnectedComponent.ConnectedNodes[1] && this.ConnectedNodes[0] != this.ConnectedNodes[0].ConnectedComponent.ConnectedNodes[1]))
            {
                this.RemoveLine();
                return;
            }
            this.line.X2 = Canvas.GetLeft(node2.image) + (int) node2.image.ActualWidth / 2;
            this.line.Y2 = Canvas.GetTop(node2.image) + (int) node2.image.ActualHeight / 2;
            this.AddNode(node2);
            if (!this.MainCircuit.AdjacencyList.ContainsKey(this.ConnectedNodes[0].ConnectedComponent))
            {
                this.MainCircuit.AdjacencyList.Add(this.ConnectedNodes[0].ConnectedComponent, new List<Component>());
            }
            this.MainCircuit.AdjacencyList[this.ConnectedNodes[0].ConnectedComponent].Add(this.ConnectedNodes[1].ConnectedComponent);
            this.MainCircuit.WireToNodes[this].Add(ConnectedNodes[1]);
        }

    }
}
