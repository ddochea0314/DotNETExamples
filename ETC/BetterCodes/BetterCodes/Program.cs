using System;

namespace BetterCodes
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Substring이 간소화 되었네요.".Substring(0, 9));
            Console.WriteLine("Substring이 간소화 되었네요."[0..9]);
        }
    }
}
