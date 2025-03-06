using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Clockwork
{
    internal class Player : GameObject
    {
        private Vector2 velocity;

        // Probably want to put gravity somewhere else, but here now for testing
        private readonly Vector2 gravity = new Vector2(0, 20f);
        // probably also move this to somewhere else later
        private KeyboardState prevKS;

        // player can't move below this height for now
        private float minHeight = 475;
        private float jumpSpeed = 9;
        
        private float moveSpeed = 4;
        private float maxHorizontalSpeed = 9;
        // it may be better to represent accelerations as time to max speed
        private float horizontalAcceleration = 45; 
        private float horizontalDeceleration = 45; 


        public Player(Texture2D tex) 
        { 
            position = new Vector2(0, 0);

            size = new Vector2(50, 50);
            texture = tex;
        }

        public void UpdateP(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            // time between frames
            float dTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            velocity.Y += gravity.Y * dTime;

            // jump
            if (ks.IsKeyDown(Keys.W) && prevKS.IsKeyUp(Keys.W))
            {
                velocity.Y -= jumpSpeed;
            }

            float horDir = 0;
            if (ks.IsKeyDown(Keys.A))
                horDir--;
            if (ks.IsKeyDown(Keys.D))
                horDir++;

            if (horDir != 0)
            {
                velocity.X = MathHelper.Clamp(velocity.X + horDir * horizontalAcceleration * dTime, 
                    -maxHorizontalSpeed, maxHorizontalSpeed);
            }
            else if (velocity.X != 0)
            {
                float currentSign = MathF.Sign(velocity.X);
                velocity.X -= horizontalDeceleration * currentSign * dTime;
                if (currentSign != MathF.Sign(velocity.X))
                    velocity.X = 0;
            }

            position += velocity;

            if (position.Y + size.Y > minHeight)
            {
                position.Y = minHeight - size.Y;
                velocity.Y = 0;
            }


            prevKS = ks;
        }


        /// <summary>
        /// This should be what is UpdateP, but I don't want to change the GameObject
        /// Update() right now.
        /// </summary>
        public override void Update()
        {
            base.Update();
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, GetRectangle(), Color.White);
        }

        /// <summary>
        /// Gets a rectangle for the object.
        /// This should probably be in GameObject.
        /// </summary>
        /// <returns></returns>
        private Rectangle GetRectangle()
        {
            return new Rectangle(position.ToPoint(), size.ToPoint());
        }
    }
}
