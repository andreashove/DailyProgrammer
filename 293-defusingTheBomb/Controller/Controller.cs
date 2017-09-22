using System;
using System.ComponentModel;
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
        private bool shouldStart = true;

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
            //threadUserInput = new QueryUser();

            soundEffects = new Sounds();

            countdownTimer.BombTimerRanOut += HandleTimerRanOut;
            //threadUserInput.SendData += HandleReceivedUserData;

            var waitHandles = new ManualResetEvent[2];

            while (true)
            {

                WriteRulesAndWiresToUser();

                if (shouldStart)
                {
                    shouldStart = false;
                    Start();
                }

                var input = view.GetValidatedUserInput();
                HandleUserInput(input);

            }
            

   
            


            /*
            for (int i = 0; i < 2; i++)
            {
                waitHandles[i] = new ManualResetEvent(false);

                new Thread(waitHandle =>
                {

                    // TODO: Do some processing...

                    // signal the corresponding wait handle
                    // ideally wrap the processing in a try/finally
                    // to ensure that the handle has been signaled
                    (waitHandle as ManualResetEvent).Set();
                }).Start(waitHandles[i]);
            }

            // wait for all handles to be signaled => this will block the main
            // thread until all the handles have been signaled (calling .Set on them)
            // which would indicate that the background threads have finished
            // We also define a 30s timeout to avoid blocking forever
            if (!WaitHandle.WaitAll(waitHandles, TimeSpan.FromSeconds(30)))
            {
                // timeout
            }

            if (!WaitHandle.WaitAll(waitHandles, TimeSpan.FromSeconds(30)))
            {
                Console.WriteLine("All handles closed");
            }
            */



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

            }

        }


        private void HandleReceivedUserData(object sender, ReceivedDataEventArgs e)
        {
            var wireToCut = e.UserInput;

            var wireCutResponse = model.CutWire(wireToCut);
            countdownTimer.Interval -= countdownTimer.Interval / 10;

            if (wireCutResponse == "boom")
            {
                //view.ClearConsole();
                view.WriteHeaderAndRulesToUser();
                view.WriteToUser(model.GetWires());
                Explode();
                PlayAgain();
                shouldStart = true;
                StopCountdown();
                StopReceivingInput();
                //RoundFinished();
                //Start();

            }

            WriteRulesAndWiresToUser();
        }

        private void RoundFinished()
        {
            

            
            GameStartupFinished();
            PlayAgain();



            shouldStart = true;
            //StopCountdown();
            //StopReceivingInput();



        }

        private void StopReceivingInput()
        {
            threadUserInput.StopThread();
        }

        private void HandleTimerRanOut(object sender, EventArgs e)
        {

            if (model.GameIsWon())
            {
                Disarm();

            }
            else
            {
                Explode();

            }

            RoundFinished();
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
            shouldStart = false;
            model.InitializeWires();
            WriteRulesAndWiresToUser();
            countdownTimer.StartThread();
            //threadUserInput.StartThread();

        }

        private void WriteRulesAndWiresToUser()
        {
            view.WriteHeaderAndRulesToUser();
            view.WriteToUser(model.GetWires());
        }
        private void GameStartupFinished()
        {
            view.WriteHeaderAndRulesToUser();
            view.WriteToUser(model.GetWiresFinished());
        }

    }
}
