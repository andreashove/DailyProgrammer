using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _293_defusingTheBomb
{
    public class Countdown
    {
        Thread thread;
        int freq = 4000;
        int interval = 500;

        public int Freq
        {
            get; set;
        }

        public int Interval
        {
            get
            {
                return interval;
            }
            set
            {
                interval = value;
            }
        }

        public event EventHandler BombTimerRanOut;

        private void Explosion(object o, EventArgs a)
        {
            if (BombTimerRanOut != null)
                BombTimerRanOut(this, null);
        }

        internal void InitializeBoom()
        {
            Explosion(this, null);
        }

        
        

        public Countdown()
        {
            thread = new Thread(new ThreadStart(PlayBeep));
            thread.IsBackground = true;
            
        }

        public void StartThread()
        {
            thread.Start();
        }

        

        private void PlayExplosion()
        {
            using (SoundPlayer player = new SoundPlayer())
            {
                player.SoundLocation = "explosion.wav";
                player.Play();
            }
               
        }

        private void PlayDisarmed()
        {
            using (SoundPlayer player = new SoundPlayer())
            {
                player.SoundLocation = "disarmed.wav";
                player.Play();
            }
        } 

        public void PlayDisarmAndStopThread()
        {
            PlayDisarmed();
            StopThread();
            
        }

        public void PlayExplosionAndStopThread()
        {
            PlayExplosion();
            StopThread();
        }
        private void StopThread()
        {
            thread.Abort();
        }

        public void ResetSound()
        {
            freq = 2000;
            interval = 500;
        }

        private void PlayBeep()
        {
            int i = 0;
            int subtractor = 15;
            while (true)
            {
                
                interval = interval - subtractor;
                if (interval == 200)
                    subtractor = 10;
                if (interval == 100)
                {
                    subtractor = 5;
                }
                if (interval == 35)
                {
                    subtractor = 1;
                }
                if (interval <= 0)
                {
                    InitializeBoom();
                    return;
                    //Explosion(this, null);
                    //Console.WriteLine("Now it should be boom boom time");
                    /*view.WriteToUser(EXPLOSION);
                    model.InitializeWires();
                    view.WriteToUser("Play again?");

                    view.GetUserInput();
                    interval = 500;*/
                }
                Console.Beep(freq, interval);
                Thread.Sleep(interval);
                i++;
            }
        }
    }
}
