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
            new(false, true, Sprites.tileDirtL),
            new(false, true, Sprites.tileDirtToGrassR),
            new(false, true, Sprites.tileGrass),
            new(false, true, Sprites.tileGrassDark),
            new(false, true, Sprites.tileGrassDarkToLight),
            new(false, true, Sprites.tileGrassEndL),
            new(false, true, Sprites.tileGrassEndR),
            new(false, true, Sprites.tileGrassLightToDark),
            new(false, true, Sprites.tileGrassToDirtL),
            new(false, true, Sprites.tileGroundBlank),
            new(true, true, Sprites.tileGroundEndL1),
            new(false, true, Sprites.tileGroundEndL2),
            new(false, true, Sprites.tileGroundEndR1),
            new(false, true, Sprites.tileGroundEndL2),
            new(false, true, Sprites.tileGroundRocks),
            new(false, true, Sprites.tileGroundRocks2),
            new(false, true, Sprites.tileGroundRocksLeavesBottom),
            new(false, true, Sprites.tileGroundRocksLeavesTop),
            new(false, true, Sprites.tileGroundTop1),
            new(false, true, Sprites.tileGroundTop2),
            new(false, true, Sprites.tileGroundTop3),
            new(false, true, Sprites.tileGroundTopRocks),
            new(false, true, Sprites.tileGroundTopRocksVines),
            new(false, true, Sprites.tileGroundTopVines1),
        };

        /// <summary>
        /// an array of all filenames of levels
        /// </summary>
        private static string[] levelFilenames = {
            "Levels/MovementIntro.map",
            "Levels/GearIntro.map",
            "Levels/ChimeIntro.map",
            "Levels/RewindIntro.map",
            "Levels/HandIntro.map",
            //"Levels/BreakableTilesIntro.map",
            "Levels/DestructibleLevel.map",
            //"Levels/AOEDash.map",
            "Levels/AOERewind.map"
        };

        private static string[] enemyFilenames =
        {
            "none",
            "Enemies/GearIntroEnemies.data",
            "none",
            "none",
            "Enemies/HandIntroEnemies.data",
            //"none",
            "none",
            //none",
            "Enemies/AOERewindEnemies.data"
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

            level.StartPosition = new Vector2(input.ReadInt32() * 32 + 32, input.ReadInt32() * 32);
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
        public void SetCurrentLevel(int index)
        {
            // range check
            if (index < 0 || index >= levelFilenames.Length) { return; }

            currentLevel = LoadLevel(levelFilenames[index]);
            currentLevelIndex = index;
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
