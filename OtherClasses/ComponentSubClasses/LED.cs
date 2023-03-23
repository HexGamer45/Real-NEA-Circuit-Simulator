using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses
{
    public class LED : Component
    {
        /*Activation voltage is required as it's a diode, so it only activates when the total
          available voltage to the LED is higher than this number
         */
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
