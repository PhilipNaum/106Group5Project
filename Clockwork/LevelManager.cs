﻿/*
 * Who has worked on this file:
 * Nathan
 */
using Microsoft.Xna.Framework;
using System;
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
        };

        /// <summary>
        /// an array of all filenames of levels
        /// </summary>
        private static string[] levelFilenames = {
        };

        private int currentLevelIndex;
        private Level currentLevel;

        /// <summary>
        /// the index of the current level
        /// </summary>
        public int CurrentLevelIndex { get => currentLevelIndex;}

        /// <summary>
        /// the current level of the game
        /// </summary>
        public Level CurrentLevel { get => currentLevel;}

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
                }
            }

            //TODO load collectibles

            input.Close();

            return level;
        }

        /// <summary>
        /// set the current level based on level index
        /// </summary>
        /// <param name="index">level index</param>
        public void SetCurrentLevel(int index)
        {
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
    }
}
