namespace Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses
{
    public class Cell : Component
    {
        public float Voltage { get; protected set; }
        public Cell(string name, Circuit circuit) : base(name, circuit)
        {
            this.Resistance = 0f;
            this.Voltage = 3f;
        }
        /*The SetVoltage Method is overriden to change the Voltage property to the inputted
          table value, or the one stored in JSON.
        */
        public override void SetVoltage(float voltage)
        {
            this.Voltage = voltage;
        }
    }
}
