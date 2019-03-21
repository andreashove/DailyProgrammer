using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace _372_perfectlyBalanced
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = false;
            while (!result)
            {
                Console.WriteLine("Input a string to check the balance: ");
                var input = Console.ReadLine();
                result = balanced(input);
                Console.WriteLine(result.ToString());
            }
        }

        private static bool balanced(string s)
        {
            ConcurrentDictionary<char,int> cd = new ConcurrentDictionary<char, int>();

            foreach (char c in s)
                cd.AddOrUpdate(c, 1, (chr, count) => count + 1);

            if (cd["x".ToCharArray()[0]] == cd["y".ToCharArray()[0]])
                return true;
            else
                return false;
        }
    }
}
