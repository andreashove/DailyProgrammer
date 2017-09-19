using System;
using System.Speech.Synthesis;
using System.Speech.Recognition;

namespace MVC_console
{
    //View: Display Logic

    public class View : IView
    {
        SpeechSynthesizer speaker = new SpeechSynthesizer();
        
        private string stringUserPrompt = "Input a time (hh:mm) or hit Enter to get current time ('q' to quit): ";

        public View()
        {
            speaker.SelectVoice("Microsoft Zira Desktop");
            

        }

        

        public void DisplayAndSpeakTextToUser(string textToUser)
        {            
            Console.WriteLine(textToUser);
            speaker.Speak(textToUser);
        }

        public string PromptUser()
        {
            Console.Write(stringUserPrompt);
            return Console.ReadLine();
        }
    }
}
