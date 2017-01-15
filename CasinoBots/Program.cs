using CasinoBots.Modules;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CasinoBots
{
    class Program
    {
        static void Main(string[] args)
        {
            GetArgs(args);

            Bot b = new Bot("eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpZCI6IjgxNjE5MyIsInRva2VuIjoiNDM5NGRiZWQ1NTk3NjU5NDFiZjhlMTU1ZTQ4NzMzM2YifQ.eYzIGqozh-sK2jmqSOwa-IlTJvrg96hIWH6ldlSJl7U.c4544bccf95fb1ab4f927dcf3909c5f6", Log);
            b.Bet().Wait();
        }

        public static void Log(string message)
        {
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine($"{DateTime.Now.ToShortTimeString()} -- {message}");
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
        }

        static void GetArgs(string[] args)
        {
            int i; 
            i = Array.FindIndex(args, x => x == "-h");

            if(i >= 0 && args.Length > i + 1)
            {
                Logger.HHash = args[i + 1];
            }
            else
            {
                Logger.RandomiseHHash();
            }
        }
    }
}
