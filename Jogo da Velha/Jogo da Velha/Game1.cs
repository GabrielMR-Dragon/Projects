using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System.Threading;
using System;

namespace Tic_Tac_Toe
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Mouse state
        MouseState prevMouseState;

        //Board
        Classes.Board board = new Classes.Board();

        //Enum
        enum GameState
        {
            Null,
            Menu,
            PlayerTurn,
            MachineTurn,
            ShowResults
        };

        //Current State
        GameState currentState = GameState.Null;

        //Timer
        float stateTimer;

        //Flags
        bool playerVsPlayer; //Diz se é humano versus humano.
        bool machineIsFirst = true; //Diz se a máquina é o primeiro jogador ou não.
        bool humanIsX = true; //Diz se o humano é X.
        bool maxDifficulty = true; //Diz se é dificuldade máxima.

        //Sprites
        Texture2D cellEmpty;
        Texture2D cellO;
        Texture2D cellX;
        Texture2D playerO;
        Texture2D playerX;
        Texture2D cpuO;
        Texture2D cpuX;

        //Sounds
        SoundEffect sndTest;

        //Fonts
        SpriteFont fontNormal;

        //Buttons
        Classes.UIButton buttonStartGamePlayerPlayer;
        Classes.UIButton buttonStartGamePlayerMachine;
        Classes.UIButton buttonMachineIsFirst;
        Classes.UIButton buttonHumanIsX;
        Classes.UIButton buttonMaxDifficulty;
        Classes.UIButton buttonQuit;

        //Posição do mouse
        Vector2 mousePointer;

        void enterGameState(GameState newState)
        {
            leaveGameState();

            currentState = newState;

            switch (currentState)
            {
                case GameState.Menu:
                    {
                        board.Clear();
                    }
                    break;

                case GameState.PlayerTurn:
                    { }
                    break;

                case GameState.MachineTurn:
                    { }
                    break;

                case GameState.ShowResults:
                    {
                        stateTimer = 5;
                    }
                    break;
            }
        }

        void updateGameState(GameTime gameTime)
        {
            //Temporizador
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (currentState)
            {
                case GameState.Menu:
                    {
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                        {
                            //Posição do mouse
                            mousePointer = new Vector2(Mouse.GetState().Position.X,
                                                       Mouse.GetState().Position.Y);

                            if (buttonStartGamePlayerPlayer.testClick(mousePointer))
                            {
                                playerVsPlayer = true;
                                board.setPlayers(new Classes.Player(1, false, cellO), new Classes.Player(2, false, cellX));
                                board.setPlayer();
                                enterGameState(GameState.PlayerTurn);
                            }

                            else if (buttonStartGamePlayerMachine.testClick(mousePointer))
                            {
                                playerVsPlayer = false;

                                if (machineIsFirst)
                                {
                                    if (humanIsX)
                                        board.setPlayers(new Classes.Player(1, true, cellO), new Classes.Player(2, false, cellX));
                                    else
                                        board.setPlayers(new Classes.Player(1, true, cellX), new Classes.Player(2, false, cellO));

                                    board.setPlayer();
                                    enterGameState(GameState.MachineTurn);
                                }
                                    
                                else
                                {
                                    if (humanIsX)
                                        board.setPlayers(new Classes.Player(1, false, cellX), new Classes.Player(2, true, cellO));
                                    else
                                        board.setPlayers(new Classes.Player(1, false, cellO), new Classes.Player(2, true, cellX));

                                    board.setPlayer();
                                    enterGameState(GameState.PlayerTurn);
                                }
                            }

                            else if (buttonQuit.testClick(mousePointer))
                                Exit();

                            if (buttonMachineIsFirst.testClick(mousePointer))
                            {
                                if (machineIsFirst)
                                {
                                    machineIsFirst = false;
                                    buttonMachineIsFirst.setText("1º");
                                }
                                else
                                {
                                    machineIsFirst = true;
                                    buttonMachineIsFirst.setText("2º");
                                }
                            }
                            
                            if (buttonHumanIsX.testClick(mousePointer))
                            {
                                if (humanIsX)
                                {
                                    humanIsX = false;
                                    buttonHumanIsX.setText("O");
                                }
                                else
                                {
                                    humanIsX = true;
                                    buttonHumanIsX.setText("X");
                                }
                            }

                            if (buttonMaxDifficulty.testClick(mousePointer))
                            {
                                if (maxDifficulty)
                                {
                                    maxDifficulty = false;
                                    buttonMaxDifficulty.setText("Nrm");
                                }
                                else
                                {
                                    maxDifficulty = true;
                                    buttonMaxDifficulty.setText("Max");
                                }
                            }
                        }
                    }
                    break;

                case GameState.PlayerTurn:
                    {
                        if (Mouse.GetState().LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                        {
                            //Posição do mouse
                            mousePointer = new Vector2(Mouse.GetState().Position.X,
                                                       Mouse.GetState().Position.Y);
                        
                            if (buttonQuit.testClick(mousePointer))
                                enterGameState(GameState.Menu);
                        
                            for (int y = 0; y < 3; y++)
                            {
                                for (int x = 0; x < 3; x++)
                                {
                                    Vector2 pMin = new Vector2(x * 64 + getScreenSize(true, 64, 3),
                                                               y * 64 + getScreenSize(false, 64, 3));
                        
                                    Vector2 pMax = pMin + new Vector2(64, 64);
                        
                                    if ((mousePointer.X > pMin.X) && (mousePointer.X < pMax.X) &&
                                        (mousePointer.Y > pMin.Y) && (mousePointer.Y < pMax.Y) && board.cell[x, y] == 0)
                                    {
                                        board.cell[x, y] = board.getPlayer();
                        
                                        board.setPlayer();                    //Altera o jogador
                                        board.setDepth(board.getDepth() - 1); //Diminui a profunidade do tabuleiro
                        
                                        if (board.isGameOver())
                                            enterGameState(GameState.ShowResults);
                                        else if (playerVsPlayer)
                                            enterGameState(GameState.PlayerTurn);
                                        else
                                            enterGameState(GameState.MachineTurn);

                                        break;
                                    }
                                }
                            }
                        }
                    }
                    break;

                case GameState.MachineTurn:
                    {
                        //LÓGICA DIFICULDADE MÁXIMA
                        if (maxDifficulty)
                        {
                            List<Classes.Board> possibilities = board.getPossibilities(board.getPlayer());

                            Classes.Board bestPossibility = null;
                            List<Classes.Board> bestPossibilities = new List<Classes.Board>();
                            List<int> bestScores = new List<int>();

                            int bestScore = -9999999;

                            foreach (Classes.Board possibility in possibilities)
                            {
                                possibility.setPlayer(); //Altera para o jogador humano
                                int temporary = Classes.Board.Minimax(possibility, possibility.getDepth() - 1, possibility.getPlayer()); //Diminui profundidade

                                if (temporary >= bestScore)
                                {
                                    bestScore = temporary;
                                    bestPossibilities.Add(possibility);
                                    bestScores.Add(bestScore);
                                }
                            }

                            Random random = new Random();
                            int randomPossibility;

                            do
                            {
                                randomPossibility = random.Next(0, bestPossibilities.Count);
                            }
                            while (bestScores[randomPossibility] < bestScore);

                            bestPossibility = bestPossibilities[randomPossibility];

                            //TODO WAIT TIME
                            board = bestPossibility;              //Realiza jogada e altera jogador
                            board.setDepth(board.getDepth() - 1); //Diminui profundidade do tabuleiro
                        }

                        //LÓGICA DIFICULDADE NORMAL
                        else
                        {
                            Random random = new Random();
                            int randomPossibility;

                            randomPossibility = random.Next(1, 10);

                            if (randomPossibility >= 9)
                            {
                                List<Classes.Board> possibilities = board.getPossibilities(board.getPlayer());

                                Classes.Board bestPossibility = null;
                                List<Classes.Board> bestPossibilities = new List<Classes.Board>();
                                List<int> bestScores = new List<int>();

                                int bestScore = -9999999;

                                foreach (Classes.Board possibility in possibilities)
                                {
                                    possibility.setPlayer(); //Altera para o jogador humano
                                    int temporary = Classes.Board.Minimax(possibility, possibility.getDepth() - 1, possibility.getPlayer()); //Diminui profundidade

                                    if (temporary >= bestScore)
                                    {
                                        bestScore = temporary;
                                        bestPossibilities.Add(possibility);
                                        bestScores.Add(bestScore);
                                    }
                                }

                                do
                                {
                                    randomPossibility = random.Next(0, bestPossibilities.Count);
                                }
                                while (bestScores[randomPossibility] < bestScore);

                                bestPossibility = bestPossibilities[randomPossibility];

                                //TODO WAIT TIME
                                board = bestPossibility;              //Realiza jogada e altera jogador
                                board.setDepth(board.getDepth() - 1); //Diminui profundidade do tabuleiro
                            }

                            else
                            {
                                List<Classes.Board> possibilities = board.getPossibilities(board.getPlayer());

                                Classes.Board possibility = null;

                                randomPossibility = random.Next(0, possibilities.Count);

                                possibility = possibilities[randomPossibility];

                                //TODO WAIT TIME
                                board = possibility; //Realiza jogada
                                board.setPlayer(); //Altera jogador
                                board.setDepth(board.getDepth() - 1); //Diminui profundidade do tabuleiro
                            }
                        }
                        
                        if (board.isGameOver())
                            enterGameState(GameState.ShowResults);
                        else
                            enterGameState(GameState.PlayerTurn);
                    }
                    break;

                case GameState.ShowResults:
                    {
                        stateTimer -= dt;
                        if (stateTimer <= 0)
                            enterGameState(GameState.Menu);
                    }
                    break;
            }
        }

        float contaTempo(GameTime gameTime, float dt)
        {
            if (dt >= 1)
                return dt;
            else
                return contaTempo(gameTime, dt + (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        void drawGameState(GameTime gameTime)
        {
            switch (currentState)
            {
                case GameState.Menu:
                    {
                        printMenu(gameTime);
                    }
                    break;

                case GameState.PlayerTurn:
                    {
                        printBoard(gameTime);
                    }
                    break;

                case GameState.MachineTurn:
                    {
                        printBoard(gameTime);
                    }
                    break;

                case GameState.ShowResults:
                    {
                        printResults(gameTime);
                    }
                    break;
            }
        }

        void leaveGameState()
        {
            switch (currentState)
            {
                case GameState.Menu:
                    { }
                    break;

                case GameState.PlayerTurn:
                    { }
                    break;

                case GameState.MachineTurn:
                    { }
                    break;

                case GameState.ShowResults:
                    { }
                    break;
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            //Mudar o tamanho do tabuleiro
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            Content.RootDirectory = "Content";

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Carrega os gráficos para o personagem
            cellEmpty = Content.Load<Texture2D>("Sprites/CellEmpty");
            cellO = Content.Load<Texture2D>("Sprites/CellO");
            cellX = Content.Load<Texture2D>("Sprites/CellX");
            playerO = Content.Load<Texture2D>("Sprites/PlayerO");
            playerX = Content.Load<Texture2D>("Sprites/PlayerX");
            cpuO = Content.Load<Texture2D>("Sprites/CPUO");
            cpuX = Content.Load<Texture2D>("Sprites/CPUX");

            //Carrega as fontes para o jogo
            fontNormal = Content.Load<SpriteFont>("Fonts/Normal");

            buttonStartGamePlayerPlayer = new Classes.UIButton(new Vector2(194, 70), new Vector2(412, 50), cellEmpty, "Jogador Vs. Jogador", fontNormal);
            buttonStartGamePlayerMachine = new Classes.UIButton(new Vector2(194, 260), new Vector2(412, 50), cellEmpty, "Jogador Vs. Máquina", fontNormal);

            buttonMachineIsFirst = new Classes.UIButton(new Vector2(274, 346), new Vector2(64, 64), cellEmpty, "2º", fontNormal);
            buttonHumanIsX = new Classes.UIButton(new Vector2(368, 346), new Vector2(64, 64), cellEmpty, "X", fontNormal);
            buttonMaxDifficulty = new Classes.UIButton(new Vector2(462, 346), new Vector2(64, 64), cellEmpty, "Max", fontNormal);

            buttonQuit = new Classes.UIButton(new Vector2(336, 450), new Vector2(128, 50), cellEmpty, "Sair", fontNormal);

            //Carrega os sons do jogo
            sndTest = Content.Load<SoundEffect>("Sounds/Test");

            //Entra em estado inicial
            enterGameState(GameState.Menu);
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            updateGameState(gameTime);

            prevMouseState = Mouse.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //Inicia desenhos
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);

            //Desenha o estado atual do jogo
            drawGameState(gameTime);

            //Finaliza desenhos
            spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Calcula o tamanho da tela dinâmicamente.
        /// </summary>
        /// <param name="axisX">Calcular eixo X ou calcular eixo Y.</param>
        /// <param name="spriteDimension">Dimensão quadrada do sprite do tabuleiro.</param>
        /// <param name="boardDimension">Quantas colunas/linhas tem o tabuleiro.</param>
        /// <returns>Retorna tamanho em valor real.</returns>
        public float getScreenSize(bool axisX, int spriteDimension, int boardDimension)
        {
            float size;

            if (axisX)
                size = (graphics.PreferredBackBufferWidth - (spriteDimension * boardDimension)) / 2;
            else
                size = (graphics.PreferredBackBufferHeight - (spriteDimension * boardDimension)) / 2;

            return size;
        }

        private void printMenu(GameTime gameTime) //Desenho do menu
        {
            buttonStartGamePlayerPlayer.Draw(spriteBatch);
            buttonStartGamePlayerMachine.Draw(spriteBatch);
            buttonQuit.Draw(spriteBatch);

            buttonMachineIsFirst.Draw(spriteBatch);
            buttonHumanIsX.Draw(spriteBatch);
            buttonMaxDifficulty.Draw(spriteBatch);
        }

        private void printBoard(GameTime gameTime) //Desenho do tabuleiro
        {
            if (currentState != GameState.ShowResults)
                buttonQuit.Draw(spriteBatch);

            string text = "Tic-Tac-Toe!";
            string currentTime = "Elapsed time: ";

            Vector2 textSize = fontNormal.MeasureString(text);
            Vector2 timeSize = fontNormal.MeasureString(currentTime);

            spriteBatch.DrawString(fontNormal, text, new Vector2(400, 20), Color.White, 0.0f, textSize * 0.5f, Vector2.One, SpriteEffects.None, 0f);

            spriteBatch.DrawString(fontNormal, currentTime, new Vector2(400, 560), Color.White, 0.0f, textSize * 0.5f, Vector2.One, SpriteEffects.None, 0f);

            if (currentState != GameState.ShowResults)
            {
                if (board.getPlayer() == 1 && playerVsPlayer)
                    spriteBatch.Draw(playerO, new Vector2(0f, 479f), null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Blue, SpriteEffects.None, 0f);

                else if (board.getPlayer() == 2 && playerVsPlayer)
                    spriteBatch.Draw(playerX, new Vector2(599f, 479f), null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Red, SpriteEffects.None, 0f);
                
                if (board.getPlayer() == 1 && playerVsPlayer == false && board.getPlayers()[0].isMachine() && humanIsX)
                    spriteBatch.Draw(cpuO, new Vector2(0f, 479f), null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Blue, SpriteEffects.None, 0f);
                
                else if (board.getPlayer() == 1 && playerVsPlayer == false && board.getPlayers()[0].isMachine() == false && humanIsX)
                    spriteBatch.Draw(playerX, new Vector2(0f, 479f), null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Blue, SpriteEffects.None, 0f);
                
                else if (board.getPlayer() == 1 && playerVsPlayer == false && board.getPlayers()[0].isMachine() && humanIsX == false)
                    spriteBatch.Draw(cpuX, new Vector2(0f, 479f), null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Blue, SpriteEffects.None, 0f);

                else if (board.getPlayer() == 1 && playerVsPlayer == false && board.getPlayers()[0].isMachine() == false && humanIsX == false)
                    spriteBatch.Draw(playerO, new Vector2(0f, 479f), null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Blue, SpriteEffects.None, 0f);

                else if (board.getPlayer() == 2 && playerVsPlayer == false && board.getPlayers()[1].isMachine() && humanIsX)
                    spriteBatch.Draw(cpuO, new Vector2(599f, 479f), null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Red, SpriteEffects.None, 0f);
                
                else if (board.getPlayer() == 2 && playerVsPlayer == false && board.getPlayers()[1].isMachine() == false && humanIsX)
                    spriteBatch.Draw(playerX, new Vector2(599f, 479f), null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Red, SpriteEffects.None, 0f);

                else if (board.getPlayer() == 2 && playerVsPlayer == false && board.getPlayers()[1].isMachine() && humanIsX == false)
                    spriteBatch.Draw(cpuX, new Vector2(599f, 479f), null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Red, SpriteEffects.None, 0f);

                else if (board.getPlayer() == 2 && playerVsPlayer == false && board.getPlayers()[1].isMachine() == false && humanIsX == false)
                    spriteBatch.Draw(playerO, new Vector2(599f, 479f), null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Red, SpriteEffects.None, 0f);

            }

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Vector2 cellPos = new Vector2(x * 64 + getScreenSize(true, 64, 3),
                                                  y * 64 + getScreenSize(false, 64, 3));

                    spriteBatch.Draw(cellEmpty, cellPos);

                    if (board.cell[x, y] == 1)
                        spriteBatch.Draw(board.getPlayers()[0].getModel(), cellPos, null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Blue, SpriteEffects.None, 0f);
                    else if (board.cell[x, y] == 2)
                        spriteBatch.Draw(board.getPlayers()[1].getModel(), cellPos, null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Red, SpriteEffects.None, 0f);
                }
            }
        }

        private void printResults(GameTime gameTime)
        {
            string text;

            if (board.gameOver() == 1 && playerVsPlayer)
                text = "Player 1 wins!";
            else if (board.gameOver() == 2 && playerVsPlayer)
                text = "Player 2 wins!";
            else if (board.gameOver() == 1 && playerVsPlayer == false && board.getPlayers()[0].isMachine())
                text = "Machine wins!";
            else if (board.gameOver() == 1 && playerVsPlayer == false && board.getPlayers()[0].isMachine() == false)
                text = "Player 1 wins!";
            else if (board.gameOver() == 2 && playerVsPlayer == false && board.getPlayers()[1].isMachine())
                text = "Machine wins!";
            else if (board.gameOver() == 2 && playerVsPlayer == false && board.getPlayers()[1].isMachine() == false)
                text = "Player 2 wins!";
            else
                text = "It's a draw!";

            spriteBatch.DrawString(
              fontNormal,
              text,
              new Vector2((800 - fontNormal.MeasureString(text).X) / 2, (200 - fontNormal.MeasureString(text).Y) / 2),  //position
              Color.White,           //color
              0.0f,                  //rotation
              Vector2.Zero,          //origin (pivot)
              Vector2.One,           //scale
              SpriteEffects.None,
              0.0f
            );

            printBoard(gameTime);
        }
    }
}
