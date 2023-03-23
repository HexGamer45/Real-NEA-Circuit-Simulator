namespace Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses
{
    public class FixedResistor : Component
    {
        //Fixed resistor is identical to the base Component, but it has a default resistance of 40
        public FixedResistor(string name, Circuit circuit) : base(name, circuit)
        {
            this.Resistance = 40f;
        }

    }
}
