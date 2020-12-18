using System;

namespace Library
{
    public class BetService
    {
        public decimal Odd { get; private set; }

        const decimal MinOdd = 1.01m;
        const decimal MaxOdd = 25m;

        public BetService()
        {
            Odd = RandomOdd();
        }

        //trigger of a new coefficient value formation!
        public float GetOdds()
        {
            Odd = RandomOdd();
            return (float)Odd;
        }

        bool IsWon()
        {
            int winningChance = (int)Math.Round(100/Odd);

            Random rand = new Random();
            int randomOutcome = rand.Next(1, 101);

            if (randomOutcome <= winningChance)             //win condition: randOutcome from 1 to winningChance (including)
                return true;
            else                                            //loss condition: randOutcome from winningChance+1 to 100 (including)
                return false;

        }


        //"slot machine" emulator
        public decimal Bet(decimal amount)
        {
            if (IsWon() == true)
                return Odd * amount;
            else
                return 0;
        }


        //generate random odd (in set range)
        decimal RandomOdd()
        {
            decimal odd;
            
            Random rand = new Random();

            double randomDouble = rand.NextDouble();
            odd = (decimal)randomDouble * (MaxOdd - MinOdd) + MinOdd;

            return Math.Round(odd, 2);
        }
    }
}
