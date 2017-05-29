using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace DesafioIA
{
    public class Player : Character
    {
        public float visionRadius = 128f;
		public Player(Vector2 initPos) : base(initPos) { }
		
		public override Vector2 GetDir()
		{
			Vector2 dir = Vector2.Zero;
			
			if (Keyboard.GetState().IsKeyDown(Keys.Right))
				dir.X += 1.0f;
			
			if (Keyboard.GetState().IsKeyDown(Keys.Left))
				dir.X += -1.0f;
			
			if (Keyboard.GetState().IsKeyDown(Keys.Up))
				dir.Y += -1.0f;
			
			if (Keyboard.GetState().IsKeyDown(Keys.Down))
				dir.Y += 1.0f;
			
			return dir;
		}
		
		public override Texture2D GetSprite()
		{
			return World.playerTexture;
		}
		
		public override bool WantsToFire()
		{
			return Keyboard.GetState().IsKeyDown(Keys.LeftControl);
		}

        public override void Draw(GameTime gameTime)
        {
            if (World.debugMode)
            {
                World.spriteBatch.Draw
                (
                 World.debugArrowTexture,
                 pos,
                 null,
                 new Color(1.0f, 1.0f, 0.0f, 1.0f),
                 0.0f,
                 new Vector2(World.debugArrowTexture.Width,
                             World.debugArrowTexture.Height) / 2f, //Pivot
                 new Vector2(2 * visionRadius / World.debugArrowTexture.Width,
                             2 * visionRadius / World.debugArrowTexture.Height), //Scale
                 SpriteEffects.None,
                 1.0f
                );
            }

            base.Draw(gameTime);
        }
    }
}
