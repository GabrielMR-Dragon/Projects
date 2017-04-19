using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tic_Tac_Toe.Classes
{
    public class Board
    {
        //Cell values:
        //0 = empty
        //1 = computer
        //2 = human

        private int player = 0;
        public int[,] cell = new int[3, 3];
        private int depth = 0;

        //Construtor
        public Board()
        {
            Clear();
        }

        //Setters
        public void setPlayer()
        {
            if (player == 1)
                player = 2;
            else
                player = 1;
        }

        public void setDepth(int depth)
        {
            this.depth = depth;
        }

        //Getters
        public int getPlayer()
        {
            return player;
        }

        public int getDepth()
        {
            return depth;
        }

        //Funções:
        public void Clear()
        {
            for (int x = 0; x < 3; x++)
                for (int y = 0; y < 3; y++)
                    cell[x, y] = 0;

            player = 0;
            depth = 9;
        }

        public int getScore()
        {
            int winner = gameOver();

            if (winner == 1)
                return 1;
            else if (winner == 2)
                return -1;
            else
                return 0;
        }

        public bool isGameOver()
        {
            if (gameOver() > 0)
                return true;

            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 3; x++)
                    if(cell[x, y] == 0)
                        return false;

            return true;
        }

        public int gameOver()
        {
            for(int player = 1; player <= 2; player++)
            {
                //Testa linhas
                for (int y = 0; y < 3; y++)
                {
                    if (cell[0, y] == player && cell[1, y] == player && cell[2, y] == player)
                        return player;
                }

                //Testa colunas
                for (int x = 0; x < 3; x++)
                {
                    if (cell[x, 0] == player && cell[x, 1] == player && cell[x, 2] == player)
                        return player;
                }

                {//Testa diagonais
                    if (cell[0, 0] == player && cell[1, 1] == player && cell[2, 2] == player)
                        return player;

                    if (cell[2, 0] == player && cell[1, 1] == player && cell[0, 2] == player)
                        return player;
                }
            }

            return 0;
        }

        public Board copyBoard()
        {
            Board copy = new Board();

            for (int y = 0; y < 3; y++)
                for (int x = 0; x < 3; x++)
                    copy.cell[x, y] = cell[x, y];

            copy.player = player;

            return copy;
        }

        public List<Board> getPossibilities(int player)
        {
            List<Board> result = new List<Board>();

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if(cell[x, y] == 0)
                    {
                        Board copy = copyBoard();

                        copy.cell[x, y] = player;

                        result.Add(copy);
                    }
                }
            }

            return result;
        }

        static public int Minimax(Board board, int depth, int player)
        {
            //if gameOver(tabuleiro) ou depth == 0
            //return CalcularScore(tabuleiro)

            if (board.isGameOver() || depth == 0)
                return board.getScore();

            int value;

            if(player == 2)
            {
                value = 9999999;

                List<Board> possibilities = board.getPossibilities(2);

                foreach(Board p in possibilities)
                    value = Math.Min(value, Minimax(p, depth - 1, 1 /* Máquina */));
            }
            else
            {
                value = -9999999;

                List<Board> possibilities = board.getPossibilities(1);

                foreach(Board p in possibilities)
                    value = Math.Max(value, Minimax(p, depth - 1, 2 /* Humano */));
            }

            return value;
        }
    }
}
