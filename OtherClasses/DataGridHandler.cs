using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Threading.Tasks;
using System.Windows.Controls;
using Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace Real_NEA_Circuit_Simulator.OtherClasses
{
    internal static class DataGridHandler
    {
        public static ObservableCollection<ComponentDisplayData> DisplayData = new ObservableCollection<ComponentDisplayData>();
        public static void AddNewComponentData(Component component)
        {
            ComponentDisplayData data = new ComponentDisplayData(component);
            Console.WriteLine(data.Name);

            DisplayData.Add(data);

        }

        public static ObservableCollection<ComponentDisplayData> LoadCollectionData()
        {
            Console.WriteLine("Loaded");
            return DisplayData;
        }
    }
}
