using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        
        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public Enemy(int health, Texture2D texture, Vector2 position)
        {
            this.health = health;
            this.texture = texture;
            this.position = position;
            home = position;
            velocity = new Vector2(.75f, 0);
            acceleration = new Vector2(0, 4);
        }

        public override void Draw(SpriteBatch sp)
        {
            sp.Draw(texture,position,new Rectangle((int)position.X,(int)position.Y,texture.Width,texture.Height),Color.White);
        }

        public override void Update(GameTime gt)
        {
            int range = 100;
            if(position.X >= home.X + range / 2 || position.X <= home.X - range/2)
            {
                velocity.X *= -1;
            }
            position.X += velocity.X;
        }

        private void CollisionResponse(GameObject other)
        {
            if (IsColliding(other))
            {
                if(other is Enemy)
                {
                    //if two enemies run into eachother, then they should turn around
                    Enemy otherEnemy = (Enemy)other;
                    velocity.X *= -1;
                    otherEnemy.Velocity = new Vector2(otherEnemy.Velocity.X * -1, otherEnemy.Velocity.Y);
                }
                if(other is Player)
                {
                    //decrease player health
                    //move player back
                    Player player = (Player)other;
                    //something like player.health -= 10; \
                    //some set amount of damage for all enemies? or maybe each enemy does its own damage
                }
                if(other is Collectible)
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
                if(other is Tile)
                {
                    //dont let the enemy fall through the ground
                }
            }

        }
    }
}
