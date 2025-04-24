/*
 * Who has worked on this file:
 * Nathan
 */
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace Clockwork
{
    internal class LevelManager
    {
        #region Singleton stuff

        private LevelManager() { }

        private static LevelManager instance = null;

        public static LevelManager Instance
        {
            get
            {
                if (instance == null)
                { instance = new LevelManager(); }

                return instance;
            }
        }

        #endregion

        /// <summary>
        /// an array of all tile types
        /// </summary>
        private static TileType[] tileTypes = {
            new(false, false, Sprites.Empty),
           new(false, false, Sprites.tileGrassTopEndL),
           new(false, false, Sprites.tileGrassToDirtTop),
           new(false, false, Sprites.tileDirtTop),
           new(false, false, Sprites.tileDirtToGrassTop),
           new(false, false, Sprites.tileGrassTop),
           new(false, false, Sprites.tileGrassToShadeTop),
           new(false, false, Sprites.tileShadeTop),
           new(false, false, Sprites.tileShadeToGrassTop),
           new(false, false, Sprites.tileGrassTopEndR),
           new(false, true, Sprites.tileGrassEndLBottom),
           new(false, true, Sprites.tileGrassToDirtBottom),
           new(false, true, Sprites.tileDirtBottom),
           new(false, true, Sprites.tileDirtToGrassBottom),
           new(false, true, Sprites.tileGrassBottom),
           new(false, true, Sprites.tileGrassToShadeBottom),
           new(false, true, Sprites.tileShadeBottom),
           new(false, true, Sprites.tileShadeToGrassBottom),
           new(false, true, Sprites.tileGrassEndRBottom),
           new(false, true, Sprites.tileGroundEndL),
           new(false, true, Sprites.tileGroundRocks1),
           new(false, true, Sprites.tileGroundRocksLeaves),
           new(false, true, Sprites.tileGroundEmpty),
           new(false, true, Sprites.tileGroundRocks2),
           new(false, true, Sprites.tileGroundEndR),
           new(false, true, Sprites.tileGroundBottomEndL),
           new(false, true, Sprites.tileGroundRocks3),
           new(false, true, Sprites.tileGroundRocks4),
           new(false, true, Sprites.tileGroundBottomEndR),
           new(true, true, Sprites.tileDestructible)
        };

        /// <summary>
        /// an array of all filenames of levels
        /// </summary>
        private static string[] levelFilenames = {
            "Levels/MovementIntro.map",
            "Levels/BreakableTilesIntro.map",
            "Levels/GearIntro.map",
            "Levels/ChimeIntro.map",
            "Levels/RewindIntro.map",
            "Levels/HandIntro.map",
            "Levels/AOEDash.map",
            "Levels/AOERewind.map"
        };

        private static string[] enemyFilenames =
        {
            "none",
            "none",
            "Content/Enemies/GearIntroEnemies.data",
            "none",
            "none",
            "Content/Enemies/HandIntroEnemies.data",
            "none",
            "Content/Enemies/AOERewindEnemies.data"
        };

        /// <summary>
        /// (untested) loads a level from filename, returns null if failed
        /// </summary>
        /// <param name="filename">the filename of the map file</param>
        /// <returns>the level</returns>
        private static Level LoadLevel(string filename)
        {
            // try to open file stream, return null if failed
            FileStream stream;
            try { stream = File.OpenRead(filename); }
            catch (Exception) { return null; }

            BinaryReader input = new BinaryReader(stream);

            // read the level dimensions
            Point dimensions = new Point(
                input.ReadInt32(),
                input.ReadInt32()
                );

            // create an empty level with dimensions
            Level level = new Level(dimensions);

            // loop for each tile on the map
            for (int y = 0; y < dimensions.Y; y++)
            {
                for (int x = 0; x < dimensions.X; x++)
                {
                    // read the tile type of the current tile
                    TileType tileType = tileTypes[input.ReadByte()];

                    // place tile on the map
                    level.Map[y, x] = new Tile(tileType, new Point(x, y));
                    if (tileType.Collidable) { level.CollidableTiles.Add(level.Map[y, x]); }
                }
            }

            // read and loop for the number of collectibles
            int collectibleCount = input.ReadInt32();
            for (int i = 0; i < collectibleCount; i++)
            {
                // read collectible type
                Type collectibleType = (Type)input.ReadByte();

                // read collectible position and calculate to screen position
                Vector2 collectiblePosition = new Point(
                    input.ReadInt32(),
                    input.ReadInt32()
                    ).ToVector2() * Tile.TileLength;

                // add collectible to list
                level.Collectibles.Add(new Collectible(
                    collectiblePosition,
                    new Vector2(16, 16),
                    collectibleType,
                    0
                    ));
            }

            level.StartPosition = new Vector2(input.ReadInt32() * 32, input.ReadInt32() * 32);
            level.SetExit(new Exit(new Vector2(input.ReadInt32() * 32, input.ReadInt32() * 32)));
            level.Enemies.AddRange(LoadEnemies(filename));
            input.Close();

            return level;
        }
        private static List<Enemy> LoadEnemies(string filename)
        {

            List<Enemy> enemies = new List<Enemy>();

            //get the enemy file that corresponds to this level
            int index = Array.IndexOf(levelFilenames, filename);
            string currentEnemyFile = enemyFilenames[index];

            if (currentEnemyFile == "none")
            {
                return enemies;
            }

            FileStream stream;
            try { stream = File.OpenRead(currentEnemyFile); }
            catch (Exception) { return enemies; }

            BinaryReader input = new BinaryReader(stream);

            int enemyCount = input.ReadInt32();
            for (int i = 0; i < enemyCount; i++)
            {
                //get enemy position
                Vector2 enemyPos = new Vector2(input.ReadInt32(), input.ReadInt32());

                //get enemy range
                int range = input.ReadInt32();

                //get enemy size
                int size = input.ReadInt32();

                //get enemy health
                int health = input.ReadInt32();

                enemies.Add(new Enemy(enemyPos, new Vector2(size, size), new Vector2(-.5f, 0), range, health));
            }

            return enemies;
        }

        private int currentLevelIndex;
        private Level currentLevel;

        /// <summary>
        /// the index of the current level
        /// </summary>
        public int CurrentLevelIndex { get => currentLevelIndex; }

        /// <summary>
        /// the current level of the game
        /// </summary>
        public Level CurrentLevel { get => currentLevel; }

        /// <summary>
        /// set the current level based on level index
        /// </summary>
        /// <param name="index">level index</param>
        public bool SetCurrentLevel(int index)
        {
            // range check
            if (index < 0 || index >= levelFilenames.Length) { return false; }

            currentLevel = LoadLevel(levelFilenames[index]);
            currentLevelIndex = index;
            return true;
        }

        /// <summary>
        /// overload for setting a custom level with filename of a map file, for testing purposes
        /// </summary>
        /// <param name="filename">map file filename</param>
        /// <param name="index">level index</param>
        public void SetCurrentLevel(string filename, int index)
        {
            currentLevel = LoadLevel(filename);
            currentLevelIndex = index;
        }

        /// <summary>
        /// Reloads the current level.
        /// Used for player deaths and restarting.
        /// </summary>
        public void ReloadLevel()
        {
            SetCurrentLevel(currentLevelIndex);
        }
    }
}
