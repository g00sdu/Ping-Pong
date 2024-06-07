using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace PingPong
{
    public class Paddle
    {
        public Rectangle rect;
        private float moveSpeed = 500f;
        private bool isSecondPlayer;
        private Color color;

        public Paddle(bool isSecondPlayer, Color color)
        {
            this.isSecondPlayer = isSecondPlayer;
            this.color = color;
            rect = new Rectangle((this.isSecondPlayer ? Globals.width - 40 : 0), 140, 40, 200);
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kstate = Keyboard.GetState();
            if ((this.isSecondPlayer ? kstate.IsKeyDown(Keys.Up) : kstate.IsKeyDown(Keys.W)) && rect.Y > 0)
            {
                rect.Y -= (int)(moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            if ((this.isSecondPlayer ? kstate.IsKeyDown(Keys.Down) : kstate.IsKeyDown(Keys.S)) && rect.Y < Globals.height - rect.Height)
            {
                rect.Y += (int)(moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        public void Draw()
        {
            Globals.spriteBatch.Draw(Globals.pixel, rect, color);
        }
    }
}