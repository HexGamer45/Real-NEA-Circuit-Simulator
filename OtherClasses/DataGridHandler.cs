using System.Collections.ObjectModel;

namespace Real_NEA_Circuit_Simulator.OtherClasses
{
    internal static class DataGridHandler
    {
        //The observable collection makes it so that other references to this get updated on changes
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
        /*LoadCollectionData can be called anywhere in the program to return a reference to
          DisplayData
         */
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
