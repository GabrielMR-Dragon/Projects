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
    public class Body : Entity
    {
		public Vector2 pos  = new Vector2(320, 240);
		public Vector2 size = new Vector2(64, 64);
		public float speed  = 100f;
		
		public Body(Vector2 initPos)
		{
			pos = initPos;
		}
		
		public override void Update(GameTime gameTime)
		{
			float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
			
			Vector2 dir = GetDir();
			
			float s = dir.Length();
			
			if(s > 0)
				dir = dir / s;

            if (pos.X <= 32)
                pos.X = 32;

            if (pos.Y <= 32)
                pos.Y = 32;

            if (pos.X >= 768)
                pos.X = 768;

            if (pos.Y >= 568)
                pos.Y = 568;
			
			//simula físicas independentes em dois eixos
			for(int axis = 0; axis < 2; axis++)
			{
				Vector2 backupPos = pos; //Salvar posição atual

                if (axis == 0)
                    pos += new Vector2(dir.X, 0f) * deltaTime * speed; //only X
                else
                    pos += new Vector2(0f, dir.Y) * deltaTime * speed; //only Y
				
				Entity collider = null;
				
				Vector2 myMin = GetMin();
				Vector2 myMax = GetMax();
				
				//Testa colisão contra todas as outras entidades do mundo
				foreach (Entity e in World.entities)
				{
					if ((e != this) && //Não é eu mesmo?
						(IgnoreCollision(e) == false) && //Ignorar colisão com outro?
						(e.IgnoreCollision(this) == false) && //Outro ignora colisão comigo?
						e.TestCollision(myMin, myMax)) //Está colidindo contra outra entidade?
						{
							collider = e; //Colisão detectada!
							CollisionDetected(e);
							break;
						}
						
					if(collider != null) //Desfaz movimento!
						pos = backupPos;
				}
			}
		}
		
		public override void Draw(GameTime gameTime)
		{
			Texture2D sprite = GetSprite();
			
			if(sprite == null)
				return;
			
			World.spriteBatch.Draw
				(
				 sprite,
				 pos,
				 null,
				 Color.White,
				 0.0f,
				 new Vector2(sprite.Width,
							 sprite.Height) / 2f, //pivot
				 new Vector2(size.X / sprite.Width,
							 size.Y / sprite.Height), //scale
				 SpriteEffects.None,
				 0.0f
				);
		}
		
		public virtual Texture2D GetSprite()
		{
			return null;
		}
		
		public virtual Vector2 GetDir()
		{
			return Vector2.Zero;
		}
		
		public float GetRadius()
		{
			return Math.Max(size.X, size.Y) / 2f;
		}
		
		public Vector2 GetMin() { return pos - size / 2; }
		
		public Vector2 GetMax() { return pos + size / 2; }
		
		public bool TestPoint(Vector2 testPos)
		{
			return (testPos.X > (pos.X - size.X / 2f)) &&
			       (testPos.Y > (pos.Y - size.Y / 2f)) &&
			       (testPos.X < (pos.X + size.X / 2f)) &&
			       (testPos.Y < (pos.Y + size.Y / 2f));
		}
		
		public override bool TestCollision(Vector2 testMin, Vector2 testMax)
		{
			Vector2 myMin = GetMin();
			Vector2 myMax = GetMax();
			
			//Testa colisão entre meu retângulo e outro retângulo
			if ((testMax.X >= myMin.X) && (testMax.Y >= myMin.Y) &&
				(testMin.X <= myMax.X) && (testMin.Y <= myMax.Y))
				return true;
			else
				return false;
		}
    }
}
