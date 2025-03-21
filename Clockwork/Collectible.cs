using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Windows.Forms;

namespace Clockwork
{
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
        private Vector2 home;
        private Vector2 velocity;
        private Type collectibleType;
        private int damage;
        private bool isActive;
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
        // this constructor will need to be changed when there are multiple collectible sprites
        public Collectible(Vector2 position, Vector2 size, Type collectibletype) : base(position, size, Sprites.collectible)
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

        public override void Update(GameTime gt)
        {
            if (this.Position.Y >= home.Y + range / 2 || this.Position.Y <= home.Y - range / 2)
            {
                velocity.Y *= -1;
            }
            this.Position = new Vector2(Position.X, Position.Y + velocity.Y);
            base.Update(gt);
        }

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
