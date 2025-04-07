namespace LevelEditor
{
    public partial class FormNewMap : Form
    {
        public delegate void CreateMap(Size dimensions);

        private event CreateMap MapCreated;

        public FormNewMap(CreateMap mapCreatedHandler) {
            InitializeComponent();

            MapCreated += mapCreatedHandler;
        }

        /// <summary>
        /// tries to parse the entered dimensions
        /// </summary>
        /// <returns>the dimensions, null otherwise</returns>
        public Size? TryParseDimensions()
        {
            // try to parse width
            int width;
            if (!int.TryParse(textBoxWidth.Text, out width))
            { return null; }

            // try to parse height
            int height;
            if (!int.TryParse(textBoxHeight.Text, out height))
            { return null; }

            return new Size(width, height);
        }

        /// <summary>
        /// makes sure dimensions are in range (EMPTY)
        /// </summary>
        /// <returns>whether the dimensions are in range</returns>
        private bool ValidateDimensions() => true;
    }
}
