﻿
/*
 * Who has worked on this file:
 * Philip
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D11;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Printing;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Windows.Forms;

namespace Clockwork
{
    internal class Enemy : GameObject
    {

        //the current health of the enemy
        private int health;

        //the max health of the eneym
        private int maxHealth;

        //used for movement. Currently, the home is set to always be the enemies starting position
        private Vector2 home;

        //velocity of the enemy
        private Vector2 velocity;

        //used for gravity, but name might change if other enemies need/use other types of acceleration
        private Vector2 acceleration;

        //the total units that make up the space the enemy can move in
        private int range;

        //represents if the enemy is dead
        private bool isDead;

        //represents if the enemy is invincible or not
        private bool invincible;

        //a timer used for the enemy's i-frames and when it died
        private double deathTimer;

        //the list of tiles that the enemy has to check for collisions
        private List<Tile> isColliding;

        //the amount of damage the enemy does to the player
        private int damage = 4;

        public int Damage
        {
            get { return damage; }
        }

        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }

        /// <summary>
        /// creates a new enemy
        /// </summary>
        /// <param name="position">the enemy's position</param>
        /// <param name="size">the size (same as sprite)</param>
        /// <param name="velocity">the enemy's velocity</param>
        /// <param name="range">the total range the enemy can move</param>
        /// <param name="health">the total health of the enemy</param>
        public Enemy(Vector2 position, Vector2 size, Vector2 velocity, int range, int health) : base(position, size, Sprites.Enemy)
        {
            this.health = health;
            maxHealth = health;
            this.range = range;
            home = new Vector2(this.Position.X + this.Size.X / 2, this.Position.Y);
            this.velocity = velocity;
            acceleration = new Vector2(0, .5f);
            isDead = false;
            invincible = false;
            deathTimer = .4;
            isColliding = new List<Tile>();
            damage = 2;
        }

        /// <summary>
        /// draw method
        /// </summary>
        /// <param name="sb">the spritebatch to draw with</param>
        public override void Draw(SpriteBatch sb)
        {
            if (!isDead)
            {
                base.Draw(sb);
            }
        }

        /// <summary>
        /// Enemies move in their set range with their home as the center
        /// <param name="gt">GameTime variable</param>
        public override void Update(GameTime gt)
        {
            //only update if the enemy isn't dead
            if (!isDead)
            {
                //if the enemy reaches the bounds of it's range, then reverse it's direction
                if (velocity.X >= -.5 || velocity.X <= .5)
                {
                    if (this.Position.X >= home.X + range / 2 - Size.X || this.Position.X <= home.X - range / 2)
                    {
                        velocity.X *= -1;
                    }
                }
                //change position, velocity, and acceleration
                this.Position = new Vector2(Position.X + velocity.X, Position.Y);
                velocity += acceleration;
                this.Position += velocity;

                //starts a timer that if the enemy is supposed to be invincible
                if (invincible)
                {
                    deathTimer -= gt.ElapsedGameTime.TotalSeconds;
                    if (deathTimer <= 0)
                    {
                        invincible = false;
                        deathTimer = .4;
                    }
                }

                //resolve tile collisions
                ResolveTileCollisions();
                base.Update(gt);
            }
            else
            {
                deathTimer += gt.ElapsedGameTime.TotalSeconds;
            }
        }

        /// <summary>
        /// Responds to collision based on the type of GameObject it is colliding with
        /// </summary>
        /// <param name="other">the other game object that this enemy is colliding with</param>
        public void CollisionResponse(GameObject other)
        {
            if (IsColliding(other))
            {

                if (other is Enemy)
                {
                    //if two enemies run into eachother, then they should turn around
                    Enemy otherEnemy = (Enemy)other;
                    Rectangle displacement = Rectangle.Intersect(GetRectangle(), otherEnemy.GetRectangle());
                    if (displacement.Height > displacement.Width)
                    {
                        if (this.Position.X < otherEnemy.Position.X)
                        {
                            this.Position = new Vector2(Position.X - displacement.Width, Position.Y);
                        }
                        else if (this.Position.X > otherEnemy.Position.X)
                        {
                            this.Position = new Vector2(Position.X + displacement.Width, Position.Y);
                        }
                    }
                    velocity.X *= -1;
                }
                if (other is Collectible)
                {
                    //depending on the type of collectible, decrease health
                    Collectible item = (Collectible)other;
                    if (!invincible)
                    {
                        Rectangle difference = Rectangle.Intersect(this.GetRectangle(), item.GetRectangle());
                        if (difference.Width <= difference.Height)
                        {
                            if (item.Position.X > this.Position.X)
                            {
                                velocity.X -= 2;
                                velocity.Y -= 5;
                            }
                            else if (item.Position.X < this.Position.X)
                            {
                                velocity.X += 2;
                                velocity.Y -= 5;
                            }
                        }
                        if(difference.Width > difference.Height)
                        {
                            if (this.Position.Y < item.Position.Y)
                            {
                                velocity.Y -= 5;
                            }
                        }
                        TakeDamage(item.Damage);
                    }
                    item.Mode = 2;

                }
                if (other is Tile && ((Tile)other).Active)
                {
                    //add the tiles to the list of colliding tiles;
                    Tile tile = (Tile)other;
                    isColliding.Add(tile);
                }
            }
        }

        /// <summary>
        /// Resolves the enemies collisions with tiles
        /// Uses very similar code the Collisions&Gravity PE
        /// </summary>
        private void ResolveTileCollisions()
        {
            //resolves vertical before horizantal so enemies can bounce when they hit the side of a tile
            for (int i = 0; i < isColliding.Count; i++)
            {
                Rectangle intsRect = Rectangle.Intersect(GetRectangle(), isColliding[i].GetRectangle());
                if (isColliding[i].Active && intsRect.Height < intsRect.Width)
                {
                    if (GetRectangle().Bottom > isColliding[i].Position.Y)
                    {
                        this.Position = new Vector2(this.Position.X, this.Position.Y - intsRect.Height);
                    }
                    else if (this.Position.Y > isColliding[i].GetRectangle().Top)
                    {
                        this.Position = new Vector2(this.Position.X, this.Position.Y + intsRect.Height);
                    }
                    velocity.Y = 0;
                    if (velocity.X > .5)
                    {
                        velocity.X = .5f;
                    }
                    if (velocity.X < -.5)
                    {
                        velocity.X = -.5f;
                    }
                }
            }

            for (int i = 0; i < isColliding.Count; i++)
            {
                Rectangle intsRect = Rectangle.Intersect(GetRectangle(), isColliding[i].GetRectangle());
                if (isColliding[i].Active && intsRect.Height >= intsRect.Width)
                {
                    if (this.Position.X < isColliding[i].Position.X)
                    {
                        this.Position = new Vector2(this.Position.X - intsRect.Width, this.Position.Y);
                    }
                    else if (this.Position.X > isColliding[i].Position.X)
                    {
                        this.Position = new Vector2(this.Position.X + intsRect.Width, this.Position.Y);
                    }
                    //bounces enemy only if rectangles intersect
                    if (isColliding[i].GetRectangle().Intersects(GetRectangle()))
                    {
                        velocity.X *= -1;
                    }
                }
            }
        }

        /// <summary>
        /// Decrease enemy health if it's not invincible
        /// Kills the enemy when it's health is <= 0
        /// </summary>
        /// <param name="damage">the incoming damage</param>
        public void TakeDamage(int damage)
        {
            if (!invincible)
            {
                health -= damage;
                invincible = true;
            }

            if (health <= 0)
            {
                isDead = true;
                invincible = false;
                deathTimer = 0;
                deadObjects.Enqueue(this);
            }
        }



        /// <summary>
        /// return false if the enemy is dead,
        /// else return true;
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override Rectangle GetRectangle()
        {
            if (isDead)
            {
                return new Rectangle(0, 0, 0, 0);
            }
            return base.GetRectangle();
        }
    }
}
