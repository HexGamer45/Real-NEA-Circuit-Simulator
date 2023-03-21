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
    public class Motor : Component
    {
        private float activationPower;
        public Motor(string name, Circuit circuit) :base(name, circuit)
        {
            this.activationPower = 0.02f;
            this.Resistance = 0.5f;
        }

        public override void PerformComponentFunction(float totalVoltage, float totalResistance)
        {
            if (this.getPowerAvailable(totalVoltage, totalResistance) >= this.activationPower)
            {
                if (this.image.Source is TransformedBitmap)
                {
                    RotateTransform rotation = new RotateTransform(this.rotation);
                    TransformedBitmap transformedBmp = new TransformedBitmap();
                    transformedBmp.BeginInit();
                    transformedBmp.Source = (BitmapImage)Application.Current.FindResource("ActiveMotor");
                    transformedBmp.Transform= rotation;
                    transformedBmp.EndInit();
                    this.image.Source = transformedBmp;
                    base.PerformComponentFunction(totalVoltage, totalResistance);
                    return;
                }
                this.image.Source = (BitmapImage)Application.Current.FindResource("ActivateMotor");
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
                transformedBmp.Source = (BitmapImage)Application.Current.FindResource("Motor");
                transformedBmp.Transform = rotation;
                transformedBmp.EndInit();
                this.image.Source = transformedBmp;
                return;
            }
            this.image.Source = (BitmapImage) Application.Current.FindResource("Motor");

        }
    }
}
