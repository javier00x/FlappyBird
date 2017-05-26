using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FlappyBird.Entities
{
    public class Bird
    {
        public Texture2D[] Textures;
        public float Rotation;
        public float YSpeed;
        public int texturePosition;
        public Vector2 Position;

        public int jumpTimer = 500;
        public double jumpElapsed = 0;

        public int animTimer = 100;
        public double animElapsed = 0;
        public int textureAdd = 1;

        public bool canJump = true;

        public bool dead = false;

        public Bird()
        {
            Textures = new Texture2D[3];
            this.Textures[0] = Statics.CONTENT.Load<Texture2D>("Textures/bird1");
            this.Textures[1] = Statics.CONTENT.Load<Texture2D>("Textures/bird2");
            this.Textures[2] = Statics.CONTENT.Load<Texture2D>("Textures/bird3");
            YSpeed = 0;
            this.Position = new Vector2(150, 300);
        }

        public void Update()
        {
            YSpeed += 0.4f;

            jumpElapsed += Statics.GAMETIME.ElapsedGameTime.TotalMilliseconds;
            if (jumpElapsed > jumpTimer)
            {
                canJump = true;
                jumpElapsed = 0;
            }

            animElapsed += Statics.GAMETIME.ElapsedGameTime.TotalMilliseconds;
            if (animElapsed > animTimer)
            {
                this.texturePosition += this.textureAdd;
                if (this.texturePosition == 2 || this.texturePosition == 0)
                    this.textureAdd = this.textureAdd * -1;
                animElapsed = 0;
            }

            if (Statics.INPUT.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.Space) && canJump)
            {
                YSpeed = -8;
            }

            Rotation = (float)Math.Atan2(YSpeed, 7);


            this.Position.Y += YSpeed;

            if (this.Position.Y > 500)
            {
                dead = true;
            }

        }

        public Rectangle Bound { get { return new Rectangle((int)this.Position.X - 20, (int)this.Position.Y - 20, 40, 40); } }

        public void Draw()
        {
            Statics.SPRITEBATCH.Draw(this.Textures[this.texturePosition], this.Position, null, Color.White, this.Rotation, new Vector2(20, 20), 1f, SpriteEffects.None, 0f);

            //show debug
            if (Statics.DEBUG)
                Statics.SPRITEBATCH.Draw(Statics.PIXEL, this.Bound, new Color(1f, 0f, 0f, 0.3f));
        }

    }
}