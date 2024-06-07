using Microsoft.Xna.Framework;

namespace PingPong;
public class Ball
{
    Rectangle rect;
    int xDir = 1, yDir = 1, moveSpeed = 250;

    public Ball()
    {
        rect = new Rectangle(Globals.width / 2 - 20, Globals.height / 2 - 20, 40, 40);
    }
    public void Update(GameTime gameTime, Paddle player1, Paddle player2)
    {
        int deltaSpeed = (int)(moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds);
        rect.X += xDir * deltaSpeed;
        rect.Y += yDir * deltaSpeed;

        if (player1.rect.Right > rect.Left && rect.Top > player1.rect.Top && rect.Bottom < player1.rect.Bottom)
        {
            xDir = 1;
            moveSpeed += 50;
        }

        if (player2.rect.Left < rect.Right && rect.Top > player2.rect.Top && rect.Bottom < player2.rect.Bottom)
        {
            xDir = -1;
            moveSpeed += 50;
        }

        if (rect.Y < 0)
        {
            yDir *= -1;
        }

        if (rect.Y > Globals.height - rect.Height)
        {
            yDir *= -1;
        }

        if (rect.X < 0)
        {
            Globals.scorePlayer2 += 1;
            moveSpeed = 250;
            resetGame();
        }

        if (rect.X > Globals.width - rect.Width)
        {
            Globals.scorePlayer1 += 1;
            moveSpeed = 250;
            resetGame();
        }
    }
    public void Draw()
    {
        Globals.spriteBatch.Draw(Globals.pixel, rect, Color.White);
    }
    public void resetGame()
    {
        rect.X = Globals.width / 2 - 20;
        rect.Y = Globals.height / 2 - 20;
        xDir *= -1;
    }

}
