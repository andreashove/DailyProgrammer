using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Timers;

namespace _293_defusingTheBomb
{
    public class Controller : IController
    {
        IView view;
        IModel model;
        Countdown countdownTimer;
        Sounds soundEffects;
        QueryUser threadUserInput;

        private int EXPLOSION = 0;
        private int VICTORY = 1;
        const string EMPTY = "";
        private bool roundFinished = false;
        private bool shouldExplode = false;


        /*
         * Should have main thread pulling the userinput via View and have the timer run on a seperate thread.
         * Implement Restart() which stops thread, check its stopped, and then restarts the whole process
         * Delete a lotta code
         */

        public Controller()
        {
            view = new View();
            model = new Model();
            countdownTimer = new Countdown();
            soundEffects = new Sounds();

            countdownTimer.BombTimerRanOut += HandleTimerRanOut;

            while (true)
            {

                Start();
                

            }
        }

        private void StopCountdown()
        {
            countdownTimer.StopThread();
        }

        private void HandleUserInput(int input)
        {
            var wireToCut = input;

            var wireCutResponse = model.CutWire(wireToCut);
            countdownTimer.Interval -= countdownTimer.Interval / 10;

            if (wireCutResponse == "boom")
            {
                countdownTimer.Interval = 100;
                shouldExplode = true;

            }

        }


        private void HandleReceivedUserData(object sender, ReceivedDataEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RoundFinished()
        {
            ProcessThreadCollection currentThreads = Process.GetCurrentProcess().Threads;
            Console.WriteLine("før Coutn: " + currentThreads.Count);
            Console.ReadLine();
            StopCountdown();

            currentThreads = Process.GetCurrentProcess().Threads;
            Console.WriteLine("etter Coutn: " + currentThreads.Count);
            Console.ReadLine();
        }

        private void HandleTimerRanOut(object sender, EventArgs e)
        {
            roundFinished = true;
            PrintResults();

            if (shouldExplode)
            {
                
                Explode();
            }
            else if (model.GameIsWon())
            {
                Disarm();

            }
            else
            {
                Explode();

            }

            Stop();
        }

        private void PrintResults()
        {
            //view.ClearConsole();
            WriteRulesAndWiresToUser();

        }

        private void Disarm()
        { 
            view.WriteToUser(VICTORY);
            soundEffects.PlayDisarmed();

        }
        private void Explode()
        {
            countdownTimer.Interval = 15;

            view.WriteToUser(EXPLOSION);
            soundEffects.PlayExplosion();
            
            

        }
       
        private void PlayAgain()
        {
            
            view.WriteToUser("Play again?");
            view.GetUserInput();
        }
        public void Start()
        {
            roundFinished = false;
            model.InitializeWires();
            
            countdownTimer.StartThread();
            while (!roundFinished)
            {
                WriteRulesAndWiresToUser();
                var input = view.GetValidatedUserInput();
                HandleUserInput(input);
            }
            

        }
        public void Stop()
        {
            view.WriteToUser("Play again? (input int between 1 and 6 due to high code quality)");
            shouldExplode = false;
            roundFinished = true;
            countdownTimer.StopThread();

        }

        private void WriteRulesAndWiresToUser()
        {
            view.WriteHeaderAndRulesToUser();

            if (roundFinished)
                view.WriteToUser(model.GetWiresFinished());
            else
                view.WriteToUser(model.GetWires());
        }


    }
}
