using System.Collections.ObjectModel;

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
                if (data.component == component)
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
