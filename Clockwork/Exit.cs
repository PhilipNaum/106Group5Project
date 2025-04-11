using Microsoft.Xna.Framework;

namespace Clockwork
{
    internal class Exit : GameObject
    {
        public bool CanExit { get; private set; }

        public Exit(Vector2 position) : base(position, new Vector2(0, 0), Sprites.Exit)
        { CanExit = false; }

        public bool ExitCheck(Level l)
        {
            foreach (Collectible c in l.Collectibles)
                if (c.Mode != 2) return false;
            return true;
        }

        public void Update(GameTime gt, Level l)
        {
            base.Update(gt);
            CanExit = ExitCheck(l);
        }
    }
}