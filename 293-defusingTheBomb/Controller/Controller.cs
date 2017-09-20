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

        private int EXPLOSION = 3;
        private int VICTORY = 4;
        const string EMPTY = "";
        private bool difficulty;

        public Controller()
        {
            view = new View();
            model = new Model();
            countdownTimer = new Countdown();

            countdownTimer.BombTimerRanOut += HandleTimerRanOut;

            
        }

        private void HandleTimerRanOut(object sender, EventArgs e)
        {
            if (model.GameIsWon())
            {
                view.ClearConsole();
                GameStartup();
                
                view.WriteToUser(VICTORY);
                countdownTimer.PlayDisarmAndStopThread();

            }
            else
            {
                view.WriteToUser(EXPLOSION);
                countdownTimer.PlayExplosionAndStopThread();
            }
            
            InitializeGame();
        }

        private void InitializeGame()
        {
            model.InitializeWires();
            view.WriteToUser("Play again?");

            view.GetUserInput();

            countdownTimer = new Countdown();
            countdownTimer.BombTimerRanOut += HandleTimerRanOut;
            countdownTimer.StartThread();
        }

        public void Start()
        {
            PromptUserToChooseDifficulty();
            countdownTimer.StartThread();

            

            
            int wireToCut;
            string wireCutResponse = EMPTY;
            string reply;

            while (true)
            {
                GameStartup();

                if (wireCutResponse != EMPTY)
                {
                    if (wireCutResponse == "won")
                    {
                        view.WriteToUser(VICTORY);
                        countdownTimer.PlayDisarmAndStopThread();
                    }


                    else if (wireCutResponse == "boom")
                    {
                        countdownTimer.Interval = 15;
                        countdownTimer.PlayExplosionAndStopThread();
                        view.WriteToUser(EXPLOSION);

                    }






                    model.InitializeWires();
                    view.WriteToUser("Play again?");

                    view.GetUserInput();
                    wireCutResponse = EMPTY;
                    countdownTimer = new Countdown();
                    countdownTimer.BombTimerRanOut += HandleTimerRanOut;
                    countdownTimer.StartThread();
                    continue;
                }

                reply = view.GetUserInput();

                try
                {
                    if (String.IsNullOrEmpty(reply))
                        continue;
                    wireToCut = int.Parse(reply);
                    wireToCut--; // due to 0 indexing
                    if (wireToCut < 0 || wireToCut > 5)
                        continue;

                    wireCutResponse = model.CutWire(wireToCut);
                    countdownTimer.Interval -= countdownTimer.Interval / 10;




                }
                catch (FormatException)
                {
                    continue;
                }

                view.ClearConsole();
            }

        }

        private void PromptUserToChooseDifficulty()
        {
            view.WriteToUser("Difficult mode? (y/n)");

            string reply = view.GetUserInput();
            difficulty = reply == "y" ? true : false;

            model.UpdateDifficultySetting(difficulty);
            view.ClearConsole();
        }

        private void GameStartup()
        {
            view.WriteHeaderAndRulesToUser();
            view.WriteToUser(model.GetWires());
        }

    }
}
