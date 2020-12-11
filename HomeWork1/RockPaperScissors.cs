using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork1
{
    class RockPaperScissors
    {
        //determine the winner between two players
        public static void WinnerDetermine2(in string player1, in string player2, out bool outcome1, out bool outcome2)
        {
            if(player1 == player2)
            {
                outcome1 = outcome2 = true;     //draw
                return;
            }

            if(player1 == "rock" && player2 == "scissors" 
                || player1 == "paper" && player2 == "rock"
                || player1 == "scissors" && player2 == "paper")
            {
                outcome1 = true;                //first player win
                outcome2 = false;               //second player lose
                return;
            }

            if (player1 == "scissors" && player2 == "rock"
                || player1 == "rock" && player2 == "paper"
                || player1 == "paper" && player2 == "scissors")
            {
                outcome1 = false;               //first player lose
                outcome2 = true;                //second player win
                return;
            }


            outcome1 = outcome2 = false;        //wrong commands received
            return;

        }
    }
}
