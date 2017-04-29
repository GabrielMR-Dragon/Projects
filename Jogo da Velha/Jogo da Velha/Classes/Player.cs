using Microsoft.Xna.Framework.Graphics;
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
        private Texture2D model; //Define se é bolinha ou xis

        //Construtor
        public Player(int playerNumber, bool machine, Texture2D model)
        {
            this.playerNumber = playerNumber;
            this.machine = machine;
            this.model = model;
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

        public void setModel(Texture2D model)
        {
            this.model = model;
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

        public Texture2D getModel()
        {
            return model;
        }
    }
}
