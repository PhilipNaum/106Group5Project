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

        private string currentAnim;
        private double frameTimer;

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
        // probably also move this to somewhere else later
        private KeyboardState prevKS;

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
            health = maxHealth;
            invincible = false;
            currentAnim = "idleBase";
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
            if (grounded && Game1.SingleKeyPress(Keys.Space))
            {
                velocity.Y -= jumpSpeed;
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
            if (Game1.SingleLeftClick())
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
                        if (horDir >= 0)
                        {
                            currentItem = new Collectible(new Vector2(this.Position.X + Size.X, this.Position.Y + Size.Y / 2),
                                new Vector2(50, 50), Type.Hand, 1, 10);
                        }
                        else if (horDir < 0)
                        {
                            currentItem = new Collectible(new Vector2(this.Position.X - Size.X, this.Position.Y + Size.Y / 2),
                                new Vector2(50, 50), Type.Hand, 1, 10);
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
                    //check if the sword actually needs to be moved
                    //home is used to calculate the rotation and final position of the sword
                    //it needs to be updated so the sword can be in the right position
                    if (currentItem.Home != this.Position)
                    {
                        if (currentItem.Home.X < this.Position.X)
                        {
                            float xDiff = this.Position.X - currentItem.Home.X;
                            currentItem.Position = new Vector2(
                                currentItem.Position.X + xDiff,
                                currentItem.Position.Y);
                        }

                        if (currentItem.Home.X > this.Position.X)
                        {
                            float xDiff = currentItem.Home.X - this.Position.X;
                            currentItem.Position = new Vector2(
                                currentItem.Position.X - xDiff,
                                currentItem.Position.Y);
                        }

                        //if the sword is above where it should be, subtract the y difference between home and position
                        if (currentItem.Home.Y > this.Position.Y)
                        {
                            float YDiff = currentItem.Home.Y - this.Position.Y;
                            currentItem.Position = new Vector2(
                                currentItem.Position.X,
                                currentItem.Position.Y - YDiff);
                        }

                        //if the sword is below where it should be, add the y difference between home and position
                        if (currentItem.Home.Y < this.Position.Y)
                        {
                            float YDiff = currentItem.Home.Y - this.Position.Y;
                            currentItem.Position = new Vector2(
                                currentItem.Position.X,
                                currentItem.Position.Y + YDiff);
                        }

                        //set the sword's home to the player's current position
                        currentItem.Home = this.Position;
                    }
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


            // Control animations
            AnimationController(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {

            if (direction == -1) base.Draw(sb, 1, Color.White, 0, SpriteEffects.FlipHorizontally, 1);
            else base.Draw(sb);

            if (currentItem != null && currentItem.CollectibleType == Type.Gear) currentItem.Draw(sb);
        }

        public void AnimationController(GameTime gt)
        {
            string thisAnim = "";

            frameTimer += gt.ElapsedGameTime.TotalMilliseconds;
            System.Diagnostics.Debug.WriteLine(currentAnim + $"\n{frameTimer}");
            // Select animation type
            if (currentAbility != Ability.None && Game1.SingleLeftClick())
                thisAnim = "use";
            else if (!currentAnim.Substring(0, 3).Equals("use") ||
                (currentAnim.Equals("useHand") && frameTimer >= (8.0 * (20.0 / (6.0 * 60.0)))) ||
                (currentAnim.Equals("useGear") && frameTimer >= (8.0 * (20.0 / (6.0 * 60.0)))) ||
                (currentAnim.Equals("useChime") && frameTimer >= (8.0 * (20.0 / (6.0 * 60.0)))) ||
                (currentAnim.Equals("useKey") && frameTimer >= (14.0 * (20.0 / (6.0 * 60.0)))) ||
                (currentAnim.Equals("useFace") && frameTimer >= (4.0 * (20.0 / (6.0 * 60.0)))))
            {
                if (grounded && Game1.SingleKeyPress(Keys.Space))
                    thisAnim = "jump";
                else if (!grounded && frameTimer - (2.0 * (20.0 / (12.0 * 60.0))) >= 0)
                    thisAnim = "air";
                else if (grounded && (Game1.KeyboardState.IsKeyDown(Keys.A) || Game1.KeyboardState.IsKeyDown(Keys.D)))
                    thisAnim = "run";
                else thisAnim = "idle";
            }
            else thisAnim = "use";

            // Set ability mode
            switch (currentAbility)
            {
                default:
                case Ability.None:
                    thisAnim += "Base";
                    break;
                case Ability.AOE:
                    thisAnim += "Chime";
                    break;
                case Ability.Dash:
                    thisAnim += "Face";
                    break;
                case Ability.Sword:
                    thisAnim += "Hand";
                    break;
                case Ability.Throw:
                    thisAnim += "Gear";
                    break;
                case Ability.Undo:
                    thisAnim += "Key";
                    break;
            }

            // Update current animation
            if (thisAnim != currentAnim)
            {
                SetAnimation(thisAnim);
                frameTimer = 0;
            }
            currentAnim = thisAnim;
            System.Diagnostics.Debug.WriteLine(currentAnim + "\n");
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
                            System.Diagnostics.Debug.WriteLine("test");
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
