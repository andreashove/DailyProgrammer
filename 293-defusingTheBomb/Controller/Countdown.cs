using System;
using System.Threading;

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

        public void StartThread(ManualResetEvent mre)
        {
            thread.Start(mre);
        }

        public void StartThread()
        {
            interval = 500;
            thread = new Thread(new ThreadStart(PlayBeep));
            thread.IsBackground = true;
            thread.Start();
        }

        public void StopThread()
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
                    
                }
                Console.Beep(freq, interval);
                Thread.Sleep(interval);
                i++;
            }
        }
    }
}
