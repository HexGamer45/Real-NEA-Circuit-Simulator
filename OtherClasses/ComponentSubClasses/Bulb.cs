using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses
{
    public class Bulb : Component
    {
        public Bulb(string name, Circuit circuit) :base(name, circuit)
        {
            this.Resistance = 20f;
        }

        public override void PerformComponentFunction(float totalVoltage, float totalResistance)
        {
            if (this.getPowerAvailable(totalVoltage,totalResistance) <= 0)
            {
                return;
            }
            if (this.image.Source is TransformedBitmap)
            {
                RotateTransform rotation = new RotateTransform(this.rotation);
                TransformedBitmap transformedBmp = new TransformedBitmap();
                transformedBmp.BeginInit();
                transformedBmp.Source = (BitmapImage)Application.Current.FindResource("LitBulb");
                transformedBmp.Transform= rotation;
                transformedBmp.EndInit();
                this.image.Source = transformedBmp;
                base.PerformComponentFunction(totalVoltage, totalResistance);
                return;
            }
            this.image.Source = (BitmapImage)Application.Current.FindResource("LitBulb");
            base.PerformComponentFunction(totalVoltage, totalResistance);
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
                transformedBmp.Source = (BitmapImage)Application.Current.FindResource("Bulb");
                transformedBmp.Transform = rotation;
                transformedBmp.EndInit();
                this.image.Source = transformedBmp;
                return;
            }
            this.image.Source = (BitmapImage) Application.Current.FindResource("Bulb");

        }
    }
}
