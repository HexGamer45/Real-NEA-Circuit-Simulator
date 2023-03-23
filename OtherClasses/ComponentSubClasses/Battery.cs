namespace Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses
{
    public class Battery : Cell
    {
        public Battery(string name, Circuit circuit) : base(name, circuit)
        {
            this.Voltage = 6f;
        }

    }
}
