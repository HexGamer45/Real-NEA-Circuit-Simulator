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
using System.Linq;
using Microsoft.Win32;
using System.Text.Json.Serialization;
using System.Text.Json;
using Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses;
using Newtonsoft.Json;
using System.Windows.Navigation;
using Real_NEA_Circuit_Simulator.OtherClasses;
using System.Collections.ObjectModel;
using Color = System.Drawing.Color;

namespace Real_NEA_Circuit_Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Workspace : Window
    {
        public Circuit MainCircuit {get; private set;}
        private Image? LeftSelectedComponent;
        private Image? SelectedComponent;
        private Image? SelectedNode;
        private Node? SelectedNodeObject;
        private Wire? SelectedWire;
        private bool MouseInCanvas;
        private bool MiddleDown;
        private bool LeftDown;
        private float MiddleRelativeX;
        private float MiddleRelativeY;
        private bool Simulating;
        private string? loadedFile;

        public Workspace()
        {
            InitializeComponent();
            this.MainCircuit = new Circuit("Main", MainCanvas);
            this.SelectedWire = null;
            this.LeftSelectedComponent= null;
            this.SelectedComponent = null;
            this.SelectedNode = null;
            this.SelectedNodeObject= null;
            this.MouseInCanvas = false;
            this.LeftDown = false;
            this.MiddleDown = false;
            this.MiddleRelativeX = 0;
            this.MiddleRelativeY = 0;
            this.Simulating = false;
            this.loadedFile = null;
            ComponentDataGrid.ItemsSource = DataGridHandler.LoadCollectionData();
        }

        private void GenerateNewCell(object sender, RoutedEventArgs eventArgs)
        {
            if (this.Simulating) { return; }
            int count = 0;
            foreach (Component component in this.MainCircuit.ComponentsList)
            {
                if (component is Cell)
                {
                    count++;
                }
            }
            Cell newComponent = new Cell("Cell" + count.ToString(), this.MainCircuit);
            this.MainCircuit.ComponentsList.Add(newComponent);
            Point position = new Point((int)MainCanvas.ActualWidth / 2, (int)MainCanvas.ActualHeight / 2);
            newComponent.RenderFirst(position);
            DataGridHandler.AddNewComponentData(newComponent);
            ComponentDataGrid.Items.Refresh();
        }
        private void GenerateNewBattery(object sender, RoutedEventArgs eventArgs)
        {
            if (this.Simulating) { return; }
            int count = 0;
            foreach (Component component in this.MainCircuit.ComponentsList)
            {
                if (component is Battery)
                {
                    count++;
                }
            }
            Battery newComponent = new Battery("Battery" + count.ToString(), this.MainCircuit);
            this.MainCircuit.ComponentsList.Add(newComponent);
            Point position = new Point((int)MainCanvas.ActualWidth / 2, (int)MainCanvas.ActualHeight / 2);
            newComponent.RenderFirst(position);
            DataGridHandler.AddNewComponentData(newComponent);
            ComponentDataGrid.Items.Refresh();
        }
        private void GenerateNewBulb(object sender, RoutedEventArgs eventArgs)
        {
            if (this.Simulating) { return; }
            int count = 0;
            foreach (Component component in this.MainCircuit.ComponentsList)
            {
                if (component is Bulb)
                {
                    count++;
                }
            }
            Bulb newComponent = new Bulb("Bulb" + count.ToString(), this.MainCircuit);
            this.MainCircuit.ComponentsList.Add(newComponent);
            Point position = new Point((int)MainCanvas.ActualWidth / 2, (int)MainCanvas.ActualHeight / 2);
            newComponent.RenderFirst(position);
            DataGridHandler.AddNewComponentData(newComponent);
            ComponentDataGrid.Items.Refresh();
        }
        private void GenerateNewBuzzer(object sender, RoutedEventArgs eventArgs)
        {
            if (this.Simulating) { return; }
            int count = 0;
            foreach (Component component in this.MainCircuit.ComponentsList)
            {
                if (component is Buzzer)
                {
                    count++;
                }
            }
            Buzzer newComponent = new Buzzer("Buzzer" + count.ToString(), this.MainCircuit);
            this.MainCircuit.ComponentsList.Add(newComponent);
            Point position = new Point((int)MainCanvas.ActualWidth / 2, (int)MainCanvas.ActualHeight / 2);
            newComponent.RenderFirst(position);
            DataGridHandler.AddNewComponentData(newComponent);
            ComponentDataGrid.Items.Refresh();
        }
        private void GenerateNewMotor(object sender, RoutedEventArgs eventArgs)
        {
            if (this.Simulating) { return; }
            int count = 0;
            foreach (Component component in this.MainCircuit.ComponentsList)
            {
                if (component is Motor)
                {
                    count++;
                }
            }
            Motor newComponent = new Motor("Motor" + count.ToString(), this.MainCircuit);
            this.MainCircuit.ComponentsList.Add(newComponent);
            Point position = new Point((int)MainCanvas.ActualWidth / 2, (int)MainCanvas.ActualHeight / 2);
            newComponent.RenderFirst(position);
            DataGridHandler.AddNewComponentData(newComponent);
            ComponentDataGrid.Items.Refresh();
        }
        private void GenerateNewLED(object sender, RoutedEventArgs eventArgs)
        {
            if (this.Simulating) { return; }
            int count = 0;
            foreach (Component component in this.MainCircuit.ComponentsList)
            {
                if (component is LED)
                {
                    count++;
                }
            }
            LED newComponent = new LED("LED" + count.ToString(), this.MainCircuit);
            this.MainCircuit.ComponentsList.Add(newComponent);
            Point position = new Point((int)MainCanvas.ActualWidth / 2, (int)MainCanvas.ActualHeight / 2);
            newComponent.RenderFirst(position);
            DataGridHandler.AddNewComponentData(newComponent);
            ObservableCollection<ComponentDisplayData> data = DataGridHandler.LoadCollectionData();
            ComponentDataGrid.Items.Refresh();
        }
        private void GenerateNewFixedResistor(object sender, RoutedEventArgs eventArgs)
        {
            if (this.Simulating) { return; }
            int count = 0;
            foreach (Component component in this.MainCircuit.ComponentsList)
            {
                if (component is FixedResistor)
                {
                    count++;
                }
            }
            FixedResistor newComponent = new FixedResistor("FixedResistor" + count.ToString(), this.MainCircuit);
            this.MainCircuit.ComponentsList.Add(newComponent);
            Point position = new Point((int)MainCanvas.ActualWidth / 2, (int)MainCanvas.ActualHeight / 2);
            newComponent.RenderFirst(position);
            DataGridHandler.AddNewComponentData(newComponent);
            ComponentDataGrid.Items.Refresh();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {

                Image? closestImage = this.GetClosestComponent();
                if (closestImage != null)
                {
                    Component closestComponent = (Component)closestImage.Tag;
                    ((TextBlock)MainGrid.FindName("CurrentlySelectedName")).Text = closestComponent.name;
                    ((Grid)MainGrid.FindName("CurrentlySelectedGrid")).Background = (Brush) Application.Current.FindResource("ComponentSelected");
                }
                else
                {
                    ((TextBlock)MainGrid.FindName("CurrentlySelectedName")).Text = "N/A";
                    ((Grid)MainGrid.FindName("CurrentlySelectedGrid")).Background = (Brush)Application.Current.FindResource("ComponentUnselected");
                }
                this.LeftSelectedComponent = closestImage;
            }
            if (e.RightButton == MouseButtonState.Pressed)
            {
                Image? closestComponentImage = this.GetClosestComponent();
                if (closestComponentImage != null)
                {
                    Component closestComponent = (Component)closestComponentImage.Tag;
                    if (closestComponent is Switch)
                    {
                        Switch switchComponent = (Switch) closestComponent;
                        switchComponent.FlipSwitch();
                        if (this.Simulating)
                        {
                            this.SimulateCircuit(sender,e);
                            this.SimulateCircuit(sender,e);
                        }
                    }
                }
            }
            if (this.Simulating) 
            { 
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.SelectedComponent = this.GetClosestComponent();
                    Image? closestImage = this.SelectedComponent;
                    if (closestImage != null)
                    {
                        Component SelectedComponentObject = (Component) closestImage.Tag;
                        /*use component data to make table under right buttons*/
                    }
                }
                return; 
            }
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
                if (this.SelectedNode!= null ) { this.SelectedNodeObject = (Node)this.SelectedNode.Tag; }
                
                Image? closestImage = this.SelectedNode;
                if (closestImage != null)
                {
                    Wire newWire = new Wire(((Node)closestImage.Tag).name + "-temp", new List<Node>() {(Node)closestImage.Tag}, this.MainCircuit);
                    newWire.RenderWithOneNode();
                    this.SelectedWire = newWire;
                }
            }
        }
        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.Simulating) { return; }
            if (this.MiddleDown && e.MiddleButton == MouseButtonState.Released)
            {
                this.MiddleDown = false;
                this.SelectedComponent = null;
            }
            if (this.LeftDown && e.LeftButton == MouseButtonState.Released)
            {
                this.LeftDown = false;
                if (this.SelectedWire != null)
                {
                    Image? closestNodeImage = GetClosestNode();
                    if (closestNodeImage != null && closestNodeImage != this.SelectedNode)
                    {
                        Node closestNode = (Node) closestNodeImage.Tag;
                        foreach (Wire wire in closestNode.ConnectedWires)
                        {
                            if (wire.ConnectedNodes.Contains(this.SelectedNodeObject))
                            {
                                wire.DeleteThisConnection();
                                this.SelectedWire.RemoveLine();
                                this.SelectedWire = null;
                                return;
                            }

                        }
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
            if (this.Simulating) { return; }
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
            if (Simulating) { return; }
            if (e.Key == Key.R && this.MouseInCanvas)
            {
                Image? componentToRotate = null;
                if (SelectedComponent != null)
                {
                    componentToRotate = SelectedComponent;
                }
                else if (LeftSelectedComponent!= null)
                {
                    componentToRotate = LeftSelectedComponent;
                }
                if (componentToRotate != null)
                {
                    Component componentObject = (Component)componentToRotate.Tag;
                    componentObject.Rotate();
                }
            }
            if (e.Key == Key.Delete && this.LeftSelectedComponent != null && this.Simulating == false)
            {
                Component componentToDelete = (Component)this.LeftSelectedComponent.Tag;
                componentToDelete.Delete();
                DataGridHandler.RemoveComponentData(componentToDelete);
                this.LeftSelectedComponent=null;
                ((TextBlock)MainGrid.FindName("CurrentlySelectedName")).Text = "N/A";
                ((Grid)MainGrid.FindName("CurrentlySelectedGrid")).Background = (Brush)Application.Current.FindResource("ComponentUnselected");
            }
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
        private void SimulateCircuit(object sender, RoutedEventArgs e)
        {
            if (this.Simulating == false)
            {
                Dictionary<Component, List<Component>> UsableCircuit = this.MainCircuit.RemoveNonCircuitComponents();
                if (UsableCircuit.Keys.Count < 2)
                {
                    return;
                }
                float totalVoltage = 0;
                float totalResistance = 0;
                foreach (Component component in UsableCircuit.Keys)
                {
                    totalResistance += component.Resistance;
                    if (component is Cell)
                    {
                        totalVoltage += ((Cell)component).Voltage;
                    }
                }
                bool componentFailed = false;
                foreach (Component component in UsableCircuit.Keys)
                {
                    component.PerformComponentFunction(totalVoltage, totalResistance);
                    if (component.Active == false)
                    {
                        componentFailed = true;
                        break;
                    }
                }
                if (componentFailed)
                {
                    this.DisableCircuit();
                    string text = "One or more component(s) failed, this could be due to a lack of voltage from cells or a switch being open.";
                    string caption = "Circuit Broken";
                    MessageBoxButton messageButton = MessageBoxButton.OK;
                    MessageBoxImage icon = MessageBoxImage.Warning;
                    MessageBox.Show(text, caption, messageButton, icon);
                }
                else
                {
                    Button button = (Button)sender;
                    button.Content = "Return to Editting";
                    this.Simulating = true;
                    ComponentDataGrid.Columns[0].IsReadOnly = true;
                    ComponentDataGrid.Columns[1].IsReadOnly = true;
                    ComponentDataGrid.Columns[2].IsReadOnly = true;
                }

            }
            else
            {
                Button button = (Button)sender;
                button.Content = "Simulate";
                this.Simulating = false;
                this.DisableCircuit();
                ComponentDataGrid.Columns[0].IsReadOnly = false;
                ComponentDataGrid.Columns[1].IsReadOnly = false;
                ComponentDataGrid.Columns[2].IsReadOnly = false;
            }
            foreach (ComponentDisplayData data in ComponentDataGrid.Items)
            {
                data.Active = data.ComponentDescribing.Active;
            }
            ComponentDataGrid.Items.Refresh();
        }
        private void DisableCircuit()
        {
            foreach (Component component in this.MainCircuit.ComponentsList)
            {
                component.DisableComponentFunction();
            }
        }
        private void ClearSelf()
        {
            this.MainCanvas.Children.Clear();
            this.MainCircuit.ComponentsList.Clear();
            this.MainCircuit.WireToNodes.Clear();
            this.MainCircuit.AdjacencyList.Clear();
            DataGridHandler.Clear();
        }
        private void Load(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Title = "Select file to load";
            openFileDialog.DefaultExt = "json";
            openFileDialog.Filter = "Json file (*.json)|*.json";
            if (openFileDialog.ShowDialog() == true)
            {
                string filepath = openFileDialog.FileName;
                this.ClearSelf();
                this.ImportFile(filepath);
            }
        }
        private void SaveAs(object sender, RoutedEventArgs e)
        {
            bool? hasClicked = false;
            string fileName = "";
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.DefaultExt = "json";
            fileDialog.Filter = "Json file (*.json)|*.json";
            fileDialog.FileName = "";
            hasClicked = fileDialog.ShowDialog();
            fileName = fileDialog.FileName;
            if (fileDialog.FileName != "" && hasClicked == true)
            {
                this.loadedFile = fileName;
            }
            this.Save(sender,e);
        }
        private void Save(object sender, RoutedEventArgs e)
        {
            Dictionary<string, Dictionary<string, string>> ComponentsToSave = new();
            Dictionary<string, List<List<int>>> AdjacencyListToSave = new();
            int Counter = 0;
            foreach (Component currentComponent in this.MainCircuit.ComponentsList)
            {
                if (ComponentsToSave.ContainsKey(currentComponent.name))
                {
                    MessageBox.Show("Unable to save file, because one or more components have the same name.", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                ComponentsToSave.Add(currentComponent.name, new Dictionary<string, string>());
                string[] typePath = currentComponent.GetType().ToString().Split('.');
                ComponentsToSave[currentComponent.name].Add("Type", currentComponent.GetType().Name);
                ComponentsToSave[currentComponent.name].Add("Resistance", currentComponent.Resistance.ToString());
                ComponentsToSave[currentComponent.name].Add("Rotation", currentComponent.rotation.ToString());
                ComponentsToSave[currentComponent.name].Add("PositionX", ((int)(Canvas.GetLeft(currentComponent.image) + currentComponent.image.ActualWidth / 2)).ToString());
                ComponentsToSave[currentComponent.name].Add("PositionY", ((int)(Canvas.GetTop(currentComponent.image) + currentComponent.image.ActualHeight / 2)).ToString());

                if (currentComponent is Cell)
                {
                    ComponentsToSave[currentComponent.name].Add("Voltage", ((Cell)currentComponent).Voltage.ToString());
                }
                AdjacencyListToSave.Add(Counter.ToString(), new List<List<int>>());
                foreach (Component neighbour in this.MainCircuit.AdjacencyList[currentComponent])
                {
                    int neighbourIndex = this.MainCircuit.ComponentsList.IndexOf(neighbour);
                    int inpOut = 0;
                    foreach (Node node in neighbour.ConnectedNodes)
                    {
                        foreach (Wire wire in node.ConnectedWires)
                        {
                            bool finished = false;
                            if (finished)
                            {
                                break;
                            }
                            foreach (Node node2 in wire.ConnectedNodes)
                            {
                                if (node2 != node)
                                {
                                    if (node2.ConnectedComponent == currentComponent)
                                    {
                                        inpOut = node.ConnectedComponent.ConnectedNodes.IndexOf(node);
                                        AdjacencyListToSave[Counter.ToString()].Add(new List<int>() { neighbourIndex, inpOut });
                                        finished = true;
                                    }
                                }
                            }
                        }
                    }
                }
                Counter += 1;
            }
            bool? hasClicked = false;
            string jsonString = ConvertDictsToJsonString(ComponentsToSave, AdjacencyListToSave);
            string fileName = "";
            SaveFileDialog fileDialog = new SaveFileDialog();
            if (this.loadedFile != null)
            {
                fileName = this.loadedFile;
            }
            else
            {
                fileDialog.DefaultExt = "json";
                fileDialog.Filter = "Json file (*.json)|*.json";
                fileDialog.FileName = "";
                hasClicked = fileDialog.ShowDialog();
                fileName = fileDialog.FileName;
            }
            if (fileName != "" && (this.loadedFile!=null || hasClicked == true))
            {
                File.WriteAllText(fileName, jsonString);
                this.loadedFile = fileName;
            }
        }
        public string ConvertDictsToJsonString(Dictionary<string, Dictionary<string, string>> Components, Dictionary<string, List<List<int>>> AdjacencyListToSave)
        {
            string jsonString = "{\"Components\":{";
            foreach (string componentNameKey in Components.Keys)
            {
                Dictionary<string, string> component = Components[componentNameKey];
                jsonString += "\n\"" + componentNameKey + "\":{";
                foreach (string key in component.Keys)
                {
                    jsonString += "\"" + key + "\":\"" + component[key] + "\",";
                }
                jsonString = jsonString.Substring(0, jsonString.Length - 1);
                jsonString += "},";
            }
            jsonString = jsonString.Substring(0, jsonString.Length - 2);
            jsonString += "}},";

            jsonString += "\n\n\"AdjacencyList\":{\n";
            if (AdjacencyListToSave.Keys.Count >= 2)
            {
                foreach (string index in AdjacencyListToSave.Keys)
                {
                    jsonString += "\"" + index + "\":[";
                    int counter = 0;
                    foreach (List<int> neighbourAndNode in AdjacencyListToSave[index])
                    {
                        counter += 1;
                        jsonString += "[" + neighbourAndNode[0] + "," + neighbourAndNode[1] + "],";
                    }
                    if (counter > 0)
                    {
                        jsonString = jsonString.Substring(0, jsonString.Length - 2);
                        jsonString += "]],";
                    }
                    else
                    {
                        jsonString += "],";
                    }
                }
                jsonString = jsonString.Substring(0, jsonString.Length - 2);
                jsonString += "]";
            }
            jsonString += "}}";

            return jsonString;
        }
        public void ImportFile(string filename)
        {
            if (filename.EndsWith(".json"))
            {
                this.loadedFile = filename;
                Dictionary<string, Dictionary<string, object>>? incoming = new Dictionary<string, Dictionary<string, object>>();
                using (StreamReader r = new StreamReader(filename))
                {
                    string data = r.ReadToEnd();
                    incoming = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, object>>>(data);
                    if (incoming != null)
                    {
                        foreach (KeyValuePair<string,object> componentNameDataPair in incoming["Components"])
                        {
                            JsonElement rawComponentData =(JsonElement) componentNameDataPair.Value;
                            Dictionary<string,string>? componentData = rawComponentData.Deserialize<Dictionary<string, string>>();
                            if (componentData != null)
                            {
                                if (componentData["Type"] == "LED")
                                {
                                    LED newComponent = new LED(componentNameDataPair.Key, MainCircuit);
                                    Point position = new Point(Convert.ToInt16(componentData["PositionX"]), Convert.ToInt16(componentData["PositionY"]));
                                    newComponent.SetResistance(float.Parse(componentData["Resistance"]));
                                    newComponent.RenderFirst(position);
                                    this.MainCircuit.ComponentsList.Add(newComponent);
                                    newComponent.Rotate(int.Parse(componentData["Rotation"]));
                                    DataGridHandler.AddNewComponentData(newComponent);
                                }
                                else if(componentData["Type"] == "FixedResistor")
                                {
                                    FixedResistor newComponent = new FixedResistor(componentNameDataPair.Key, MainCircuit);
                                    Point position = new Point(Convert.ToInt16(componentData["PositionX"]), Convert.ToInt16(componentData["PositionY"]));
                                    newComponent.SetResistance(float.Parse(componentData["Resistance"]));
                                    newComponent.RenderFirst(position);
                                    this.MainCircuit.ComponentsList.Add(newComponent);
                                    //newComponent.Rotate(int.Parse(componentData["Rotation"]));
                                    DataGridHandler.AddNewComponentData(newComponent);
                                }
                                else if (componentData["Type"] == "Cell")
                                {
                                    Cell newComponent = new Cell(componentNameDataPair.Key, MainCircuit);
                                    Point position = new Point(Convert.ToInt16(componentData["PositionX"]), Convert.ToInt16(componentData["PositionY"]));
                                    newComponent.SetResistance(float.Parse(componentData["Resistance"]));
                                    newComponent.SetVoltage(float.Parse(componentData["Voltage"]));
                                    newComponent.RenderFirst(position);
                                    this.MainCircuit.ComponentsList.Add(newComponent);
                                    newComponent.Rotate(int.Parse(componentData["Rotation"]));
                                    DataGridHandler.AddNewComponentData(newComponent);
                                }

                            }

                        }
                        foreach (KeyValuePair<string, object> componentNameDataPair in incoming["AdjacencyList"])
                        {
                            JsonElement rawConnections = (JsonElement)componentNameDataPair.Value;
                            List<List<int>>? neighbours = rawConnections.Deserialize<List<List<int>>>();
                            if (neighbours != null && neighbours.Count > 0)
                            {
                                Component currentComponent = this.MainCircuit.ComponentsList[Convert.ToInt16(componentNameDataPair.Key)];
                                if (!this.MainCircuit.AdjacencyList.ContainsKey(currentComponent))
                                {
                                    this.MainCircuit.AdjacencyList.Add(currentComponent, new List<Component>());
                                }
                                foreach (List<int> neighbourDat in neighbours)
                                {
                                    int inpOut = 0;
                                    if (neighbourDat[1] == 0)
                                    {
                                        inpOut = 1;
                                    }

                                    int neighbourIndex = neighbourDat[0];
                                    Component neighbour = this.MainCircuit.ComponentsList[Convert.ToInt16(neighbourIndex)];
                                    if (!this.MainCircuit.AdjacencyList.ContainsKey(neighbour))
                                    {
                                        this.MainCircuit.AdjacencyList.Add(neighbour, new List<Component>());
                                    }
                                    bool connAlreadyMade = false;
                                    foreach (Wire connWire in this.MainCircuit.WireToNodes.Keys)
                                    {
                                        if (this.MainCircuit.WireToNodes[connWire].Contains(currentComponent.ConnectedNodes[inpOut]) && this.MainCircuit.WireToNodes[connWire].Contains(neighbour.ConnectedNodes[neighbourDat[1]]))
                                        {
                                            connAlreadyMade = true;
                                        }
                                    }
                                    if (!connAlreadyMade)
                                    {
                                        Wire newWire = new Wire(currentComponent.name + inpOut + "-" + neighbour.name + neighbourDat[1], new List<Node>() { currentComponent.ConnectedNodes[inpOut] }, MainCircuit);
                                        newWire.RenderWithOneNode();
                                        newWire.ConnectSecondNode(neighbour.ConnectedNodes[neighbourDat[1]]);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
