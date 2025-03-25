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
        
        public Collectible(Vector2 position, Vector2 size, Type collectibletype) : base(position, size, collectibletype)
        {
            this.collectibleType = collectibletype;
            damage = 0;
            isActive = true;
            home = this.Position;
            range = 7;
            velocity = new Vector2(0, .05f);
        }
        public override void Draw(SpriteBatch sb)
        {
            if (IsActive)
            {
                base.Draw(sb);
            }
        }

        /// <summary>
        /// Makes the item float up and down before being collected
        /// </summary>
        /// <param name="gt"></param>
        public override void Update(GameTime gt)
        {
            if (this.Position.Y >= home.Y + range / 2 || this.Position.Y <= home.Y - range / 2)
            {
                velocity.Y *= -1;
            }
            this.Position = new Vector2(Position.X, Position.Y + velocity.Y);
            base.Update(gt);
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
