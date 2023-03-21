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
using System.Windows.Media;

namespace Real_NEA_Circuit_Simulator.OtherClasses
{
    internal static class DataGridHandler
    {
        public static ObservableCollection<ComponentDisplayData> DisplayData = new ObservableCollection<ComponentDisplayData>();
        public static void AddNewComponentData(Component component)
        {
            ComponentDisplayData data = new ComponentDisplayData(component);
            DisplayData.Add(data);

        }

        public static void RemoveComponentData(Component component)
        {
            foreach (ComponentDisplayData data in DisplayData)
            {
                if (data.Component == component)
                {
                    DisplayData.Remove(data);
                    break;
                }
            }
        }

        public static ObservableCollection<ComponentDisplayData> LoadCollectionData()
        {
            return DisplayData;
        }

        public static void Clear()
        {
            DisplayData.Clear();
        }
    }
}
