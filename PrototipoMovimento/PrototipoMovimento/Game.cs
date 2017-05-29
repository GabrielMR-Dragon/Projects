using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace DesafioIA
{
    public class World : Game
    {
        GraphicsDeviceManager graphics;

        //Lote de sprites e fonte
        public static SpriteFont fontNormal;
        public static SpriteBatch spriteBatch;

        //Sprites
        public static Texture2D playerTexture;
        public static Texture2D animalTexture;
        public static Texture2D objectTexture;
        public static Texture2D debugArrowTexture;
        public static Texture2D debugCircleTexture;

        //Keyboard state
        public static KeyboardState prevKeyState = Keyboard.GetState();

        //Debug mode
        public static bool debugMode = false;

        //World.entities
        public static List<Entity> entities = new List<Entity>();

        public World()
        {
            graphics = new GraphicsDeviceManager(this);

            //Mudar o tamanho da tela
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;

            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            //Pacote de sprites
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Carrega fonte
            fontNormal = Content.Load<SpriteFont>("Fonts/Normal");

            //Carrega conteúdo
            playerTexture = Content.Load<Texture2D>("Sprites/Personagem");
            animalTexture = Content.Load<Texture2D>("Sprites/Animal");
            objectTexture = Content.Load<Texture2D>("Sprites/mail");
            debugArrowTexture = Content.Load<Texture2D>("Sprites/debug_arrow");
            debugCircleTexture = Content.Load<Texture2D>("Sprites/debug_circle");

            //Carrega posições dos personagens
            entities.Add(new Player(new Vector2(400, 300)));

            entities.Add(new Animal(new Vector2(32, 32)));
            entities.Add(new Animal(new Vector2(32, 568)));
            entities.Add(new Animal(new Vector2(768, 32)));
            entities.Add(new Animal(new Vector2(768, 568)));
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            List<Entity> tmp = new List<Entity>(entities);
            foreach (Entity e in tmp)
                e.Update(gameTime);

            if (Keyboard.GetState().IsKeyDown(Keys.F1) && !prevKeyState.IsKeyDown(Keys.F1))
                debugMode = !debugMode;

            prevKeyState = Keyboard.GetState();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //Inicia desenhos
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);


            //Efetua desenhos
            foreach (Entity e in entities)
                e.Draw(gameTime);

            //Escreve texto
            spriteBatch.DrawString(
              fontNormal,
              "F1 - Debug Mode (" + (debugMode ? "DEBUG ON" : "DEBUG OFF") + ")\nControl - Shoot!",
              new Vector2(10, 10),  //position
              Color.White,          //color
              0.0f,                 //rotation
              Vector2.Zero,         //origin (pivot)
              Vector2.One,          //scale
              SpriteEffects.None,
              0.0f
            );

            //Finaliza desenhos
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
