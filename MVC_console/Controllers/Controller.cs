using System;
using System.Speech.Recognition;

namespace MVC_console
{
    //Controller: Event Logic

    public class Controller : IController
    {
        private IModel model;
        private IView view;
        SpeechRecognizer sr = new SpeechRecognizer();

        public Controller()
        {
            view  = new View();
            model = new Model();

            Choices colors = new Choices();
            colors.Add(new string[] { "tell me the time", "time" });

            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(colors);

            // Create the Grammar instance.
            Grammar g = new Grammar(gb);

            sr.LoadGrammar(g);

            sr.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sr_SpeechRecognized);
        }

        private void sr_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence >= 0.1)
            {
                Console.Write("Reconized ~ " + e.Result.Text + " ~ with confidence " + e.Result.Confidence);
                Console.WriteLine();
            }
            
            //view.DisplayAndSpeakTextToUser("You said: " + e.Result.Text);
            //Console.WriteLine(e.Result.Text);
        }

        public void Start()
        {
            LoopConversionOfTimeToSpeech(); 
        }

        private void LoopConversionOfTimeToSpeech()
        {
            while (true)
            {
                view.DisplayAndSpeakTextToUser(model.HandleUserInput(view.PromptUser()));
            }
        }
    }
}
