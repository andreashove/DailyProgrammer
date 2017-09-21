using System;
using System.ComponentModel;
using System.Threading;

namespace _293_defusingTheBomb
{
    public class Controller : IController
    {
        IView view;
        IModel model;
        Countdown countdownTimer;
        Sounds soundEffects;

        private int EXPLOSION = 3;
        private int VICTORY = 4;
        const string EMPTY = "";
        bool running = true;

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
                StopCountdownThread();
                PlayAgain();
            }
        }

        private void StopCountdownThread()
        {
            countdownTimer.StopThread();
        }

        private void HandleTimerRanOut(object sender, EventArgs e)
        {
            running = false;

            if (model.GameIsWon())
            {
                Disarm();

            }
            else
            {
                Explode();
            }
        }

        private void Disarm()
        {
            view.ClearConsole();
            GameStartupFinished();
            view.WriteToUser(VICTORY);
            soundEffects.PlayDisarmed();

        }
        private void Explode()
        {
            countdownTimer.Interval = 15;
            view.ClearConsole();
            GameStartupFinished();
            view.WriteToUser(EXPLOSION);
            soundEffects.PlayExplosion();

        }
       
        private void PlayAgain()
        {
            view.WriteToUser("Play again?");
            view.GetUserInput();
            running = true;
        }
        public void Start()
        {
            model.InitializeWires();
            countdownTimer.StartThread();
            while (running)
            {
                
                
                StartupServeRulesAndWiresToUser();

                var wireToCut = view.GetValidatedUserInput();

                var wireCutResponse = model.CutWire(wireToCut);
                countdownTimer.Interval -= countdownTimer.Interval / 10;

                if (wireCutResponse == "boom")
                {

                    Explode();
                    running = false;
                } 
            }
        }

        private void StartupServeRulesAndWiresToUser()
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
