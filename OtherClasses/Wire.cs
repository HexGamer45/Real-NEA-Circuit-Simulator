using System.Collections.Generic;
using System.Drawing;
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
        public Wire(List<Node> nodes, Circuit MainCircuit)
        {
            this.line = null;
            this.MainCircuit = MainCircuit;
            this.ConnectedNodes = nodes;

        }

        public void AddNode(Node node)
        {
            if (node == null) { return; }
            this.ConnectedNodes.Add(node);
            node.ConnectedWires.Add(this);
        }
        public void RenderWithOneNode()
        {
            this.ConnectedNodes[0].AddWire(this);
            Line newLine = new Line();
            newLine.X1 = Canvas.GetLeft(this.ConnectedNodes[0].image) + (int) this.ConnectedNodes[0].image.ActualWidth/2;
            newLine.Y1 = Canvas.GetTop(this.ConnectedNodes[0].image) + (int) this.ConnectedNodes[0].image.ActualHeight / 2;
            newLine.X2 = Mouse.GetPosition(this.MainCircuit.MainCanvas).X;
            newLine.Y2 = Mouse.GetPosition(this.MainCircuit.MainCanvas).Y;
            newLine.StrokeThickness= 3;
            newLine.Stroke = new SolidColorBrush(Colors.Black);
            this.MainCircuit.MainCanvas.Children.Add(newLine);
            this.line = newLine;
            if (!this.MainCircuit.WireToNodes.ContainsKey(this))
            {
                this.MainCircuit.WireToNodes.Add(this, new List<Node>());
                this.MainCircuit.WireToNodes[this].Add(ConnectedNodes[0]);
            }
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
            this.MainCircuit.WireToNodes.Remove(this);
            this.ConnectedNodes.Clear();
            
        }
        public void DeleteThisConnection()
        {
            foreach (Node node in this.ConnectedNodes)
            {
                node.ConnectedWires.Remove(this);
            }
            this.MainCircuit.MainCanvas.Children.Remove(this.line);
            this.line = null;
            this.MainCircuit.AdjacencyList[this.ConnectedNodes[0].ConnectedComponent].Remove(this.ConnectedNodes[1].ConnectedComponent);
            this.MainCircuit.AdjacencyList[this.ConnectedNodes[1].ConnectedComponent].Remove(this.ConnectedNodes[0].ConnectedComponent);
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
            if (!this.MainCircuit.AdjacencyList[this.ConnectedNodes[0].ConnectedComponent].Contains(node2.ConnectedComponent))
            {
                this.MainCircuit.AdjacencyList[this.ConnectedNodes[0].ConnectedComponent].Add(node2.ConnectedComponent);
            }

            if (!this.MainCircuit.AdjacencyList.ContainsKey(node2.ConnectedComponent))
            {
                this.MainCircuit.AdjacencyList.Add(node2.ConnectedComponent, new List<Component>());
            }
            if (!this.MainCircuit.AdjacencyList[node2.ConnectedComponent].Contains(this.ConnectedNodes[0].ConnectedComponent))
            {
                this.MainCircuit.AdjacencyList[node2.ConnectedComponent].Add(this.ConnectedNodes[0].ConnectedComponent);
            }
            this.MainCircuit.WireToNodes[this].Add(ConnectedNodes[1]);

        }
    }
}
