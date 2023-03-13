using Real_NEA_Circuit_Simulator.OtherClasses;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Point = System.Drawing.Point;
using Size = System.Windows.Size;

namespace Real_NEA_Circuit_Simulator
{
    public class Component
    {
        public float Resistance { get; protected set; }
        public Circuit MainCircuit { get; protected set;}
        public List<Node> ConnectedNodes {get; protected set;}
        public string name { get; protected set; }
        public Image? image { get; protected set; }
        public Component(string name, Circuit circuit)
        {
            this.image = null;
            this.MainCircuit = circuit;
            this.name = name;
            this.ConnectedNodes = new List<Node>() { new Node(name + "0", this), new Node(name + "1", this) };
        }

        public void SetResistance(float resistance)
        {
            this.Resistance = resistance;
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


        public void RenderFirst(Point position)
        {
            Image image = new Image();
            image.Source = (BitmapImage) Application.Current.FindResource(this.GetType().Name);
            image.Tag = this;
            Canvas canvas = this.MainCircuit.MainCanvas;
            canvas.Children.Add(image);
            image.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            image.Arrange(new Rect(0, 0, image.DesiredSize.Width, image.DesiredSize.Height));
            position.X -= (int)image.ActualWidth / 2;
            position.Y -= (int)image.ActualHeight / 2;
            Canvas.SetLeft(image, position.X);
            Canvas.SetTop(image, position.Y);
            this.image = image;
            for (int i = 0; i < this.ConnectedNodes.Count; i ++)
            {
                Node node = this.ConnectedNodes[i];
                int direction;
                if (i == 0) {direction = -1; }
                else {direction = 1; }
                Point nodePosition = new Point(position.X + (int) (image.ActualWidth / 2 * direction + image.ActualWidth/2), position.Y + ((int)image.ActualHeight / 2));
                node.RenderFirst(nodePosition);
                
            }
        }

        public void Move(Point position)
        {
            if (this.image != null)
            {
                position.X -= (int)this.image.ActualWidth / 2;
                if (position.X > this.MainCircuit.MainCanvas.ActualWidth - this.image.ActualWidth - this.ConnectedNodes[0].image.ActualWidth / 2) { position.X = (int)(this.MainCircuit.MainCanvas.ActualWidth - this.image.ActualWidth - this.ConnectedNodes[0].image.ActualWidth / 2); }
                else if (position.X < this.ConnectedNodes[0].image.ActualWidth / 2) { position.X = (int)this.ConnectedNodes[0].image.ActualWidth / 2; }
                position.Y -= (int)this.image.ActualHeight / 2;
                if (position.Y > this.MainCircuit.MainCanvas.ActualHeight - this.image.ActualHeight) { position.Y = (int)(this.MainCircuit.MainCanvas.ActualHeight - this.image.ActualHeight); }
                else if (position.Y < 0) { position.Y = 0; }
                Canvas.SetLeft(this.image, position.X);
                Canvas.SetTop(this.image, position.Y);
                for (int i = 0; i < this.ConnectedNodes.Count; i++)
                {
                    Node node = this.ConnectedNodes[i];
                    int direction;
                    if (i == 0) { direction = -1; }
                    else { direction = 1; }
                    Point nodePosition = new Point(position.X + (int)(this.image.ActualWidth / 2 * direction + image.ActualWidth / 2), position.Y + ((int)this.image.ActualHeight / 2));
                    node.Move(nodePosition);

                }

                foreach (Node node in this.ConnectedNodes)
                {
                    foreach (Wire wire in node.ConnectedWires)
                    {
                        if (wire.ConnectedNodes[0] == node)
                        {
                            wire.line.X1 = Canvas.GetLeft(node.image) + (int)node.image.ActualWidth / 2;
                            wire.line.Y1 = Canvas.GetTop(node.image) + (int)node.image.ActualHeight / 2;
                        }
                        else
                        {
                            wire.line.X2 = Canvas.GetLeft(node.image) + (int)node.image.ActualWidth / 2;
                            wire.line.Y2 = Canvas.GetTop(node.image) + (int)node.image.ActualHeight / 2;
                        }
                    }
                }
            }
        }

        public float VoltageChange(float current)
        {
            return -this.Resistance * current;
        }


    }
}
