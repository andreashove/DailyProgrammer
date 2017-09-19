using System;
using System.Collections.Generic;

namespace _293_defusingTheBomb
{
    class View : IView
    {
        string initUserPrompt =
        @" 
          ____        __               _____ _          ____                  _     
         |  _ \  ___ / _|_   _ ___  __|_   _| |__   ___| __ )  ___  _ __ ___ | |__  
         | | | |/ _ \ |_| | | / __|/ _ \| | | '_ \ / _ \  _ \ / _ \| '_ ` _ \| '_ \ 
         | |_| |  __/  _| |_| \__ \  __/| | | | | |  __/ |_) | (_) | | | | | | |_) |
         |____/ \___|_|  \__,_|___/\___||_| |_| |_|\___|____/ \___/|_| |_| |_|_.__/ 
        ";

        string explosion =
        @"
          ____                        _ 
         | __ )  ___   ___  _ __ ___ | |
         |  _ \ / _ \ / _ \| '_ ` _ \| |
         | |_) | (_) | (_) | | | | | |_|
         |____/ \___/ \___/|_| |_| |_(_)
        ";


        string victory =
        @" 
         __     ___      _                   _ 
         \ \   / (_) ___| |_ ___  _ __ _   _| |
          \ \ / /| |/ __| __/ _ \| '__| | | | |
           \ V / | | (__| || (_) | |  | |_| |_|
            \_/  |_|\___|\__\___/|_|   \__, (_)
                                       |___/  
        ";

        string disarmed =
        @" 
          ____  _                                   _ _ 
         |  _ \(_)___  __ _ _ __ _ __ ___   ___  __| | |
         | | | | / __|/ _` | '__| '_ ` _ \ / _ \/ _` | |
         | |_| | \__ \ (_| | |  | | | | | |  __/ (_| |_|
         |____/|_|___/\__,_|_|  |_| |_| |_|\___|\__,_(_)
                                                
        ";


        const int RULES = 0;
        const int WHICH_WIRE_TO_CUT = 1;
        

        List<string> userPrompts;

        public View()
        {
            userPrompts = new List<string>()
            {
                @"If you cut a:  
                - white cable you can't cut (white) or black cable.
                - red cable you have to cut a green one.
                - black cable it is not allowed to cut a white, green or orange one.
                - orange cable you should cut a red or black one.     
                - green cable you have to cut a orange or white one.
                - purple cable you can't cut a (purple), green, orange or white cable",
                "Bomb go boom, unless you cut the correct wires.",
                "Which wire would you like to cut?",
                "\n\n\n************************\n********       *********\n******** BOOM! *********\n********       *********\n************************\n\n\n",
                "\n\n\n************************\n********       *********\n******* VICTORY! *******\n********       *********\n************************\n\n\n"


            };

            
            
        
        }
        public void ClearConsole()
        {
            Console.Clear();
        }
        public void WriteHeaderAndRulesToUser()
        {
            ClearConsole();
            Console.WriteLine(initUserPrompt);
            Console.WriteLine(userPrompts[RULES]);
        }

       
        public void WriteToUser(string str)
        {

            if (String.IsNullOrEmpty(str))
                Console.WriteLine(userPrompts[WHICH_WIRE_TO_CUT]);
            else
                Console.WriteLine(str);
            
        }

        public void WriteToUser(int i)
        {
            //ClearConsole();
            if (i==3)
                Console.WriteLine(explosion);
            if (i==4)
                Console.WriteLine(disarmed);
        }


        public string GetUserInput()
        {
            return Console.ReadLine();
        }
    }
}
