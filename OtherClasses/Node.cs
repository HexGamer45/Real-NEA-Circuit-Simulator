﻿using Real_NEA_Circuit_Simulator.OtherClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using Point = System.Drawing.Point;

namespace Real_NEA_Circuit_Simulator
{
    public class Node
    {
        public Image? image { get; private set;}
        public Component ConnectedComponent {get; private set;}
        public List<Wire> ConnectedWires { get; private set;}
        public string name { get; private set; }
        public Node(string name,Component component)
        {
            this.name = name;
            this.ConnectedComponent = component;
            this.ConnectedWires = new List<Wire>();
            this.image = null;
        }

        public void AddWire(Wire wire)
        {
            this.ConnectedWires.Add(wire); 
        }

        public void AddWires(List<Wire> wires) 
        {
            foreach (Wire wire in wires) 
            {
                this.AddWire(wire);
            }
        }

        public void RenderFirst(Point position)
        {
            Image image = new Image();
            string NodeColour = "red";
            if (this.ConnectedComponent.ConnectedNodes[0] == this)
            {
                NodeColour = "blue";
            }
            //image.Source = this.ConnectedComponent.MainCircuit.MainCanvas. --use xaml resources
            double width = image.Source.Width;
            image.Tag = this;
            Canvas canvas = this.ConnectedComponent.MainCircuit.MainCanvas;
            canvas.Children.Add(image);
            image.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            image.Arrange(new Rect(0, 0, image.DesiredSize.Width, image.DesiredSize.Height));
            position.X -= (int)image.ActualWidth / 2;
            position.Y -= (int)image.ActualHeight / 2;

            Canvas.SetLeft(image, position.X);
            Canvas.SetTop(image, position.Y);
            this.image = image;
        }

        public void Move(Point position)
        {
            if (this.image != null)
            {
                position.X -= (int)image.ActualWidth / 2;
                if (position.X > this.ConnectedComponent.MainCircuit.MainCanvas.ActualWidth - this.image.ActualWidth) { position.X = (int)(this.ConnectedComponent.MainCircuit.MainCanvas.ActualWidth - this.image.ActualWidth); }
                else if (position.X < 0) { position.X = 0; }
                position.Y -= (int)image.ActualHeight / 2;
                if (position.Y > this.ConnectedComponent.MainCircuit.MainCanvas.ActualHeight - this.image.ActualHeight) { position.Y = (int)(this.ConnectedComponent.MainCircuit.MainCanvas.ActualHeight - this.image.ActualHeight); }
                else if (position.Y < 0) { position.Y = 0; }
                Canvas.SetLeft(this.image, position.X);
                Canvas.SetTop(this.image, position.Y);
            }
        }
    }
}
