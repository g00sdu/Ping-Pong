using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PingPong
{
    public class Button
    {
        public Button _startButton, _exitButton, _howToPlayButton;
        private Texture2D _texture;
        private Vector2 _position;
        private Rectangle _boundingBox;

        public Button(Texture2D texture, Vector2 position)
        {
            _texture = texture;
            _position = new Vector2(position.X - texture.Width / 2, position.Y - texture.Height / 2);
            _boundingBox = new Rectangle((int)_position.X, (int)_position.Y, texture.Width, texture.Height);
        }

        public bool IsClicked()
        {
            MouseState mouseState = Mouse.GetState();
            return _boundingBox.Contains(mouseState.X, mouseState.Y) && mouseState.LeftButton == ButtonState.Pressed;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
