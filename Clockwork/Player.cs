using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Clockwork
{
    internal class Player : GameObject
    {
        private Vector2 velocity;

        // Probably want to put gravity somewhere else, but here now for testing
        private readonly Vector2 gravity = new Vector2(0, 0.5f);
        // probably also move this to somewhere else later
        private KeyboardState prevKS;

        // player can't move below this height for now
        private float minHeight = 475;
        private float moveSpeed = 4;
        private float jumpSpeed = 9;


        public Player(Texture2D tex) 
        { 
            position = new Vector2(0, 0);

            size = new Vector2(50, 50);
            texture = tex;
        }

        public override void Update()
        {
            // everything in here should be adjusted by frame times at some point
            KeyboardState ks = Keyboard.GetState();

            velocity.Y += gravity.Y;

            // jump
            if (ks.IsKeyDown(Keys.W) && prevKS.IsKeyUp(Keys.W))
            {
                velocity.Y -= jumpSpeed;
            }

            float horDir = 0;
            if (ks.IsKeyDown(Keys.A))
                horDir--;
            if (ks.IsKeyDown(Keys.D))
                horDir++;

            velocity.X = horDir * moveSpeed;

            position += velocity;

            if (position.Y + size.Y > minHeight)
            {
                position.Y = minHeight - size.Y;
                velocity.Y = 0;
            }


            prevKS = ks;
        }

        public override void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, GetRectangle(), Color.White);
        }

        /// <summary>
        /// Gets a rectangle for the object.
        /// This should probably be in GameObject.
        /// </summary>
        /// <returns></returns>
        private Rectangle GetRectangle()
        {
            return new Rectangle(position.ToPoint(), size.ToPoint());
        }
    }
}
