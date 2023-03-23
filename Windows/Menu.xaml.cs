using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Real_NEA_Circuit_Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void NewProject(object sender, RoutedEventArgs e)
        {
            Workspace workspace = new Workspace();
            workspace.WindowState = WindowState.Maximized;
            workspace.Show();
            this.Close();
        }
        private void OpenProject(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Title = "Select file to load";
            openFileDialog.DefaultExt = "json";
            openFileDialog.Filter = "Json file (*.json)|*.json";
            string filepath = "";
            if (openFileDialog.ShowDialog() == true)
            {
                filepath = openFileDialog.FileName;
            }
            else { return; }
            Workspace workspace = new Workspace();
            workspace.ImportFile(filepath);
            workspace.WindowState = WindowState.Maximized;
            workspace.Show();
            this.Close();
        }
    }
}
