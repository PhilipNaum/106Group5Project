using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Printing;
using System.Security.Cryptography.X509Certificates;

namespace Clockwork
{
    internal class Enemy : GameObject
    {
        private int health;
        private Vector2 home;
        private Vector2 velocity;
        private Vector2 acceleration;
        private int range;
        private Rectangle collisionArea;

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public int Width
        {
            get { return (int)Size.X; }
        }

        public int Height
        {
            get { return (int)Size.Y; }
        }


        public Enemy(Vector2 position, Vector2 size, Vector2 velocity, int range, int health) : base(position, size, Sprites.enemy)
        {
            this.health = health;
            this.range = range;
            home = this.Position;
            this.velocity = velocity;
            acceleration = new Vector2(0, .5f);
        }

        public override void Draw(SpriteBatch sb)
        {
            base.Draw(sb);
        }

        /// <summary>
        /// Enemy moves starts in a set position, and then can move  
        /// </summary>
        /// <param name="gt"></param>
        public override void Update(GameTime gt)
        {
            if (this.Position.X >= home.X + range / 2 || this.Position.X <= home.X - range / 2)
            {
                velocity.X *= -1;
            }
            this.Position = new Vector2(Position.X + velocity.X, Position.Y);
            if (this.Position.Y < 300)
            {
                velocity += acceleration;
                this.Position += velocity;
            }
            base.Update(gt);
        }

        public void CollisionResponse(GameObject other)
        {
            if (IsColliding(other))
            {
                if (other is Enemy)
                {
                    //if two enemies run into eachother, then they should turn around
                    Enemy otherEnemy = (Enemy)other;
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
                    //depending on the type of collectible, decrease health
                    Collectible item = (Collectible)other;
                    if (item.CollectibleType == Type.Hand
                        || item.CollectibleType == Type.Gear
                        || item.CollectibleType == Type.Chime)
                    {
                        health -= item.Damage;
                    }
                }
                if (other is Tile)
                {
                    //dont let the enemy fall through the ground
                    //Tile tile = (Tile)other;
                    //Rectangle intsRect = Rectangle.Intersect(this.collisionbox,)
                }
            }
        }

        public override bool IsColliding(GameObject other)
        {
            if(other is Enemy)
            {
                Enemy otherEnemy = (Enemy)other;
                if (GetRectangle().Intersects(otherEnemy.GetRectangle()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            
        }
    }
}

