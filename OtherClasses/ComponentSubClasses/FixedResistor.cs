﻿namespace Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses
{
    public class FixedResistor : Component
    {
        public FixedResistor(string name, Circuit circuit) : base(name, circuit)
        {
            this.Resistance = 40f;
        }

    }
}
