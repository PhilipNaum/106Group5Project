namespace LevelEditor
{
    public partial class Form1 : Form
    {
        private const int mapPadding = 30;
        private const int selectorPadding = 5;
        private const int selectorSpacing = 5;

        private Level? level;
        private PictureBox[,]? pictureBoxMap;
        private ObjectType? selected;
        private bool unsaved;
        private Button[]? tileSelectionButtons;
        private Button[]? collectibleSelectionButtons;

        public Form1()
        {
            InitializeComponent();

            InitializeTileSelector();
            InitializeCollectibleSelector();

            SelectObject(Objects.TileTypes[0]);
        }

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
                    tile.Size = new Size(tileLength, tileLength);
                    tile.BackColor = Color.PowderBlue;
                    tile.SizeMode = PictureBoxSizeMode.StretchImage;
                    tile.BackgroundImageLayout = ImageLayout.Stretch;

                    // add click response
                    tile.Click += pictureBoxMapTile_Click;

                    // add tile to group box
                    groupBoxMap.Controls.Add(tile);

                    // add tile to map
                    pictureBoxMap[y, x] = tile;
                }
            }
        }

        /// <summary>
        /// sets up tile selector
        /// </summary>
        private void InitializeTileSelector()
        {
            // calculate length of tile buttons
            int buttonLength = tabPageTiles.Height - 2 * selectorPadding;

            tileSelectionButtons = new Button[Objects.TileTypes.Length];

            // loop for all tile indices
            for (int i = 0; i < Objects.TileTypes.Length; i++)
            {
                // create button
                Button button = new Button();

                // set up button
                button.Location = new Point(
                    selectorPadding + i * (buttonLength + selectorSpacing),
                    selectorPadding
                    );
                button.Size = new Size(buttonLength, buttonLength);
                button.Image = Objects.TileTypes[i].Texture;

                // add click response
                button.Click += tileSelectionButton_Click;

                // add button to tab page
                tabPageTiles.Controls.Add(button);

                // add button to array
                tileSelectionButtons[i] = button;
            }
        }

        /// <summary>
        /// sets up collectible (item) selector
        /// </summary>
        private void InitializeCollectibleSelector()
        {
            // calculate length of collectible buttons
            int buttonLength = tabPageItems.Height - 2 * selectorPadding;

            collectibleSelectionButtons = new Button[Objects.CollectibleTypes.Length];

            // loop for all collectible indices
            for (int i = 0; i < Objects.CollectibleTypes.Length; i++)
            {
                // create button
                Button button = new Button();

                // set up button
                button.Location = new Point(
                    selectorPadding + i * (buttonLength + selectorSpacing),
                    selectorPadding
                    );
                button.Size = new Size(buttonLength, buttonLength);
                button.Image = Objects.CollectibleTypes[i].Texture;

                // add click response
                button.Click += collectibleSelectionButton_Click;

                // add button to tab page
                tabPageItems.Controls.Add(button);

                // add button to array
                collectibleSelectionButtons[i] = button;
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

            // initialise map
            InitializeMap(newLevel.Dimensions);

            // update level
            level = newLevel;

            // update picture box map images to tile texture
            for (int y = 0; y < level.Height; y++)
            {
                for (int x = 0; x < level.Width; x++)
                {
                    pictureBoxMap![y, x].BackgroundImage = level.GetTileAt(x, y).Texture;
                }
            }

            // loop for each collectible
            foreach (KeyValuePair<Point, int> collectiblePair in level.Collectibles)
            {
                int x = collectiblePair.Key.X;
                int y = collectiblePair.Key.Y;

                pictureBoxMap![y, x].Image = level.GetCollectibleAt(x, y)!.Texture;
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
        /// paints the selected tile / collectible
        /// </summary>
        /// <param name="x">x</param>
        /// <param name="y">y</param>
        private void PaintObject(int x, int y)
        {
            // return if unready
            if (
                level == null ||
                selected == null ||
                pictureBoxMap == null
                ) { return; }

            // check object category
            if (selected.Category == ObjectCategory.Tile)
            {
                // paint tile
                level.SetTileAt(x, y, selected);
                pictureBoxMap[y, x].BackgroundImage = selected.Texture;
            }
            else if (selected.Category == ObjectCategory.Collectible)
            {
                // place collectible
                level.SetCollectibleAt(x, y, selected);
                pictureBoxMap[y, x].Image = selected.Texture;
            }
        }

        /// <summary>
        /// selects an object
        /// </summary>
        /// <param name="selection">the selection</param>
        private void SelectObject(ObjectType selection)
        {
            // update selected and picture box
            selected = selection;
            pictureBoxSelected.Image = selection.Texture;
        }

        /// <summary>
        /// when a tile selection button is clicked
        /// </summary>
        private void tileSelectionButton_Click(object? sender, EventArgs e)
        {
            // return if unready
            if (sender == null || tileSelectionButtons == null) { return; }

            // get tile index
            int index = Array.IndexOf(tileSelectionButtons, (Button)sender);

            // select the tile
            SelectObject(Objects.TileTypes[index]);
        }

        /// <summary>
        /// when a collectible selection button is clicked
        /// </summary>
        private void collectibleSelectionButton_Click(object? sender, EventArgs e)
        {
            // return if unready
            if (sender == null || collectibleSelectionButtons == null) { return; }

            // get collectible index
            int index = Array.IndexOf(collectibleSelectionButtons, (Button)sender);

            // select the collectible
            SelectObject(Objects.CollectibleTypes[index]);
        }

        /// <summary>
        /// when a tile is clicked
        /// </summary>
        private void pictureBoxMapTile_Click(object? sender, EventArgs e)
        {
            if (
                sender == null ||
                level == null ||
                pictureBoxMap == null
                ) { return; }

            // find the tile's location and paint the tile
            for (int y = 0; y < level.Height; y++)
            {
                for (int x = 0; x < level.Width; x++)
                {
                    if (pictureBoxMap[y, x] == sender) { PaintObject(x, y); }
                }
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
