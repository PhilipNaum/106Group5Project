/*
 * Who has worked on this file:
 * Philip
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using SharpDX.DirectWrite;
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
        //used for movement. Currently, the home is set to always be the collecible's starting position
        private Vector2 home;

        private Vector2 velocity;

        //the type of collectible this collectible is
        private Type collectibleType;

        //the damage the collectible does. Only used for weapons(gear, hand, and chime)
        private int damage;

        //How to check whether the collectible should float in place, is being used, or neither
        //0 is floating in place, waiting to be collected
        //1 is activley being used
        //2 is can not be collected, is not being used
        private int mode;

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

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }


        public int Mode
        {
            get { return mode; }
            set { mode = value; }
        }

        public Collectible(Vector2 position, Vector2 size, Type collectibletype, int mode) : base(position, size, collectibletype)
        {
            this.collectibleType = collectibletype;
            this.mode = mode;
            home = this.Position;
            range = 7;
            velocity = new Vector2(0, .05f);
        }
        
        public Collectible(Vector2 position, Vector2 size,Type collectibletype, int mode, int damage)
            : this(position, size, collectibletype, mode)
        {
            this.damage = damage;
        }

        public override void Draw(SpriteBatch sb)
        {
            if (mode != 2)
            {
                base.Draw(sb);
            }
        }
            
        

        /// <summary>
        /// Makes the item float up and down before being collected if mode is 0;
        /// </summary>
        public override void Update(GameTime gt)
        { 
            if (mode==0)
            {
                if (this.Position.Y >= home.Y + range / 2 || this.Position.Y <= home.Y - range / 2)
                {
                    velocity.Y *= -1;
                }
                this.Position = new Vector2(Position.X, Position.Y + velocity.Y);
            }
            else if (mode == 1)
            {
                switch (CollectibleType)
                {
                    case Type.Gear:
                        range = 400;
                        if (Vector2.Distance(home, this.Position) < range)
                        {
                            this.Position += velocity*14;
                        }
                        else
                        {
                            mode = 2;
                        }
                        break;
                    case Type.Hand:
                        Vector2 test = this.Position;
                        for(int i = 0; i < 45; i++)
                        {
                            test.X += 1;
                            test.Y += 2;
                        }
                        this.Position = test;
                        break;
                }
            }
            base.Update(gt);
        }

        /// <summary>
        /// Performs collision test and responds approriatley
        /// </summary>
        /// <param name="other">the other game object to be checkd</param>
        public void CollisionResponse(GameObject other)
        {
            if (IsColliding(other))
            {
                //sets mode to 2 if the player touches it
                //(makes it completely inactive)
                //Only do this if the item can be collected
                //(mode 0)
                if (other is Player && mode == 0)
                {
                    mode = 2;
                }

                //if it hits an enemy, then do appropriate damage
                if (other is Enemy)
                {
                    Enemy otherEnemy = (Enemy)other;

                    switch (collectibleType)
                    {
                        case Type.Gear:
                            otherEnemy.TakeDamage(damage);
                            if (mode == 1)
                            {
                                mode = 2;
                            }
                            break;
                    }
                }

                if (other is Tile && mode == 1)
                {
                    System.Diagnostics.Debug.WriteLine("test");
                    mode = 2;
                }
            }
        }

        /// <summary>
        /// Only returns a valid rectangle if mode is 2
        /// Might be changed later once level manager is done
        /// </summary>
        /// <returns></returns>
        public override Rectangle GetRectangle()
        {
            if (mode == 2)
            {
                return new Rectangle(0, 0, 0, 0);
            }
            return base.GetRectangle();
        }

    }
}
