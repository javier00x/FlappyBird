using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FlappyBird.Entities
{
    public class Tuyaux
    {
        public Texture2D texture;
        public Vector2 position;

        public bool scored = false;

        public Tuyaux()
        {
            this.texture = Statics.CONTENT.Load<Texture2D>("Textures/tuyaux");
            this.position = new Vector2(420, Statics.RANDOM.Next(-200, 5));
        }

        public void Update()
        {
            this.position.X -= 2f;
        }

        public Rectangle TopBound { get { return new Rectangle((int)this.position.X, (int)this.position.Y, 55, 308); } }
        public Rectangle BottomBound { get { return new Rectangle((int)this.position.X, (int)this.position.Y + 460, 55, 340); } }



        public void Draw()
        {
            Statics.SPRITEBATCH.Draw(this.texture, this.position, Color.White);


            if (Statics.DEBUG)
            {
                //show debug top
                Statics.SPRITEBATCH.Draw(Statics.PIXEL, this.TopBound, new Color(1f, 0f, 0f, 0.3f));
                //show debug bottom
                Statics.SPRITEBATCH.Draw(Statics.PIXEL, this.BottomBound, new Color(1f, 0f, 0f, 0.3f));
            }
        }
    }
}