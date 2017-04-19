using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Threading;

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
        bool gameStarted = false; //Diz se o jogo iniciou ou não.
        bool playerVsPlayer; //Diz se é humano versus humano.
        bool machineIsFirst = true; //Diz se a máquina é o primeiro jogador ou não.
        bool gameFinished; //Diz se o jogo terminou.

        //Sprites
        Texture2D cellEmpty;
        Texture2D cellO;
        Texture2D cellX;
        Texture2D playerO;
        Texture2D playerX;
        Texture2D cpuO;
        Texture2D cpuX;

        //Fonts
        SpriteFont fontNormal;

        //Buttons
        Classes.UIButton buttonStartGamePlayerPlayer;
        Classes.UIButton buttonStartGamePlayerMachine;
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
                    {

                    }
                    break;

                case GameState.MachineTurn:
                    {

                    }
                    break;

                case GameState.ShowResults:
                    {

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
                                //gameStarted = true;
                                //board.setPlayer();
                                enterGameState(GameState.PlayerTurn);
                            }

                            else if (buttonStartGamePlayerMachine.testClick(mousePointer))
                            {
                                playerVsPlayer = false;
                                //gameStarted = true;
                                //board.setPlayer();
                                enterGameState(GameState.MachineTurn);
                            }

                            else if (buttonQuit.testClick(mousePointer))
                                Exit();

                            if (Mouse.GetState().LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                            {
                                Vector2 pMin = new Vector2(720, 128);

                                Vector2 pMax = pMin + new Vector2(64, 64);

                                if ((mousePointer.X > pMin.X) && (mousePointer.X < pMax.X) &&
                                    (mousePointer.Y > pMin.Y) && (mousePointer.Y < pMax.Y))
                                {
                                    if (machineIsFirst)
                                        machineIsFirst = false;
                                    else
                                        machineIsFirst = true;
                                }
                            }
                        }
                    }
                    break;

                case GameState.PlayerTurn:
                    {

                    }
                    break;

                case GameState.MachineTurn:
                    {
                        //RANDOMIZAR JOGADAS DA MÁQUINA

                        //List<Classes.Board> possibilities = board.getPossibilities(1);

                        //board = possibilities[0];

                        //RANDOMIZAR JOGADAS DA MÁQUINA
                    }
                    break;

                case GameState.ShowResults:
                    {

                    }
                    break;
            }
        }

        void drawGameState(GameTime gameTime)
        {
            switch (currentState)
            {
                case GameState.Menu:
                    {

                    }
                    break;

                case GameState.PlayerTurn:
                    {

                    }
                    break;

                case GameState.MachineTurn:
                    {

                    }
                    break;

                case GameState.ShowResults:
                    {

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

            ///
            /// Mudar o tamanho do tabuleiro
            ///
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

            buttonStartGamePlayerPlayer = new Classes.UIButton(new Vector2(10, 70), new Vector2(512, 50), cellEmpty, "Jogador Vs. Jogador", fontNormal);
            buttonStartGamePlayerMachine = new Classes.UIButton(new Vector2(10, 140), new Vector2(512, 50), cellEmpty, "Jogador Vs. Máquina", fontNormal);
            buttonQuit = new Classes.UIButton(new Vector2(10, 210), new Vector2(128, 50), cellEmpty, "Sair", fontNormal);

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

            //if (gameStarted)
            //    logicBoard(gameTime);
            //else
            //    logicMenu(gameTime);

            prevMouseState = Mouse.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //Inicia desenhos
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);

            if (gameStarted)
                printBoard(gameTime);
            else
                printMenu(gameTime);
            
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

        private void logicMenu(GameTime gameTime) //Lógica do menu principal
        {
            //Posição do mouse
            mousePointer = new Vector2(Mouse.GetState().Position.X,
                                       Mouse.GetState().Position.Y);

            if (Mouse.GetState().LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
            {
                if (buttonStartGamePlayerPlayer.testClick(mousePointer))
                {
                    playerVsPlayer = true;
                    gameStarted = true;
                    board.setPlayer();
                }

                else if (buttonStartGamePlayerMachine.testClick(mousePointer))
                {
                    playerVsPlayer = false;
                    gameStarted = true;
                    board.setPlayer();
                }

                else if (buttonQuit.testClick(mousePointer))
                    Exit();

                if(Mouse.GetState().LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    Vector2 pMin = new Vector2(720, 128);

                    Vector2 pMax = pMin + new Vector2(64, 64);

                    if ((mousePointer.X > pMin.X) && (mousePointer.X < pMax.X) &&
                        (mousePointer.Y > pMin.Y) && (mousePointer.Y < pMax.Y))
                    {
                        if (machineIsFirst)
                            machineIsFirst = false;
                        else
                            machineIsFirst = true;
                    }
                }
            }
        }

        private void printMenu(GameTime gameTime) //Desenho do menu
        {
            buttonStartGamePlayerPlayer.Draw(spriteBatch);
            buttonStartGamePlayerMachine.Draw(spriteBatch);
            buttonQuit.Draw(spriteBatch);

            Vector2 cellPos = new Vector2(720, 128);

            spriteBatch.Draw(cellEmpty, cellPos);

            if (machineIsFirst == true)
                spriteBatch.Draw(cellX, cellPos, Color.Red);
            else
                spriteBatch.Draw(cellO, cellPos, Color.Blue);
        }

        private void logicBoard(GameTime gameTime) //Lógica do tabuleiro
        {
            if (playerVsPlayer)
            {
                if (Mouse.GetState().LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    //Posição do mouse
                    mousePointer = new Vector2(Mouse.GetState().Position.X,
                                               Mouse.GetState().Position.Y);

                    if (buttonQuit.testClick(mousePointer))
                    {
                        gameStarted = false;
                        board.Clear();
                        return;
                    }

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
                            }
                        }
                    }
                }
            }
            else
            {
                if (board.getPlayer() == 1)
                {
                    List<Classes.Board> possibilities = board.getPossibilities(board.getPlayer());

                    Classes.Board bestPossibility = null;
                    int bestScore = -9999999;

                    foreach (Classes.Board possibility in possibilities)
                    {
                        possibility.setPlayer(); //Altera para o jogador humano
                        int temporary = Classes.Board.Minimax(possibility, possibility.getDepth() - 1, possibility.getPlayer()); //Diminui profundidade

                        if (temporary > bestScore)
                        {
                            bestScore = temporary;
                            bestPossibility = possibility;
                        }
                    }

                    Thread.Sleep(1000);                   //TODO: SUBSTITUIR por um timer manual
                    board = bestPossibility;              //Realiza jogada e altera jogador
                    board.setDepth(board.getDepth() - 1); //Diminui profundidade do tabuleiro
                }

                else if (Mouse.GetState().LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released && board.getPlayer() == 2)
                {
                    //Posição do mouse
                    mousePointer = new Vector2(Mouse.GetState().Position.X,
                                               Mouse.GetState().Position.Y);

                    if (buttonQuit.testClick(mousePointer))
                    {
                        gameStarted = false;
                        board.Clear();
                        return;
                    }

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
                            }
                        }
                    }
                }
            }

            prevMouseState = Mouse.GetState();

            int vencedor = 0;
            vencedor = board.gameOver();

            if (vencedor > 0)
            {
                System.Console.WriteLine("Jogador vencedor: " + vencedor);
            }
            else if(vencedor == 0 && board.isGameOver())
            {
                System.Console.WriteLine("Empate.");
            }
        }

        private void printBoard(GameTime gameTime) //Desenho do tabuleiro
        {
            buttonQuit.Draw(spriteBatch);

            string text = "Tic-Tac-Toe!";
            string currentTime = "Elapsed time: " + (int)gameTime.TotalGameTime.Seconds;

            Vector2 textSize = fontNormal.MeasureString(text);
            Vector2 timeSize = fontNormal.MeasureString(currentTime);

            spriteBatch.DrawString(fontNormal, text, new Vector2(400, 20), Color.White, 0.0f, textSize * 0.5f, Vector2.One, SpriteEffects.None, 0f);

            spriteBatch.DrawString(fontNormal, currentTime, new Vector2(400, 560), Color.White, 0.0f, textSize * 0.5f, Vector2.One, SpriteEffects.None, 0f);

            if (board.getPlayer() == 1 && playerVsPlayer)
                spriteBatch.Draw(playerO, new Vector2(0f, 479f), null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Blue, SpriteEffects.None, 0f);

            else if (board.getPlayer() == 2 && playerVsPlayer)
                spriteBatch.Draw(playerX, new Vector2(599f, 479f), null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Red, SpriteEffects.None, 0f);

            if (board.getPlayer() == 1 && playerVsPlayer == false && machineIsFirst)
                spriteBatch.Draw(cpuO, new Vector2(0f, 479f), null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Blue, SpriteEffects.None, 0f);

            else if (board.getPlayer() == 1 && playerVsPlayer == false && machineIsFirst == false)
                spriteBatch.Draw(playerO, new Vector2(0f, 479f), null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Red, SpriteEffects.None, 0f);

            else if (board.getPlayer() == 2 && playerVsPlayer == false && machineIsFirst == false)
                spriteBatch.Draw(cpuX, new Vector2(599f, 479f), null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Blue, SpriteEffects.None, 0f);

            else if (board.getPlayer() == 2 && playerVsPlayer == false && machineIsFirst)
                spriteBatch.Draw(playerX, new Vector2(599f, 479f), null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Red, SpriteEffects.None, 0f);

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Vector2 cellPos = new Vector2(x * 64 + getScreenSize(true, 64, 3),
                                                  y * 64 + getScreenSize(false, 64, 3));

                    spriteBatch.Draw(cellEmpty, cellPos);

                    if (board.cell[x, y] == 1)
                        spriteBatch.Draw(cellO, cellPos, null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Blue, SpriteEffects.None, 0f);
                    else if (board.cell[x, y] == 2)
                        spriteBatch.Draw(cellX, cellPos, null, null, new Vector2(0f, 0f), 0f, new Vector2(1f, 1f), Color.Red, SpriteEffects.None, 0f);
                }
            }
        }
    }
}
