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
    public class LED : Component
    {
        private float activationVoltage;
        public LED(string name, Circuit circuit) :base(name, circuit)
        {
            this.activationVoltage = 1.5f;
            this.Resistance = 20f;
        }

        public override void PerformComponentFunction(float totalVoltage, float totalResistance)
        {
            if (this.getVoltageAvailable(totalVoltage, totalResistance) >= this.activationVoltage)
            {
                if (this.image.Source is TransformedBitmap)
                {
                    RotateTransform rotation = new RotateTransform(this.rotation);
                    TransformedBitmap transformedBmp = new TransformedBitmap();
                    transformedBmp.BeginInit();
                    transformedBmp.Source = (BitmapImage)Application.Current.FindResource("LitLED");
                    transformedBmp.Transform= rotation;
                    transformedBmp.EndInit();
                    this.image.Source = transformedBmp;
                    base.PerformComponentFunction(totalVoltage, totalResistance);
                    return;
                }
                this.image.Source = (BitmapImage)Application.Current.FindResource("LitLED");
                base.PerformComponentFunction(totalVoltage, totalResistance);
            }
            return;
        }

        public override void DisableComponentFunction()
        {
            base.DisableComponentFunction();
            if (this.image.Source is TransformedBitmap)
            {
                RotateTransform rotation = new RotateTransform(this.rotation);
                TransformedBitmap transformedBmp = new TransformedBitmap();
                transformedBmp.BeginInit();
                transformedBmp.Source = (BitmapImage)Application.Current.FindResource("LED");
                transformedBmp.Transform = rotation;
                transformedBmp.EndInit();
                this.image.Source = transformedBmp;
                return;
            }
            this.image.Source = (BitmapImage) Application.Current.FindResource("LED");

        }
    }
}
