using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace FlappyBird.Screens
{
    public class GameScreen : Screen
    {
        public Texture2D background;
        public Texture2D sand;
        public Texture2D gameover;
        public Entities.Bird Bird;
        public Entities.Scroll Scroll;
        public SpriteFont Font;

        public int score = 0;

        public List<Entities.Tuyaux> Tuyaux;
        public int tuyauxTimer = 2000;
        public double tuyauxElapsed = 0;



        public GameScreen()
        {

        }



        public override void LoadContent()
        {
            background = Statics.CONTENT.Load<Texture2D>("Textures/background");
            sand = Statics.CONTENT.Load<Texture2D>("Textures/sand");
            gameover = Statics.CONTENT.Load<Texture2D>("Textures/gameover");

            Font = Statics.CONTENT.Load<SpriteFont>("Fonts/font");


            Reset();
            base.LoadContent();
        }

        public void Reset()
        {
            Bird = new Entities.Bird();
            Scroll = new Entities.Scroll();

            Tuyaux = new List<Entities.Tuyaux>();
            Tuyaux.Add(new Entities.Tuyaux());
            score = 0;
            tuyauxElapsed = 0;
        }

        public override void Update()
        {
            tuyauxCreator();
            if (!Bird.dead)
            {
                for (int i = Tuyaux.Count - 1; i > -1; i--)
                {
                    if (Tuyaux[i].position.X < -50)
                        Tuyaux.RemoveAt(i);
                    else
                    {
                        Tuyaux[i].Update();
                        if (!Tuyaux[i].scored && Bird.Position.X > Tuyaux[i].position.X + 50)
                        {
                            Tuyaux[i].scored = true;
                            score++;
                        }

                        if (Bird.Bound.Intersects(Tuyaux[i].TopBound) || Bird.Bound.Intersects(Tuyaux[i].BottomBound))
                        {
                            Bird.dead = true;
                        }
                    }

                }

                Bird.Update();
                Scroll.Update();
            }


            if (Bird.dead && Statics.INPUT.isKeyPressed(Microsoft.Xna.Framework.Input.Keys.R))
            {
                this.Reset();
            }

            base.Update();
        }

        public void tuyauxCreator()
        {
            tuyauxElapsed += Statics.GAMETIME.ElapsedGameTime.TotalMilliseconds;
            if (tuyauxElapsed > tuyauxTimer)
            {
                Tuyaux.Add(new Entities.Tuyaux());
                tuyauxElapsed = 0;
            }

        }



        public override void Draw()
        {
            Statics.SPRITEBATCH.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.LinearWrap, null, null);

            Statics.SPRITEBATCH.Draw(this.background, Vector2.Zero, Color.White);
            foreach (var item in Tuyaux)
            {
                item.Draw();
            }

            Statics.SPRITEBATCH.Draw(this.sand, new Vector2(0, 529), Color.White);

            Scroll.Draw();


            Bird.Draw();

            Statics.SPRITEBATCH.DrawString(this.Font, "Score : " + this.score.ToString(), new Vector2(10, 10), Color.Red);

            if (Bird.dead)
            {

                Statics.SPRITEBATCH.Draw(Statics.PIXEL, new Rectangle(0, 0, Statics.GAME_WIDTH, Statics.GAME_HEIGHT), new Color(1f, 0f, 0f, 0.3f));
                Statics.SPRITEBATCH.Draw(this.gameover, new Vector2(0, 80), Color.White);
            }


            Statics.SPRITEBATCH.End();
            base.Draw();
        }
    }
}