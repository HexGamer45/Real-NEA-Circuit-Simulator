using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Path = System.IO.Path;

namespace Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses
{
    public class Buzzer : Component
    {
        public SoundPlayer player;
        private float activationPower;
        public Buzzer(string name, Circuit circuit) :base(name, circuit)
        {
            this.player = new SoundPlayer(Path.GetFullPath(@"..\..\..\Assets\BuzzerSoundEffect.wav"));
            this.player.LoadAsync();
            this.activationPower = 0.03f;
            this.Resistance = 15f;
        }

        public override void PerformComponentFunction(float totalVoltage, float totalResistance)
        {
            if (this.getPowerAvailable(totalVoltage, totalResistance) >= this.activationPower)
            {
                
                player.PlayLooping();

                base.PerformComponentFunction(totalVoltage, totalResistance);
            }
            return;
        }

        public override void DisableComponentFunction()
        {
            this.player.Stop();
            base.DisableComponentFunction();
        }
    }
}
