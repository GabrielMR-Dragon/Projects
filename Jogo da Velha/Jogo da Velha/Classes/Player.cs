using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tic_Tac_Toe.Classes
{
    public class Player
    {
        private int score;
        private bool circle; //Define se é círculo ou x
        private string name;

        //Construtor
        public Player(string name)
        {
            this.name = name;
            score = 0;
        }

        //Setters
        public void setScore(int score)
        {
            this.score = score;
        }

        public void setCircle(bool circle)
        {
            this.circle = circle;
        }

        public void setName(string name)
        {
            this.name = name;
        }

        //Getters
        public int getScore()
        {
            return score;
        }

        public bool isCircle()
        {
            return circle;
        }

        public string getName()
        {
            return name;
        }
    }
}
