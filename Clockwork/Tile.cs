/*
 * Who has worked on this file:
 * Nathan
 */
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D11;
using System.Diagnostics;
using System.Threading;

namespace Clockwork
{
    /// <summary>
    /// a tile
    /// </summary>
    internal class Tile : GameObject
    {
        /// <summary>
        /// the length of a tile
        /// </summary>
        public const int TileLength = 32;
        // this has to match the size of the tile sprite

        private TileType tileType;
        private bool active;
        private Point gridPosition;

        // only used if the tile is destructible
        private bool tileTouched;
        private readonly float tileDestructTimer = 1;
        private float tileDestructCountdown;

        /// <summary>
        /// the type of the tile
        /// </summary>
        public TileType TileType { get => tileType; }

        /// <summary>
        /// whether the tile is active (not destroyed)
        /// </summary>
        public bool Active { get => active; set { active = value; } }

        /// <summary>
        /// the position of the tile on the level grid
        /// </summary>
        /// 
        public Point GridPosition { get => gridPosition; }


        /// <summary>
        /// creates a tile
        /// </summary>
        /// <param name="tileType">the tile type</param>
        /// <param name="gridPosition">the position of the tile on the level grid</param>
        public Tile(TileType tileType, Point gridPosition)
            : base(gridPosition.ToVector2() * new Vector2(TileLength), new Vector2(TileLength), tileType.TileSprite)
        {
            // calculate and set position based on grid position
            Position = gridPosition.ToVector2() * TileLength;
            
            // set active
            active = true;

            // set tile type
            this.tileType = tileType;

            // set grid position
            this.gridPosition = gridPosition;

            tileDestructCountdown = tileDestructTimer;

        }

        public override void Update(GameTime gt)
        {
            if (active && tileType.Breakable && tileTouched)
            {
                tileDestructCountdown -= (float)gt.ElapsedGameTime.TotalSeconds;

                if (tileDestructCountdown <= 0)
                {
                    active = false;
                    tileTouched = false;
                    deadObjects.Enqueue(this);
                    tileDestructCountdown = tileDestructTimer;
                }
            }

            base.Update(gt);
        }

        /// <summary>
        /// draws the tile
        /// </summary>
        public override void Draw(SpriteBatch spriteBatch)
        {
            // draw the tile if active
            if (active) { base.Draw(spriteBatch); }
        }

        /// <summary>
        /// Starts a timer before the tile breaks.
        /// </summary>
        public void TilePlayerCollision()
        {
            tileTouched = true;
        }

        /// <summary>
        /// Instantly breaks the tile.
        /// </summary>
        public void TileWeaponCollision()
        {
            tileTouched = true;
            tileDestructCountdown = 0;
        }

        /// <summary>
        /// checks collision of the tile with another object, returns false if it's inactive or non-collidable
        /// </summary>
        /// <param name="other">the object to check</param>
        /// <returns>whether or not it is colliding</returns>
        public override bool IsColliding(GameObject other)
            => active && tileType.Collidable ?
            base.IsColliding(other) :
            false
            ;
    }
}
