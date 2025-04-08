namespace LevelEditor
{
    public partial class Form1 : Form
    {
        private const int mapPadding = 30;

        private Level? level;
        private PictureBox[,]? pictureBoxMap;
        private ObjectType? selected;
        private bool unsaved;

        public Form1() { InitializeComponent(); }

        /// <summary>
        /// sets up an empty map
        /// </summary>
        /// <param name="dimensions">dimensions of the map</param>
        private void InitializeMap(Size dimensions)
        {
            // clear group box
            groupBoxMap.Controls.Clear();

            // create an empty level
            level = new Level(dimensions);

            // create a picture box map
            pictureBoxMap = new PictureBox[dimensions.Height, dimensions.Width];

            // calculate the length of tiles for the picture boxes
            int tileLength = Math.Min(
                (groupBoxMap.Width - 2 * mapPadding) / dimensions.Width,
                (groupBoxMap.Height - 2 * mapPadding) / dimensions.Height
                );

            // loop for each tile on the map
            for (int y = 0; y < dimensions.Height; y++)
            {
                for (int x = 0; x < dimensions.Width; x++)
                {
                    // create picture box
                    PictureBox tile = new PictureBox();

                    // set up tile
                    tile.Location = new Point(
                        x * tileLength + mapPadding,
                        y * tileLength + mapPadding
                        );
                    tile.Size = new Size(
                        tileLength,
                        tileLength
                        );

                    // add click response
                    //tile.Click += pictureBoxMapTile_Click;

                    // add tile to group box
                    groupBoxMap.Controls.Add(tile);

                    // add tile to map
                    pictureBoxMap[y, x] = tile;
                }
            }
        }

        /// <summary>
        /// tries to load a level
        /// </summary>
        /// <param name="filename">filename to load</param>
        private void TryLoadLevel(string filename)
        {
            // try to load level
            Level? newLevel = Level.Load(filename);

            // show error message and return if failed
            if (newLevel == null)
            {
                MessageBox.Show(
                    "unable to load map",
                    "error loading map",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );

                return;
            }

            // update level
            level = newLevel;

            // initialise map
            InitializeMap(newLevel.Dimensions);

            // update picture box map images to tile texture
            for (int y = 0; y < level.Height; y++)
            {
                for (int x = 0; x < level.Width; x++)
                {
                    pictureBoxMap![y, x].Image = level.GetTileAt(x, y).Texture;
                }
            }
        }

        /// <summary>
        /// tries to save the level
        /// </summary>
        /// <param name="filename">filename to save as</param>
        private void TrySaveLevel(string filename)
        {
            // return if no level loaded
            if (level == null) { return; }

            // try to save level and report result
            if (level.Save(filename))
            {
                MessageBox.Show(
                    "map saved successfully",
                    "map saved",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                    );
            }
            else
            {
                MessageBox.Show(
                    "unable to save map",
                    "error saving map",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                    );
            }
        }

        /// <summary>
        /// when the new map is clicked
        /// </summary>
        private void buttonNewMap_Click(object sender, EventArgs e)
        { new FormNewMap(InitializeMap).ShowDialog(); }

        /// <summary>
        /// when the load button is clicked
        /// </summary>
        private void buttonLoad_Click(object sender, EventArgs e)
        {
            // show file dialog, return if closed
            if (openFileDialogLoadMap.ShowDialog() != DialogResult.OK) { return; }

            // try to load level
            TryLoadLevel(openFileDialogLoadMap.FileName);
        }

        /// <summary>
        /// when the save button is clicked
        /// </summary>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            // return if no level loaded
            if (level == null) { return; }

            // show dialog, return if closed
            if (saveFileDialogSaveMap.ShowDialog() != DialogResult.OK) { return; }

            // try to save level
            TrySaveLevel(saveFileDialogSaveMap.FileName);
        }
    }
}
