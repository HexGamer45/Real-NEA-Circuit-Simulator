﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Windows.Shapes;
using Point = System.Drawing.Point;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace Real_NEA_Circuit_Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Workspace : Window
    {
        public Circuit MainCircuit {get; private set;}
        private Image? SelectedComponent;
        private Image? SelectedNode;
        private Wire? SelectedWire;
        private bool MouseInCanvas;
        private bool MiddleDown;
        private bool LeftDown;
        private float MiddleRelativeX;
        private float MiddleRelativeY;

        public Workspace()
        {
            InitializeComponent();
            this.MainCircuit = new Circuit("Main", MainCanvas);
            this.SelectedWire = null;
            this.SelectedComponent = null;
            this.SelectedNode = null;
            this.MouseInCanvas = false;
            this.LeftDown = false;
            this.MiddleDown = false;
            this.MiddleRelativeX = 0;
            this.MiddleRelativeY = 0;
        }

        private void GenerateNewComponent(object sender, RoutedEventArgs e)
        {
            Button button = (Button) sender;
            string tag = (string)button.Tag;
            int count = 0;
            foreach (Component component in this.MainCircuit.ComponentsList)
            {
                if (component.type == tag)
                {
                    count++;
                }
            }
            Component newComponent = new Component(tag+$"{count.ToString()}", tag, this.MainCircuit);
            this.MainCircuit.ComponentsList.Add(newComponent);
            Point position = new Point((int)MainCanvas.ActualWidth/2, (int)MainCanvas.ActualHeight/2);
            newComponent.RenderFirst(position);
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                this.MiddleDown = true;
                this.SelectedComponent = this.GetClosestComponent();
                Image? closestImage = this.SelectedComponent;
                if (closestImage != null)
                {
                    this.MiddleRelativeX = (float) (Mouse.GetPosition(MainCanvas).X - (Canvas.GetLeft(closestImage) + closestImage.ActualWidth / 2));
                    this.MiddleRelativeY = (float) (Mouse.GetPosition(MainCanvas).Y - (Canvas.GetTop(closestImage) + closestImage.ActualHeight / 2));
                }

                Grid_MouseMove(sender, e);
            }
            else if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.LeftDown = true;
                this.SelectedNode = this.GetClosestNode();
                Image? closestImage = this.SelectedNode;
                if (closestImage != null)
                {
                    Wire newWire = new Wire(((Node)closestImage.Tag).name + "-temp", new List<Node>() {(Node)closestImage.Tag}, this.MainCircuit);
                    newWire.RenderWithOneNode();
                    newWire.AddNode((Node)closestImage.Tag);
                    this.SelectedWire = newWire;
                }
            }
        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.MiddleDown)
            {
                this.MiddleDown = false;
                this.SelectedComponent = null;
            }
            if (this.LeftDown)
            {
                this.LeftDown = false;
                if (this.SelectedWire != null)
                {
                    Image? closestNodeImage = GetClosestNode();
                    if (closestNodeImage != null)
                    {
                        Node closestNode = (Node)closestNodeImage.Tag;
                        this.SelectedWire.ConnectSecondNode(closestNode);
                    }
                    else { this.SelectedWire.RemoveLine(); }
                    this.SelectedWire = null;
                }
            }
        }

        private Image? GetClosestComponent()
        {
            float mouseX = (float)Mouse.GetPosition(MainCanvas).X;
            float mouseY = (float)Mouse.GetPosition(MainCanvas).Y;
            float closestMagnitude = float.PositiveInfinity;
            Image? closestImage = null;
            foreach (var obj in MainCanvas.Children)
            {
                Image? image = obj as Image;
                if (image != null)
                {
                    if (image.Tag is Component)
                    {
                        float centreX = (float)(Canvas.GetLeft(image) + image.ActualWidth / 2);
                        float centreY = (float)(Canvas.GetTop(image) + image.ActualHeight / 2);
                        if ((mouseX <= centreX + image.ActualWidth / 2 && mouseX >= centreX - image.ActualWidth / 2) && (mouseY <= centreY + image.ActualHeight / 2 && mouseY >= centreY - image.ActualHeight / 2))
                        {
                            if (Math.Pow((Math.Pow((centreX - mouseX), 2) + Math.Pow((centreY - mouseY), 2)), 0.5) < closestMagnitude)
                            {
                                closestMagnitude = (float)Math.Pow((Math.Pow((centreX - mouseX), 2) + Math.Pow((centreY - mouseY), 2)), 0.5);
                                closestImage = image;
                            }
                        }
                    }
                }
            }
            return closestImage;
        }

        private Image? GetClosestNode()
        {
            float mouseX = (float)Mouse.GetPosition(MainCanvas).X;
            float mouseY = (float)Mouse.GetPosition(MainCanvas).Y;
            float closestMagnitude = float.PositiveInfinity;
            Image? closestImage = null;
            foreach (var obj in MainCanvas.Children)
            {
                Image? image = obj as Image;
                if (image != null)
                {
                    if (image.Tag is Node)
                    {
                        float centreX = (float)(Canvas.GetLeft(image) + image.ActualWidth / 2);
                        float centreY = (float)(Canvas.GetTop(image) + image.ActualHeight / 2);
                        if ((mouseX <= centreX + image.ActualWidth / 2 && mouseX >= centreX - image.ActualWidth / 2) && (mouseY <= centreY + image.ActualHeight / 2 && mouseY >= centreY - image.ActualHeight / 2))
                        {
                            if (Math.Pow((Math.Pow((centreX - mouseX), 2) + Math.Pow((centreY - mouseY), 2)), 0.5) < closestMagnitude)
                            {
                                closestMagnitude = (float)Math.Pow((Math.Pow((centreX - mouseX), 2) + Math.Pow((centreY - mouseY), 2)), 0.5);
                                closestImage = image;
                            }
                        }
                    }
                }
            }
            return closestImage;
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed && this.MouseInCanvas)
            {
                Image? closestImage = this.SelectedComponent;
                if (closestImage != null)
                {
                    Component closestComponent = (Component)closestImage.Tag;
                    closestComponent.Move(new Point((int) (Mouse.GetPosition(MainCanvas).X - this.MiddleRelativeX), (int) (Mouse.GetPosition(MainCanvas).Y - this.MiddleRelativeY)));
                }
            }
            if (e.LeftButton == MouseButtonState.Pressed && this.MouseInCanvas)
            {
                Wire? wire = this.SelectedWire;
                if (wire != null) 
                {
                    wire.MoveOneNodeLine(new Point((int)(Mouse.GetPosition(MainCanvas).X), (int)(Mouse.GetPosition(MainCanvas).Y)));
                }
            }
        }
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.Key == Key.R)
            {
                if (this.SelectedComponent != null)
                {
                    this.SelectedComponent = (Image)((BitmapImage)this.SelectedComponent)
                }
            }*/
        }

        private void MainCanvas_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!this.MouseInCanvas)
            {
                this.MouseInCanvas = true;
            }
        }

        private void MainCanvas_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.MouseInCanvas)
            {
                this.MouseInCanvas = false;
            }
        }

    }
}
