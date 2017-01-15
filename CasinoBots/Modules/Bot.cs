using KriPod.Primedice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CasinoBots.Modules
{
    class Bot
    {
        public string Access { get; set; }

        public delegate void LogMethod(string message);
        protected LogMethod Log;
        int Cycles = 100;

        public PrimediceClient Client;

        public Bot(string token, LogMethod log, int cycles = 100)
        {
            Access = token;
            Log = log;
            Cycles = cycles;
        }

        public async Task Bet()
        {

            Client = new PrimediceClient(Access);
            bool last = true;
            double amount = 1;
            decimal total = 0;

            while(true)
            {

                if (!last)
                    amount *= 2;
                else
                    amount = 1;

                var bet = await Client.Bets.Create(amount, BetCondition.GreaterThan, 50.49f);

                if (bet.IsWon)
                    total++;

                last = bet.IsWon;

                Log("Last is " + last + "; amount: " + amount + "; total: " + total + "; Roll: " + bet.Roll + "; target: " + bet.Target);

                Thread.Sleep(1800);

            }


        }
    }
}
