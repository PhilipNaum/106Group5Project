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
        
        private float maxHorizontalSpeed = 9;
        // it may be better to represent accelerations as time to max speed
        private float horizontalAcceleration = 45; 
        private float horizontalDeceleration = 45;

        // probably want to dash farther horizontally than vertically
        private float dashSpeedX = 14;
        private float dashSpeedY = 6;

        // enum so there can only be one ability active at a time
        private Ability currentAbility;
        private enum Ability
        {
            None,
            Dash
        }


        public Player(Texture2D tex) 
        { 
            position = new Vector2(0, 0);
            currentAbility = Ability.Dash;
            size = new Vector2(50, 50);
            texture = tex;
        }

        public void UpdateP(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            MouseState ms = Mouse.GetState();
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

            // this locks player to be under the max speed
            // and decelerates if the player holds nothing
            //if (horDir != 0)
            //{
            //    velocity.X = MathHelper.Clamp(velocity.X + horDir * horizontalAcceleration * dTime, 
            //        -maxHorizontalSpeed, maxHorizontalSpeed);
            //}
            //else if (velocity.X != 0)
            //{
            //    float currentSign = MathF.Sign(velocity.X);
            //    velocity.X -= horizontalDeceleration * currentSign * dTime;
            //    if (currentSign != MathF.Sign(velocity.X))
            //        velocity.X = 0;
            //}
            // only accelerate if under max speed
            if (horDir != 0 && MathF.Abs(velocity.X) < maxHorizontalSpeed)
            {
                velocity.X = velocity.X + horDir * horizontalAcceleration * dTime;
            }
            // 
            else if (velocity.X != 0 && horDir == 0)
            {
                float currentSign = MathF.Sign(velocity.X);
                velocity.X -= horizontalDeceleration * currentSign * dTime;
                if (currentSign != MathF.Sign(velocity.X))
                    velocity.X = 0;
            }
            // this enables the player to go above max speed temporarily, in cases like dashing
            if (MathF.Abs(velocity.X) > maxHorizontalSpeed)
            {
                float currentSign = MathF.Sign(velocity.X);
                velocity.X -= horizontalDeceleration * currentSign * dTime;
                if (currentSign != MathF.Sign(velocity.X))
                    velocity.X = 0;
            }

            // use ability
            if (ks.IsKeyDown(Keys.Space) && prevKS.IsKeyUp(Keys.Space))
            {
                switch (currentAbility)
                {
                    case Ability.None:
                        break;
                    case Ability.Dash:
                        // need to decide whether dash
                        // overwrites or adds to velocity
                        velocity = Dash(ms);
                        break;
                    default:
                        break;
                }
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
        /// Returns new velocity after dash.
        /// Just a possible implementation of a dash.
        /// </summary>
        /// <param name="mouseState"></param>
        /// <returns></returns>
        private Vector2 Dash(MouseState mouseState)
        {
            // need direction from player to mouse as a vector
            Vector2 direction = Vector2.Normalize(mouseState.Position.ToVector2()
                - (position + size / 2));

            //return direction * dashSpeed;
            return new Vector2(direction.X * dashSpeedX, direction.Y * dashSpeedY);
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
