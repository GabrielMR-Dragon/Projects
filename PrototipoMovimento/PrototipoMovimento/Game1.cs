using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PrototipoMovimento
{
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Sprites
        Texture2D personagem;

        //Player speed and position
        float playerSpeed = 100.0f;
        Vector2 position = new Vector2(100, 100);

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);

            //Mudar o tamanho da tela
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
            //Pacote de sprites
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Carrega conteúdo
            personagem = Content.Load<Texture2D>("Sprites/Personagem");
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            movementPlayer((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            //Inicia desenhos
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.NonPremultiplied);

            //Desenhos
            spriteBatch.Draw(
                personagem,
                position,
                null,
                null,
                new Vector2(personagem.Width, personagem.Height) / 2.0f,
                0.0f,
                Vector2.One,
                null,
                SpriteEffects.None,
                0.0f);

            //Finaliza desenhos
            spriteBatch.End();

            base.Draw(gameTime);
        }

        void movementPlayer(float dt)
        {
            //Direction
            Vector2 direction = Vector2.Zero;

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Up))
                direction.Y += -10;

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Down))
                direction.Y += +10;

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Left))
                direction.X += -10;

            if (Keyboard.GetState(PlayerIndex.One).IsKeyDown(Keys.Right))
                direction.X += +10;

            float s = (float)System.Math.Sqrt(direction.X * direction.X + direction.Y * direction.Y); //Dir.Length()

            if (s > 0)
                direction = direction / s;

            position += direction * dt * playerSpeed;

            if (position.X <= 32)
                position.X = 32;

            if (position.Y <= 32)
                position.Y = 32;

            if (position.X >= 768)
                position.X = 768;

            if (position.Y >= 568)
                position.Y = 568;
        }
    }
}
