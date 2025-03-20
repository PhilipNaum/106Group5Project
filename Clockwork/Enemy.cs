using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel.Design.Serialization;
using System.Drawing.Printing;
using System.Security.Cryptography.X509Certificates;

namespace Clockwork
{
    internal class Enemy : GameObject
    {
        private int health;
        private Texture2D texture;
        private Vector2 position;
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

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public int Width
        {
            get { return texture.Width; }
        }

        public int Height
        {
            get { return texture.Height; }
        }


        public Enemy(Texture2D texture, Vector2 position, Vector2 velocity, int range, int health)
        {
            this.health = health;
            this.texture = texture;
            this.position = position;
            this.range = range;
            home = position;
            this.velocity = velocity;
            acceleration = new Vector2(0, .5f);
        }

        public override void Draw(SpriteBatch sp)
        {
            sp.Draw(texture, position, Color.White);
        }

        public override void Update(GameTime gt)
        {
            if (position.X >= home.X + range / 2 || position.X <= home.X - range / 2)
            {
                velocity.X *= -1;
            }
            position.X += velocity.X;
            if (position.Y < 300)
            {

                ApplyGravity();
            }
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
                if (position.X >= otherEnemy.Position.X)
                {

                    if (position.X <= otherEnemy.Position.X + otherEnemy.Width)
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
                    if (position.X + Width >= otherEnemy.Position.X)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            
        }

        public void ApplyGravity()
        {
            velocity += acceleration;
            position += velocity;
        }
    }
}

