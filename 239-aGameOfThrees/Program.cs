using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _239_aGameOfThrees
{
    class Program
    {
        static void Main(string[] args)
        {
            string _ans;
            string _in;
            int _number;
            bool _notDone = true;

            Console.WriteLine("Game of Threes! Input a number (integer) to be divided by 3 until it is 1\n");
                

            while (_notDone)
            {
                Console.Write("Input number/integer: ");
                _in = Console.ReadLine();
                if (String.IsNullOrEmpty(_in))
                {
                    Console.Write("Continue? (y/n): ");
                    _ans = Console.ReadLine();
                    if (_ans == "n")
                        _notDone = false;
                    Console.WriteLine("");
                    continue;
                }
                    

                try
                {
                    _number = int.Parse(_in);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Error: \""+_in+"\" is not an integer.");
                    continue;
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Error: \"" + _in + "\" is too large or too small.");
                    continue;
                }
                

                Divide(_number);

                Console.WriteLine("");
                Console.Write("Continue? (y/n): ");
                _ans = Console.ReadLine();
                if (_ans == "n")
                    _notDone = false;
                Console.WriteLine("");

            }
            
        }

        private static void Divide(int n)
        {
            Console.Write(n);

            if (n == 1 || n == -1)
                return;
            

            if (n % 3 == 0)
            {
                Console.WriteLine("");
                n = n/3;
                
            }
            else if ((n + 1) % 3 == 0)
            {
                Console.WriteLine(" + 1");
                n = (n + 1)/3;
                

            }
            else if ((n - 1) % 3 == 0)
            {
                Console.WriteLine(" - 1");
                n = (n - 1)/3;

            }
            

            Divide(n);
        }
    }
}
