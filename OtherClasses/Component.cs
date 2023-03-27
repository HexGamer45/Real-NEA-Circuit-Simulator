using System;
using System.Collections.Generic;
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
            this.ConnectedNodes = new List<Node>() { new Node(this), new Node(this) };
            this.MainCircuit.AdjacencyList.Add(this, new List<Component>());
        }

        #region General
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
        public void Rotate(int degrees = 90)
        {
            this.rotation += degrees;
            while (this.rotation > 270)
            {
                this.rotation -= 360;
            }
            RotateTransform rotation = new RotateTransform(degrees) { CenterX = 0.5, CenterY = 0.5 };
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
            Point position = new Point((int)(Canvas.GetLeft(this.image)), (int)(Canvas.GetTop(this.image)));
            this.ConnectedNodes[0].Move(position, 0);
            this.ConnectedNodes[1].Move(position, 1);
        }
        public void Delete()
        {
            foreach (Node node in this.ConnectedNodes)
            {
                List<Wire> wiresToDelete = new List<Wire>();
                foreach (Wire wire in node.ConnectedWires)
                {
                    wiresToDelete.Add(wire);
                }
                foreach (Wire wire in wiresToDelete)
                {
                    wire.DeleteThisConnection();
                }
                this.MainCircuit.MainCanvas.Children.Remove(node.image);
            }
            if (this.MainCircuit.AdjacencyList.ContainsKey(this))
            {
                this.MainCircuit.AdjacencyList.Remove(this);
            }
            if (this.MainCircuit.ComponentsList.Contains(this))
            {
                this.MainCircuit.ComponentsList.Remove(this);
            }
            this.ConnectedNodes.Clear();
            this.MainCircuit.MainCanvas.Children.Remove(this.image);
            if (this.MainCircuit.ComponentsList.Contains(this))
            {
                this.MainCircuit.ComponentsList.Remove(this);
            }

        }
        public void SetName(string name)
        {
            this.name = name;
        }
        public void SetResistance(float resistance)
        {
            this.Resistance = resistance;
        }
        /*SetVoltage can be overidden by subclasses (polymorphism); Cells need to return their
          Given voltage. 
         */
        public virtual void SetVoltage(float voltage)
        {
            return;
        }
        public void SetActive(bool active)
        {
            this.Active = active;
        }
        #endregion

        #region Simulation
        //These two dynamically return the available voltage and power for the component.
        protected float getVoltageAvailable(float totalVoltage, float totalResistance)
        {
            if (this.Resistance <= 0)
            {
                return 0;
            }
            float accessedVolts = totalVoltage * this.Resistance / totalResistance;
            return accessedVolts;
        }
        protected float getPowerAvailable(float totalVoltage, float totalResistance)
        {
            if (this.Resistance <= 0 )
            {
                return 0;
            }
            float power = (float)Math.Pow(this.getVoltageAvailable(totalVoltage,totalResistance), 2) / this.Resistance;
            return power;
        }
        //These are base methods, then they are added to later if a subclass of component needs to.
        public virtual void PerformComponentFunction(float totalVoltage, float totalResistance)
        {
            this.Active = true;
        }
        public virtual void DisableComponentFunction()
        {
            this.Active = false;
        }
        #endregion
    }
}
