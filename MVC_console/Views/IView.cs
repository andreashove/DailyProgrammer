using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_console
{
    interface IView
    {
        void DisplayAndSpeakTextToUser(string textToUser);
        string PromptUser();
    }
}
