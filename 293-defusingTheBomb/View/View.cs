using System;
using System.Collections.Generic;

namespace _293_defusingTheBomb
{
    class View : IView
    {
        const int EXPLOSION = 0;
        const int DISARM = 1;
        string header =
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

        string disarmed =
        @" 
          ____  _                                   _ _ 
         |  _ \(_)___  __ _ _ __ _ __ ___   ___  __| | |
         | | | | / __|/ _` | '__| '_ ` _ \ / _ \/ _` | |
         | |_| | \__ \ (_| | |  | | | | | |  __/ (_| |_|
         |____/|_|___/\__,_|_|  |_| |_| |_|\___|\__,_(_)
                                                
        ";

        string rules =

        @"
        +---------+------------------------+-----------------+
        | If  cut |       Can't cut        |    Must cut     |
        +---------+------------------------+-----------------+
        | white   | black                  |                 |
        | red     |                        | green           |
        | black   | white, green or orange |                 |
        | orange  |                        | red or black    |
        | green   |                        | orange or white |
        | purple  | green, orange or white |                 |
        +---------+------------------------+-----------------+
        "
        ;


        public View()
        {
            
        }

        public void ClearConsole()
        {
            Console.Clear();
        }
        public void WriteHeaderAndRulesToUser()
        {
            ClearConsole();
            Console.WriteLine(header);
            Console.WriteLine(rules);
        }

       
        public void WriteToUser(string str)
        {
            Console.WriteLine(str);
        }

        public void WriteToUser(int i)
        {
            if (i==0)
                Console.WriteLine(explosion);
            if (i==1)
                Console.WriteLine(disarmed);
        }

        public string GetUserInput()
        {
            return Console.ReadLine();
        }

        public int GetValidatedUserInput()
        {
            var inputValid = false;
            var input = "";
            var intInput = -1;

            while (!inputValid)
            {
                var inp = Console.ReadKey().Key; 
                switch (inp)
                {
                    case ConsoleKey.D1:
                        input = "1";
                        break;

                    case ConsoleKey.D2:
                        input = "2";
                        break;

                    case ConsoleKey.D3:
                        input = "3";
                        break;

                    case ConsoleKey.D4:
                        input = "4";
                        break;

                    case ConsoleKey.D5:
                        input = "5";
                        break;

                    case ConsoleKey.D6:
                        input = "6";
                        break;

                    default:
                        input = "-1";
                        break;


                }
                //input = Console.ReadLine();

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

            return intInput;
        }
    }
}
