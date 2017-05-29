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
    public class Entity
    {
		public virtual void Update(GameTime gameTime)
		{
		}
		
		public virtual void Draw(GameTime gameTime)
		{	
		}
		
		//Testa colisão contra um retângulo
		public virtual bool TestCollision(Vector2 testMin, Vector2 testMax)
		{
			return false;
		}
		
		//Testa se deve ignorar colisão contra outra entidade
		public virtual bool IgnoreCollision(Entity other)
		{
			return false;
		}
		
		//Chamado quando uma colisão ocorreu
		public virtual void CollisionDetected(Entity other)
		{
		}
    }
}
