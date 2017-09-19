using System;
using System.Threading;

namespace _293_defusingTheBomb
{
    public class Controller : IController
    {
        IView view;
        IModel model;
        private int EXPLOSION = 3;
        private int VICTORY = 4;
        const string EMPTY = "";
        private bool DIFFICULT;

        public Controller()
        {
            view = new View();
            model = new Model();
            
        }

        public void Start()
        {
            StartCountdown();

            string reply;

            view.WriteToUser("Difficult mode? (y/n)");
            reply = view.GetUserInput();
            DIFFICULT = reply == "y" ? true : false;

            model.UpdateDifficultySetting(DIFFICULT);
            view.ClearConsole();

            
            int wireToCut;
            string wireCutResponse = EMPTY;

            while (true)
            {
                
                view.WriteHeaderAndRulesToUser();
                
                view.WriteToUser(model.GetWires());
                

                if (wireCutResponse != EMPTY)
                {
                    if (wireCutResponse == "won")
                    
                        view.WriteToUser(VICTORY);

                    else if (wireCutResponse == "boom")
                    
                        view.WriteToUser(EXPLOSION);

                        


                    model.InitializeWires();
                    view.WriteToUser("Play again?");

                    view.GetUserInput();
                    wireCutResponse = EMPTY;
                    //StartCountdown();
                    continue;
                }

                reply = view.GetUserInput();

                try
                {
                    wireToCut = int.Parse(reply);
                    wireToCut--; // due to 0 indexing
                    if (wireToCut < 0 || wireToCut > 5)
                        continue;

                    wireCutResponse = model.CutWire(wireToCut);
                    
                     
                    
     
                }
                catch (FormatException)
                {
                    continue;
                }

                view.ClearConsole();
            }
            
        }

        private void StartCountdown()
        {
            Thread beepThread = new Thread(new ThreadStart(PlayBeep));
            beepThread.IsBackground = true;
            beepThread.Start();


            
        }

        private void PlayBeep()
        {
            int i = 0;
            int freq = 2000;
            int interval = 500;
            while (true)
            {
                if (i % 10 == 0)
                    interval = interval / 2;
                if (interval == 125)
                {
                    view.WriteToUser(EXPLOSION);
                    model.InitializeWires();
                    view.WriteToUser("Play again?");

                    view.GetUserInput();
                    interval = 500;
                }
                Console.Beep(freq, interval);
                Thread.Sleep(interval);
                i++;
            }
        }
    }
}
