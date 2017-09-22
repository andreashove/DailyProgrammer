using System;
using System.Threading;


namespace _293_defusingTheBomb
{
    public class QueryUser
    {
        private Thread thread;
        public event EventHandler<ReceivedDataEventArgs> SendData;

        private void InvokeSendDataEvent(object o, ReceivedDataEventArgs a)
        {
            if (SendData != null)
                SendData(this, a);
        }

        internal void SendBackUserInput(int input)
        {

            ReceivedDataEventArgs ea = new ReceivedDataEventArgs() { UserInput = input };
            InvokeSendDataEvent(this, ea);
        }

        public QueryUser()
        {
            /*thread = new Thread(waitHandle =>
            {
                (waitHandle as ManualResetEvent).Set();
            })*/
            thread = new Thread(new ThreadStart(GetValidatedUserInput));
            thread.IsBackground = true;

        }

        public void StartThread(ManualResetEvent[] mre)
        {
            mre[0] = new ManualResetEvent(false);
            
            thread.Start(mre);
        }

        public void StartThread()
        {
            thread = new Thread(new ThreadStart(GetValidatedUserInput));
            thread.IsBackground = true;
            thread.Start();
        }
        public void StopThread()
        {
            thread.Abort();
        }

        private void GetValidatedUserInput()
        {
            
            while (true)
            {
                    var inputValid = false;
                    var input = "";
                    var intInput = -1;

                    while (!inputValid)
                    {
                        input = Console.ReadLine();

                        try
                        {
                            if (String.IsNullOrEmpty(input))
                                continue;
                            intInput = int.Parse(input);
                            intInput--; // due to 0 indexing
                            if (intInput < 0 || intInput > 5)
                                continue;

                            inputValid = true;

                        }
                        catch (FormatException)
                        {
                            continue;
                        }
                    }

                    SendBackUserInput(intInput);

            }
        }
    }
}
