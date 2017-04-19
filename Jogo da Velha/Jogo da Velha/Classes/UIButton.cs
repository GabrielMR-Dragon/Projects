using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tic_Tac_Toe.Classes
{
    public class UIButton
    {
        Vector2 position;
        Vector2 size;
        Texture2D background;
        string text;
        SpriteFont font;

        public UIButton(Vector2 position, Vector2 size, Texture2D background, string text, SpriteFont font)
        {
            this.position = position;
            this.size = size;
            this.background = background;
            this.text = text;
            this.font = font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, position, null, Color.White, 0.0f, Vector2.Zero, new Vector2(size.X / background.Width, size.Y / background.Height), SpriteEffects.None, 0.0f);

            Vector2 textSize = font.MeasureString(text);

            spriteBatch.DrawString
                (font,                  //Fonte
                text,                   //String
                position + size * 0.5f, //Posição
                Color.White,            //Cor
                0.0f,                   //Rotação
                textSize * 0.5f,        //Origem (Pivô)
                Vector2.One,            //Escala
                SpriteEffects.None,     //Efeitos
                0f);                    //Profundidade da camada
        }

        public bool testClick(Vector2 testPosition)
        {
            if ((testPosition.X > position.X) && (testPosition.X < position.X + size.X) &&
               (testPosition.Y > position.Y) && (testPosition.Y < position.Y + size.Y))
                return true;
            else
                return false;
        }
    }
}
