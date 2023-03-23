using System.Media;
using Path = System.IO.Path;

namespace Real_NEA_Circuit_Simulator.OtherClasses.ComponentSubClasses
{
    public class Buzzer : Component
    {
        /*Activation power is required as it's a buzzer, so it only activates when the total
          available power to the Buzzer is higher than this number
         */
        private SoundPlayer player;
        private float activationPower;
        public Buzzer(string name, Circuit circuit) :base(name, circuit)
        {
            //This stores and loads a sound file to be played on loop on activation.
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
