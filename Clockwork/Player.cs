﻿/*
 * Who has worked on this file:
 * Leo
 * Philip
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Clockwork
{
    internal class Player : GameObject
    {
        //the collectible that represents the thrown gear
        private Collectible gearThrow;

        //enemies that weapons must check collisions for
        private List<Enemy> enemies;

        private Vector2 velocity;
        public Vector2 Velocity { 
            get { return velocity; } 
            set { velocity = value; } 
        }

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

        // if the player is on the ground
        // used for checking if the player should be able to jump
        private bool grounded;
        public bool Grounded { 
            get { return grounded; } 
            set { grounded = value; } 
        }

        // enum so there can only be one ability active at a time
        private Ability currentAbility;
        private enum Ability
        {
            None,
            Dash,
            Throw
        }


        public Player(Vector2 position, Vector2 size, List<Enemy> enemies) : base(position, size, Sprites.Player)
        {
            currentAbility = Ability.None;
            this.enemies = enemies;
            grounded = false;
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
            if (grounded && ks.IsKeyDown(Keys.W) && prevKS.IsKeyUp(Keys.W))
            {
                velocity.Y -= jumpSpeed;
            }

            float horDir = 0;
            if (ks.IsKeyDown(Keys.A))
                horDir--;
            if (ks.IsKeyDown(Keys.D))
                horDir++;

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
                    case Ability.Throw:
                        //these statements make sure that a gear can only be thrown once the one before is gone
                        
                        if (gearThrow == null)
                        {
                            gearThrow = new Collectible(this.Position,new Vector2(50,50) ,Type.Gear, 1, 2);
                            gearThrow.Velocity = Vector2.Normalize(ms.Position.ToVector2()
                            - (this.Position + this.Size / 2));
                        }
                        else if (gearThrow.Mode == 2)
                        {
                            gearThrow = new Collectible(this.Position, new Vector2(50, 50), Type.Gear, 1, 2);
                            gearThrow.Velocity = Vector2.Normalize(ms.Position.ToVector2()
                            - (this.Position + this.Size / 2));
                        }
                        break;
                    default:
                        break;
                }
            }

            //putting this here makes sure it updates every frame
            //same reason why the object itself is a field
            if (gearThrow != null)
            {
                gearThrow.Update(gameTime);
                for (int i = 0; i < enemies.Count; i++)
                {
                    gearThrow.CollisionResponse(enemies[i]);
                }
            }
            this.Position += velocity;

            if (this.Position.Y + this.Size.Y > minHeight)
            {
                this.Position = new Vector2(Position.X, minHeight - this.Size.Y);
                velocity.Y = 0;
            }

            prevKS = ks;

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);

            if (gearThrow != null)
            {
                gearThrow.Draw(sb);
            }
        }

        public void CollisionResponse(GameObject other)
        {
            if (IsColliding(other))
            {
                if (other is Collectible)
                {
                    Collectible item = (Collectible)other;
                    //changes the player's ability based on the item's type
                    switch (item.CollectibleType)
                    {
                        case (Type.Gear):
                            currentAbility = Ability.Throw;
                            break;
                        case (Type.Face):
                            currentAbility = Ability.Dash;
                            break;
                    }
                }
            }
        }
    }
}
