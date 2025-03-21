using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Windows.Forms;

namespace Clockwork
{
    //types the collectible can be
    public enum Type
    {
        Gear,
        Hand,
        Face,
        Key,
        Chime
    }
    internal class Collectible : GameObject
    {
        private Texture2D texture;

        private Vector2 position;

        //used for movement. Currently, the home is set to always be the enemies starting position
        private Vector2 home;

        private Vector2 velocity;

        private Type collectibleType;

        //the damage the collectible does. Only used for weapons(gear, hand, and chime)
        private int damage;
        

        //whether the item can be collected or not
        private bool isActive;

        //the total units that make up the space the item floats in before being collected
        private int range;

        public Type CollectibleType
        {
            get { return collectibleType; }
        }

        public int Damage
        {
            get { return damage; }
        }

        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }

        /// <summary>
        /// creates a new item to be collected
        /// </summary>
        /// <param name="texture">the item's texture</param>
        /// <param name="position">the item's current position</param>
        /// <param name="collectibletype">the type of collectible</param>
        public Collectible(Texture2D texture, Vector2 position, Type collectibletype)
        {
            this.texture = texture;
            this.position = position;
            this.collectibleType = collectibletype;
            damage = 0;
            isActive = true;
            home = position;
            range = 7;
            velocity = new Vector2(0, .05f);
        }
        public override void Draw(SpriteBatch sp)
        {
            if (IsActive)
            {
                sp.Draw(texture, position, Color.White);

                sp.Draw(texture, new Rectangle((int)home.X-(texture.Width/4), (int)home.Y + 175,75,10),Color.Gray);
            }
        }

        /// <summary>
        /// Makes the item float up and down before being collected
        /// </summary>
        /// <param name="gt"></param>
        public override void Update(GameTime gt)
        {
            if (position.Y >= home.Y + range / 2 || position.Y <= home.Y - range / 2)
            {
                velocity.Y *= -1;
            }
            position.Y += velocity.Y;
            
        }
        //Basic IsColliding override
        public override bool IsColliding(GameObject other)
        {
            if(other is Player)
            {
                isActive = false;
            }
            return base.IsColliding(other);
        }

    }
}
