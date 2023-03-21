using System;
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
    public class Switch : Component
    {
        public bool switchClosed { get; private set; }
        public Switch(string name, Circuit circuit) :base(name, circuit)
        {
            this.switchClosed = true;
            this.Resistance = 0f;
        }

        public override void PerformComponentFunction(float totalVoltage, float totalResistance)
        {
            base.PerformComponentFunction(totalVoltage, totalResistance);
            return;
        }

        public override void DisableComponentFunction()
        {
            base.DisableComponentFunction();
        }

        public void FlipSwitch()
        {
            this.switchClosed = !this.switchClosed;
        }
    }
}
