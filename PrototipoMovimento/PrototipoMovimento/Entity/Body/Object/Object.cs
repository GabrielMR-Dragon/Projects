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
    public class Object : Body
    {
		public float health = 1000f;
		
		public Vector2 dir = Vector2.Zero;
		
		public Character myShooter = null;

        public float lifeTime = 30f;
		
		public Object(Character shooter, Vector2 initPos, Vector2 initDir) : base(initPos)
		{
			myShooter = shooter;
			dir = initDir;
			speed *= 2;
			size /= 4;
		}
		
		public override Vector2 GetDir()
		{
			return dir;
		}
		
		public override Texture2D GetSprite()
		{
			return World.objectTexture;
		}

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            base.Update(gameTime);

            lifeTime -= deltaTime;

            if (lifeTime <= 0 || health <= 0)
            {
                World.entities.Remove(this);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            if (World.debugMode)
            {
                World.spriteBatch.DrawString(World.fontNormal, "Health: " + health + " | Expire time: " + lifeTime, new Vector2(this.pos.X, this.pos.Y+50f), Color.White);
            }

            base.Draw(gameTime);
        }
		
		public override void CollisionDetected(Entity other)
		{
			if (other is Animal)
			{
				Animal a = (Animal)other;
                this.health -= a.damage;
			}
		}
		
		public override bool IgnoreCollision(Entity other)
		{
			if (other == myShooter) //Ignorar colisão com meu atirador!
				return true;
				
			if (other is Object) //Ignorar colisão contra outros objetos!
				return true;
				
			return false;
		}
    }
}
