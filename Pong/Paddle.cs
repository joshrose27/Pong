using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pong
{
    public class Paddle : Sprite
    {
        private readonly PlayerTypes _playertype;

        public enum PlayerTypes
        {
            Human,
            Computer
        }

        public Paddle(Texture2D texture, Vector2 location, Rectangle gameBoundaries, PlayerTypes playertype) : base(texture, location, gameBoundaries)
        {
            _playertype = playertype;
        }

        public override void Update(GameTime gameTime, GameObjects gameObjects)
        {
#if ANDROID
            float PADDLESPEED = 6.5f;
#else 
            float PADDLESPEED = 6.5f;
#endif
            
            if (_playertype == PlayerTypes.Computer)
            {
                var random = new Random();
                var reactionThreshold = random.Next(30, 130);
                // Computer player movement
                if (gameObjects.Ball.Location.Y + gameObjects.Ball.Height < Location.Y + reactionThreshold)
                {
                    Velocity = new Vector2(0, -PADDLESPEED);
                }

                if (gameObjects.Ball.Location.Y > Location.Y + Height + reactionThreshold)
                {
                    Velocity = new Vector2(0, PADDLESPEED);
                }


            }

            if (_playertype == PlayerTypes.Human)
            {
                //Move paddle up
                if (Keyboard.GetState().IsKeyDown(Keys.Up) || gameObjects.TouchInput.Up)
                    Velocity = new Vector2(0, -PADDLESPEED);

                //move paddle down
                if (Keyboard.GetState().IsKeyDown(Keys.Down) || gameObjects.TouchInput.Down)
                    Velocity = new Vector2(0, PADDLESPEED);
            }

            base.Update(gameTime, gameObjects);
        }

        protected override void CheckBounds()
        {
            Location.Y = MathHelper.Clamp(Location.Y, 0, _gameBoundaries.Height - _texture.Height);
        }
    }    
}
