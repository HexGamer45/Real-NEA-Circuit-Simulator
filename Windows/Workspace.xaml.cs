using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Real_NEA_Circuit_Simulator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Workspace : Window
    {
        Circuit MainCircuit = new Circuit();

        public Workspace()
        {
            InitializeComponent();
        }

        private void GenerateNewComponent(object sender, RoutedEventArgs e)
        {
            Button button = (Button) sender;
            string tag = (string)button.Tag;
            Console.WriteLine(tag);
            Component newComponent = Component.ComponentNames[tag];

        }
    }
}
