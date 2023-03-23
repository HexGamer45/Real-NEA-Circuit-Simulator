using Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses;

namespace Real_NEA_Circuit_Simulator.OtherClasses
{
    public class ComponentDisplayData
    {
        public Component component { get; private set; }
        public string name;
        public float resistance;
        public float voltage;
        public bool active;

        /*
        The following properies have defined functions like this because the DataGridHandler is associated with a .NET
        Class (Used to update the data table in the workspace) that can only access fields in the following format:
        */
        public string Name 
        {
            get
            {
                return name;
            } set
            {
                name = value;
                component.SetName(name);
            }
        }
        public float Resistance
        {
            get
            {
                return resistance;
            }
            set
            {
                resistance = value;
                component.SetResistance(resistance);
            }
        }
        public float Voltage
        {
            get
            {
                return voltage;
            }
            set
            {
                voltage = value;
                component.SetVoltage(voltage);
            }
        }
        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
                component.SetActive(active);
            }
        }
        public ComponentDisplayData(Component component)
        {
            this.component = component;
            name = component.name;
            resistance = component.Resistance;
            if (component is Cell)
            {
                voltage = ((Cell)component).Voltage;
            }
            else
            {
                voltage = 0;
            }
            active = component.Active;
        }
    }
}
