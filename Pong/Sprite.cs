using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pong
{
    public abstract class Sprite
    {
        protected Texture2D _texture;
        public Vector2 Location;
        public Rectangle _gameBoundaries;
        public Vector2 Velocity { get; protected set; }

        public int Width {
            get
            {
                return _texture.Width;
            }
        }

        public int Height
        {
            get
            {
                return _texture.Height;
            }
        }

        public Rectangle BoundingBox  { 
            get
            {
                return new Rectangle((int)Location.X, (int)Location.Y, Width, Height);
            }
        }

        public Sprite(Texture2D texture, Vector2 location, Rectangle gameBoundaries)
        {
            _texture = texture;
            Location = location;
            _gameBoundaries = gameBoundaries;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Location, Color.White);
        }

        public virtual void Update(GameTime gameTime, GameObjects gameObjects)
        {
            Location += Velocity;
            CheckBounds();
        }

        protected abstract void CheckBounds();
    }
}
