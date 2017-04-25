using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tic_Tac_Toe.Classes
{
    public class Player
    {
        private int playerNumber; //1 ou 2
        private bool machine; //Define se é humano ou máquina

        //Construtor
        public Player(int playerNumber, bool machine)
        {
            this.playerNumber = playerNumber;
            this.machine = machine;
        }

        //Setters
        public void setPlayerNumber(int playerNumber)
        {
            this.playerNumber = playerNumber;
        }

        public void setMachine(bool machine)
        {
            this.machine = machine;
        }

        //Getters
        public int getPlayerNumber()
        {
            return playerNumber;
        }

        public bool isMachine()
        {
            return machine;
        }
    }
}
