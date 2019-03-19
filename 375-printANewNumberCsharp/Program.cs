using System;

namespace _375_printANewNumberCsharp
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("A number is input in computer then a new no should get printed by adding one to each of its digit:");
                var input = Console.ReadLine();
                int value;
                var result = "";
                if (int.TryParse(input, out value))
                {
                    var inputArr = input.ToCharArray();
                    foreach (Char c in inputArr)
                    {
                        int converted;
                        int.TryParse(c.ToString(), out converted);
                        converted += 1;
                        result += converted.ToString();
                    }
                    Console.WriteLine("Result: " + result);
                    Console.WriteLine("Any key to quit");
                    Console.Read();
                    Environment.Exit(0);


                }
                else
                {
                    continue;
                }
            }
        }
    }
}
