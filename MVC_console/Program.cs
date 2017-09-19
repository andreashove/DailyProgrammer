using System;

namespace MVC_console
{
    /* Simple MVC console app setup: https://stackoverflow.com/questions/1108247/mvc-like-design-for-console-applications */
    
    class Program
    {
        static void Main(string[] args)
        {         
            IController ctr = new Controller();
            ctr.Start();
        }
    }
}
