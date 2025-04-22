/*
 * Who has worked on this file:
 * Leo
 * Philip
 */
using Microsoft.Win32.SafeHandles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct2D1.Effects;
using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.Net.Http;
using System.Runtime.CompilerServices;

namespace Clockwork
{
    internal class Player : GameObject
    {
        //the collectible that represents the players current item;
        private Collectible currentItem;

        private bool invincible;

        private double timer = 2;

        private readonly int maxHealth = 10;
        private int health;

        private int direction;

        public Collectible CurrentItem
        {
            get { return currentItem; }
        }

        private Vector2 velocity;
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        // Probably want to put gravity somewhere else, but here now for testing
        private readonly Vector2 gravity = new Vector2(0, 20f);

        private KeyboardState prevKS;
        private MouseState prevMS;

        private float jumpSpeed = 9;

        private float maxHorizontalSpeed = 9;
        // it may be better to represent accelerations as time to max speed
        private float horizontalAcceleration = 45;
        private float horizontalDeceleration = 45;

        // probably want to dash farther horizontally than vertically
        private float dashSpeedX = 14;
        private float dashSpeedY = 6;

        // if the player is on the ground
        // used for checking if the player should be able to jump
        private bool grounded;
        public bool Grounded
        {
            get { return grounded; }
            set { grounded = value; }
        }

        private bool hasDash;
        public bool HasDash
        {
            get { return hasDash; }
            set { hasDash = value; }
        }

        // enum so there can only be one ability active at a time
        private Ability currentAbility;
        private enum Ability
        {
            None,
            Dash,
            Throw,
            Sword,
            AOE,
            Undo
        }

        public Player(Vector2 position, Vector2 size) : base(position, size, Sprites.Player)
        {
            currentAbility = Ability.None;
            grounded = false;
            hasDash = false;
            health = maxHealth;
            invincible = false;
        }

        /// <summary>
        /// Resets the players position, velocity, and ability.
        /// For starting/restarting levels.
        /// </summary>
        public void ResetPlayer()
        {
            Position = LevelManager.Instance.CurrentLevel.StartPosition - new Vector2(0, 32);

            velocity = Vector2.Zero;
            currentAbility = Ability.None;
            health = maxHealth;
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
                - (this.Position + this.Size / 2));

            hasDash = false;
            //return direction * dashSpeed;
            return new Vector2(direction.X * dashSpeedX, direction.Y * dashSpeedY);
        }


        /// <summary>
        /// Runs every frame. Does player movement and abilities.
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            MouseState ms = Mouse.GetState();
            // time between frames
            float dTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            velocity.Y += gravity.Y * dTime;

            // jump
            if (grounded && ks.IsKeyDown(Keys.Space) && prevKS.IsKeyUp(Keys.Space))
            {
                velocity.Y -= jumpSpeed;
                grounded = false;
            }

            float horDir = 0;
            if (ks.IsKeyDown(Keys.A))
            {
                horDir--;
                direction = -1;
            }
            if (ks.IsKeyDown(Keys.D))
            {
                horDir++;
                direction = 1;
            }

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
            if (ms.LeftButton == ButtonState.Pressed && prevMS.LeftButton == ButtonState.Released)
            {
                switch (currentAbility)
                {
                    case Ability.None:
                        break;
                    case Ability.Dash:
                        // need to decide whether dash
                        // overwrites or adds to velocity
                        if (hasDash)
                            velocity = Dash(ms);
                        break;
                    case Ability.Throw:
                        //if statement makes sure that a gear can only be thrown once the one before is gone
                        if (currentItem == null || currentItem.Mode == 2)
                        {
                            currentItem = new Collectible(new Vector2(this.Position.X + Size.X / 4, this.Position.Y + Size.Y / 4), new Vector2(32, 32), Type.Gear, 1, 2);
                            currentItem.Sprite.SetAnimation("gearSpin");
                            currentItem.Velocity = Vector2.Normalize(ms.Position.ToVector2()
                            - (this.Position + this.Size / 2));
                        }
                        break;
                    case Ability.Sword:
                        //create a new sword
                        if (direction == 1)
                        {
                            currentItem = new Collectible(new Vector2(this.Position.X + Size.X, this.Position.Y - Size.Y / 4),
                                new Vector2(80, 80), Type.Hand, 1, 10);
                        }
                        else if (direction == -1)
                        {
                            currentItem = new Collectible(new Vector2(this.Position.X - Size.X * 2, this.Position.Y - Size.Y / 4),
                                new Vector2(80, 80), Type.Hand, 1, 10);
                        }
                        currentItem.Home = this.Position;

                        break;
                    case Ability.AOE:
                        if (currentItem == null || currentItem.Mode == 2)
                        {
                            currentItem = new Collectible(
                            new Vector2(this.Position.X - 20, this.Position.Y - Size.Y / 4),
                            new Vector2(72, 96), Type.Chime, 1, 5);
                        }
                        break;
                    case Ability.Undo:
                        currentItem.Update(gameTime);
                        break;
                    default:
                        break;
                }
            }
            //putting this here makes sure it updates every frame
            //same reason why the object itself is a field
            if (currentItem != null && currentItem.CollectibleType != Type.Key)
            {
                currentItem.Update(gameTime);

                if (currentAbility == Ability.AOE)
                {
                    //set the position of the aoe 
                    currentItem.Position = new Vector2(this.Position.X - 20, this.Position.Y - Size.Y / 4);
                }
                //keep the sword with the player
                if (currentAbility == Ability.Sword)
                {
                    if(direction == 1)
                    currentItem.Position = new Vector2(this.Position.X + Size.X, this.Position.Y - Size.Y/4);

                    if (direction == -1)
                        currentItem.Position = new Vector2(this.Position.X - Size.X * 2, this.Position.Y - Size.Y/4);
                }
            }

            if (invincible)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
                if (timer <= 0)
                {
                    timer = 1;
                    invincible = false;
                }
            }

            this.Position += velocity;

            prevKS = ks;
            prevMS = ms;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            SetPlayerAnimation();
            if (direction == -1) base.Draw(sb, 1, Color.White, 0, SpriteEffects.FlipHorizontally, 1);
            else base.Draw(sb);

            if (currentItem != null && currentItem.CollectibleType != Type.Key) currentItem.Draw(sb);
        }

        public void SetPlayerAnimation()
        {
            string animName = "";
            switch (currentAbility)
            {
                case Ability.AOE:
                    animName = "Chime";
                    break;
                case Ability.Dash:
                    animName = "Face";
                    break;
                case Ability.Sword:
                    animName = "Hand";
                    break;
                case Ability.Undo:
                    animName = "Key";
                    break;
                case Ability.Throw:
                    animName = "Gear";
                    break;
                case Ability.None:
                    animName = "Base";
                    break;
            }
            SetAnimation($"air{animName}");
        }

        public void CollisionResponse(GameObject other)
        {
            if (IsColliding(other))
            {
                if (other is Collectible)
                {
                    Collectible item = (Collectible)other;
                    //changes the player's ability based on the item's type
                    currentItem = null;
                    switch (item.CollectibleType)
                    {
                        case (Type.Gear):
                            currentAbility = Ability.Throw;
                            break;
                        case (Type.Face):
                            currentAbility = Ability.Dash;
                            break;
                        case (Type.Hand):
                            currentAbility = Ability.Sword;
                            break;
                        case (Type.Chime):
                            currentAbility = Ability.AOE;
                            break;
                        case (Type.Key):
                            currentAbility = Ability.Undo;
                            currentItem = new Collectible(this.Position, this.Size, Type.Key, 1);
                            currentItem.KeyTurn += GameObject.ReverseTime;
                            break;
                    }
                }
                if (other is Enemy)
                {
                    Enemy otherEnemy = (Enemy)other;
                    if (!invincible)
                    {
                        //get the intersection rectangle of the enemy and the player
                        //get the intersection rectangle of the enemy and the player
                        Rectangle displacement = Rectangle.Intersect(this.GetRectangle(), otherEnemy.GetRectangle());

                        //vertical interactions
                        if (displacement.Height <= displacement.Width)
                        {
                            //enemy hits you from the bottom
                            if (this.Position.Y < otherEnemy.Position.Y)
                            {
                                this.Position = new Vector2(this.Position.X, this.Position.Y - displacement.Height);
                                velocity.Y = 0;
                                velocity.Y -= 5;
                                if (this.Position.X >= otherEnemy.Position.X + otherEnemy.Size.X / 2)
                                {
                                    velocity.X = 0;
                                    velocity.X += 10;
                                }
                                else
                                {
                                    velocity.X = 0;
                                    velocity.X -= 10;
                                }
                            }

                            //enemy hits you from the top
                            //might not need but will be good to have
                            if (this.Position.Y > otherEnemy.Position.Y)
                            {
                                this.Position = new Vector2(this.Position.X, this.Position.Y - displacement.Height);
                            }
                        }

                        //horizantal interactions
                        if (displacement.Height > displacement.Width)
                        {
                            //if the enemy hits you from the left
                            if (this.Position.X < otherEnemy.Position.X)
                            {
                                this.Position = new Vector2(this.Position.X - displacement.Width, this.Position.Y);
                                //set velocity to 0 to get rid of any movment
                                velocity.X = 0;
                                velocity.X -= 10;
                            }
                            //if the enemy hits you from the right
                            else if (this.Position.X > otherEnemy.Position.X)
                            {
                                this.Position = new Vector2(this.Position.X + displacement.Width, this.Position.Y);
                                //set velocity to 0 to get rid of any movment
                                velocity.X = 0;
                                velocity.X += 10;
                            }
                        }

                        TakeDamage(otherEnemy.Damage);
                    }
                }
            }
        }

        private void TakeDamage(int damage)
        {
            if (!invincible)
            {
                health -= damage;
                invincible = true;
            }

            // player death
            if (health <= 0)
            {
                // this also resets health to max
                ResetPlayer();
                invincible = true;

                LevelManager.Instance.ReloadLevel();
            }
        }
    }
}
