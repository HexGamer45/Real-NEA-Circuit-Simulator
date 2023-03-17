using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses
{
    public class LED : Component
    {
        public float wavelength { get; private set; }
        private float activationVoltage;
        public LED(string name, Circuit circuit) :base(name, circuit)
        {
            this.activationVoltage = 0f;
            this.Resistance = 1f;
        }

        public override void PerformComponentFunction(float totalVoltage, float totalResistance)
        {
            base.PerformComponentFunction(totalVoltage, totalResistance);
            Console.WriteLine(this.activationVoltage);
            Console.WriteLine(this.getVoltageAvailable(totalVoltage, totalResistance));
            if (this.getVoltageAvailable(totalVoltage, totalResistance) >= this.activationVoltage)
            {
                this.image.Source = (BitmapImage)Application.Current.FindResource("LitLED");
            }
            return;
        }

        public override void DisableComponentFunction()
        {
            base.DisableComponentFunction();
            this.image.Source = (BitmapImage) Application.Current.FindResource("LED");
        }
    }
}
