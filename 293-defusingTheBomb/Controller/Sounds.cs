using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace _293_defusingTheBomb
{
    public class Sounds
    {
        public void PlayExplosion()
        {
            using (SoundPlayer player = new SoundPlayer())
            {
                player.SoundLocation = "explosion.wav";
                player.Play();
            }

        }

        public void PlayDisarmed()
        {
            using (SoundPlayer player = new SoundPlayer())
            {
                player.SoundLocation = "disarmed.wav";
                player.Play();
            }
        }
    }
}
