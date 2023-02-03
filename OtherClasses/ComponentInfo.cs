﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_NEA_Circuit_Simulator.OtherClasses
{
    internal class ComponentInfo
    {
        public float Resistance;
        public float WorkingVoltage;
        public bool Active = false;
        public Component ComponentDescribing;

        public ComponentInfo(float resistance, float workingVoltage, Component componentDescribing)
        {
            Resistance = resistance;
            WorkingVoltage = workingVoltage;
            ComponentDescribing = componentDescribing;
        }
    }
}
