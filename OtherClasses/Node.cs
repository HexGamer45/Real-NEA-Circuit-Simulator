using System.Collections.Generic;
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
        public Node(Component component)
        {
            this.ConnectedComponent = component;
            this.ConnectedWires = new List<Wire>();
            this.image = null;
        }

        public void AddWire(Wire wire)
        {
            this.ConnectedWires.Add(wire); 
        }
        public void RenderFirst(Point position)
        {
            Image image = new Image();
            if (this.ConnectedComponent.ConnectedNodes[0] == this)
            {
                image.Source = ((BitmapImage)Application.Current.FindResource("redNode"));
            }
            else
            {
                image.Source = ((BitmapImage)Application.Current.FindResource("blueNode"));
            }
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
        public void Move(Point position, int inpOut)
        {
            if (this.image != null)
            {
                if (this.ConnectedComponent.rotation == 0)
                {
                    if (inpOut == 0)
                    {
                        Canvas.SetLeft(this.image, position.X - this.image.ActualWidth/2);
                        Canvas.SetTop(this.image, position.Y + this.ConnectedComponent.image.Source.Height /2 - this.image.ActualHeight / 2);
                    }
                    else
                    {
                        Canvas.SetLeft(this.image, position.X - this.image.ActualWidth / 2 + this.ConnectedComponent.image.Source.Width);
                        Canvas.SetTop(this.image, position.Y + this.ConnectedComponent.image.Source.Height / 2 - this.image.ActualHeight / 2);
                    }
                }
                else if(this.ConnectedComponent.rotation == 90)
                {
                    if (inpOut == 0)
                    {
                        Canvas.SetLeft(this.image, position.X + this.ConnectedComponent.image.Source.Width / 2 - this.image.ActualWidth / 2);
                        Canvas.SetTop(this.image, position.Y - this.image.ActualHeight / 2);
                    }
                    else
                    {
                        Canvas.SetLeft(this.image, position.X + this.ConnectedComponent.image.Source.Width / 2 - this.image.ActualWidth / 2);
                        Canvas.SetTop(this.image, position.Y - this.image.ActualHeight / 2 + this.ConnectedComponent.image.Source.Height);
                    }
                }
                else if (this.ConnectedComponent.rotation == 180)
                {
                    if (inpOut == 1)
                    {
                        Canvas.SetLeft(this.image, position.X - this.image.ActualWidth / 2);
                        Canvas.SetTop(this.image, position.Y + this.ConnectedComponent.image.Source.Height / 2 - this.image.ActualHeight / 2);
                    }
                    else
                    {
                        Canvas.SetLeft(this.image, position.X - this.image.ActualWidth / 2 + this.ConnectedComponent.image.Source.Width);
                        Canvas.SetTop(this.image, position.Y + this.ConnectedComponent.image.Source.Height / 2 - this.image.ActualHeight / 2);
                    }
                }
                else if (this.ConnectedComponent.rotation == 270)
                {
                    if (inpOut == 1)
                    {
                        Canvas.SetLeft(this.image, position.X);
                        Canvas.SetTop(this.image, position.Y + this.ConnectedComponent.image.Source.Width / 2 - this.image.ActualHeight);
                    }
                    else
                    {
                        Canvas.SetLeft(this.image, position.X + this.ConnectedComponent.image.Source.Width / 2 - this.image.ActualWidth / 2);
                        Canvas.SetTop(this.image, position.Y - this.image.ActualHeight / 2 + this.ConnectedComponent.image.Source.Height);
                    }
                }
                foreach (Wire wire in this.ConnectedWires)
                {
                    if (wire.ConnectedNodes[0] == this)
                    {
                        wire.line.X1 = Canvas.GetLeft(this.image) + (int)this.image.ActualWidth / 2;
                        wire.line.Y1 = Canvas.GetTop(this.image) + (int)this.image.ActualHeight / 2;
                    }
                    else
                    {
                        wire.line.X2 = Canvas.GetLeft(this.image) + (int)this.image.ActualWidth / 2;
                        wire.line.Y2 = Canvas.GetTop(this.image) + (int)this.image.ActualHeight / 2;
                    }
                }
            }
        }
    }
}
