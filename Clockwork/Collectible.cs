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
        private Texture2D texture;
        private Vector2 position;
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

        public override void Update(GameTime gt)
        {
            if (position.Y >= home.Y + range / 2 || position.Y <= home.Y - range / 2)
            {
                velocity.Y *= -1;
            }
            position.Y += velocity.Y;
            
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
