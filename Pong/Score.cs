using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Pong
{
    public class Score
    {
        private readonly SpriteFont _font;
        private readonly Rectangle _gameBoundaries;

        public int PlayerScore { get; set; }
        public int ComputerScore { get; set; }

        public Score(SpriteFont font, Rectangle gameBoundaries)
        {
            _font = font;
            _gameBoundaries = gameBoundaries;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var scoreText = string.Format("{0}:{1}", PlayerScore, ComputerScore);
            var xPosition = (_gameBoundaries.Width/2) - (_font.MeasureString(scoreText).X/2);
            var position = new Vector2(xPosition, _gameBoundaries.Height - 100);

            spriteBatch.DrawString(_font, scoreText, position, Color.Black);
        }

        public void Update(GameTime gameTime, GameObjects gameObjects)
        {
            if (gameObjects.Ball.Location.X + gameObjects.Ball.Width < 0)
            {
                ComputerScore++;
                gameObjects.Ball.AttachTo(gameObjects.PlayerPaddle);
            }

            if (gameObjects.Ball.Location.X > _gameBoundaries.Width)
            {
                PlayerScore++;
                gameObjects.Ball.AttachTo(gameObjects.PlayerPaddle);
            }
        }
    }
}