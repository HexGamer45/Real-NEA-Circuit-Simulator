using Real_NEA_Circuit_Simulator.OtherClasses;
using Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using Point = System.Drawing.Point;
using Size = System.Windows.Size;

namespace Real_NEA_Circuit_Simulator
{
    public class Component
    {
        public int rotation { get; protected set; }
        public bool Active { get; protected set; }
        public float Resistance { get; protected set; }
        public Circuit MainCircuit { get; protected set;}
        public List<Node> ConnectedNodes {get; protected set;}
        public string name { get; protected set; }
        public Image? image { get; protected set; }
        public Component(string name, Circuit circuit)
        {
            this.rotation = 0;
            this.image = null;
            this.MainCircuit = circuit;
            this.name = name;
            this.ConnectedNodes = new List<Node>() { new Node(name + "0", this), new Node(name + "1", this) };
            this.MainCircuit.AdjacencyList.Add(this, new List<Component>());
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
                if (position.Y > this.MainCircuit.MainCanvas.ActualHeight - this.image.ActualHeight - this.ConnectedNodes[0].image.ActualHeight / 2) { position.Y = (int)(this.MainCircuit.MainCanvas.ActualHeight - this.image.ActualHeight - this.ConnectedNodes[0].image.ActualHeight / 2); }
                else if (position.Y < this.ConnectedNodes[0].image.ActualHeight / 2) { position.Y = (int)this.ConnectedNodes[0].image.ActualHeight / 2; }
                Canvas.SetLeft(this.image, position.X);
                Canvas.SetTop(this.image, position.Y);
                this.ConnectedNodes[0].Move(position, 0);
                this.ConnectedNodes[1].Move(position, 1);
            }
        }

        public float getVoltageAvailable(float totalVoltage, float totalResistance)
        {
            float accessedVolts = totalVoltage * this.Resistance / totalResistance;
            return accessedVolts;
        }
        public float getPowerAvailable(float totalVoltage, float totalResistance)
        {
            float power = (float)Math.Pow(this.getVoltageAvailable(totalVoltage,totalResistance), 2) / this.Resistance;
            return power;
        }

        public virtual void PerformComponentFunction(float totalVoltage, float totalResistance)
        {
            this.Active = true;
        }

        public virtual void DisableComponentFunction()
        {
            this.Active = false;
        }

        public void SetName(string name)
        {
            this.name = name;
        }

        public void SetResistance(float resistance)
        {
            this.Resistance = resistance;
        }

        public virtual void SetVoltage(float voltage)
        {
            return;
        }

        public void SetActive(bool active)
        {
            this.Active = active;
        }

        public void Rotate(int degrees = 90)
        {
            this.rotation += degrees;
            while (this.rotation > 270)
            {
                this.rotation -= 360;
            }
            RotateTransform rotation = new RotateTransform(degrees) {CenterX=0.5,CenterY=0.5 };
            if (this.image.Source is TransformedBitmap)
            {
                TransformedBitmap transformBmp = new TransformedBitmap();
                transformBmp.BeginInit();
                transformBmp.Source = (BitmapSource)this.image.Source;
                transformBmp.Transform = rotation;
                transformBmp.EndInit();
                this.image.Source = transformBmp;
            }
            else
            {
                TransformedBitmap transformBmp = new TransformedBitmap();
                transformBmp.BeginInit();
                transformBmp.Source = (BitmapImage)this.image.Source;
                transformBmp.Transform = rotation;
                transformBmp.EndInit();
                this.image.Source = transformBmp;
            }
            Point position = new Point((int)Canvas.GetLeft(this.image), (int)Canvas.GetTop(this.image));
            this.ConnectedNodes[0].Move(position, 0);
            this.ConnectedNodes[1].Move(position, 1);
        }

    }
}
