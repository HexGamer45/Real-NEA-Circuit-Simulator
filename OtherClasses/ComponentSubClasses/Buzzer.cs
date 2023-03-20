﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses
{
    public class Buzzer : Component
    {
        public float wavelength { get; private set; }
        private float activationPower;
        public Buzzer(string name, Circuit circuit) :base(name, circuit)
        {
            this.activationPower = 0.03f;
            this.Resistance = 15f;
        }

        public override void PerformComponentFunction(float totalVoltage, float totalResistance)
        {
            if (this.getPowerAvailable(totalVoltage, totalResistance) >= this.activationPower)
            {
                base.PerformComponentFunction(totalVoltage, totalResistance);
            }
            return;
        }

        public override void DisableComponentFunction()
        {
            base.DisableComponentFunction();
        }
    }
}
