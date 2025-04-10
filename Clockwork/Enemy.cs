﻿/*
 * Who has worked on this file:
 * Philip
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D11;
using SharpDX.MediaFoundation;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Printing;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Clockwork
{
    internal class Enemy : GameObject
    {

        //the health of the enemy
        private int health;
        
        //used for movement. Currently, the home is set to always be the enemies starting position
        private Vector2 home;

        private Vector2 velocity;

        //used for gravity, but name might change if other enemies need/use other types of acceleration
        private Vector2 acceleration;

        //the total units that make up the space the enemy can move in
        private int range;

        //represents if the enemy is dead
        private bool isDead;

        //represents if the enemy is invincible or not
        private bool invincible;

        //a timer used for the enemy's i-frames
        private double timer;

        //the list of tiles that the enemy has to check for collisions
        private List<Tile> isColliding;

        private int damage;

        public int Damage
        {
            get { return damage; }
        }

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public bool Invincible
        {
            get { return invincible; }
            set { invincible = value; }
        }

        public Enemy(Vector2 position, Vector2 size, Vector2 velocity, int range, int health) : base(position, size, Sprites.Enemy)
        {
            this.health = health;
            this.range = range;
            home = this.Position;
            this.velocity = velocity;
            acceleration = new Vector2(0, .5f);
            isDead = false;
            invincible = false;
            timer = .2;
            isColliding = new List<Tile>();
            damage = 2;
        }
        public override void Draw(SpriteBatch sb)
        {
            if (!isDead)
            {
                base.Draw(sb);
            }
        }

        /// <summary>
        /// Enemies move in their set range with their home as the center
        /// <param name="gt">the game time paramter to be passed through</param>
        public override void Update(GameTime gt)
        {
            if (!isDead)
            {
                if (this.Position.X >= home.X + range / 2 - Size.X || this.Position.X <= home.X - range / 2)
                {
                    velocity.X *= -1;
                }
                this.Position = new Vector2(Position.X + velocity.X, Position.Y);
                velocity += acceleration;
                this.Position += velocity;
                if (invincible)
                {
                    timer -= gt.ElapsedGameTime.TotalSeconds;
                    if (timer <= 0)
                    {
                        invincible = false;
                        timer = .3;
                    }
                }
                ResolveTileCollisions();
                base.Update(gt);
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
                    if(displacement.Height> displacement.Width)
                    {
                        if (this.Position.X < otherEnemy.Position.X)
                        {
                            this.Position = new Vector2(Position.X - displacement.Width, Position.Y);
                        }
                        else if(this.Position.X > otherEnemy.Position.X)
                        {
                            this.Position = new Vector2(Position.X + displacement.Width, Position.Y);
                        }
                    }
                    velocity.X *= -1;
                }
                if (other is Player)
                {
                    //decrease player health
                    //move player back
                    Player player = (Player)other;
                    //something like player.health -= 10;
                    //some set amount of damage for all enemies? or maybe each enemy does its own damage
                }
                if (other is Collectible)
                {
                    ////depending on the type of collectible, decrease health
                    Collectible item = (Collectible)other;
                    System.Diagnostics.Debug.WriteLine("hit");
                    TakeDamage(item.Damage);
                    item.Mode = 2;
                    velocity.X *= -2;
                    velocity.Y -= 5;

                    //Right now, collectible handles everything, but I might change that later
                }
                if (other is Tile)
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
                if (intsRect.Height < intsRect.Width)
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
                    if(velocity.X > .5)
                    {
                        velocity.X = .5f;
                    }
                    if(velocity.X < -.5)
                    {
                        velocity.X = -.5f;
                    }
                }
            }

            for (int i = 0; i < isColliding.Count; i++)
            {
                Rectangle intsRect = Rectangle.Intersect(GetRectangle(), isColliding[i].GetRectangle());
                if (intsRect.Height >= intsRect.Width)
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
                        velocity.X *= 1;
                    }
                }
            }
        }

        //test IsColliding method for milestone 1.5, can be changed.
        //Checks if the other is an enemy since I didn't want to mess with gameObject

        /// <summary>
        /// Checks if this object is colliding with another
        /// </summary>
        /// <param name="other">the game object to check collision with</param>
        /// <returns>a bool represnting if they collide or not</returns>

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
            }
        }

        /// <summary>
        /// Creates and returns a rectangle reprenting the enemy.
        /// If the enemy is dead, then 
        /// </summary>
        /// <returns>A rectangle with the same position and texture width and height as the enemy</returns>
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

