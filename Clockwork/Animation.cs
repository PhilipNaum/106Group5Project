namespace AnimationHelper
{
    // Emma Rausch
    // 27/2/25
    /// <summary>
    /// Helper class for AnimatedSprite that tracks animations within the frames list
    /// </summary>
    public class Animation
    {
        // === Fields and Properties

        // Internal frame tracker
        // Indexed from 0
        private int frameOn;

        /// <summary>
        /// Index of the begining of the animation
        /// </summary>
        public int Start { get; set; }

        /// <summary>
        /// Index of the end of the animation
        /// </summary>
        public int End { get; set; }


        public int Length
        {
            get => End - Start + 1;
        }

        /// <summary>
        /// FPS the animation plays at
        /// </summary>
        public int FPS { get; set; }

        /// <summary>
        /// Frame the animation is currently on
        /// </summary>
        public int CurrentFrame
        {
            get => Start + frameOn;
        }


        // === Constructors ===

        /// <summary>
        /// Create a new Animation
        /// </summary>
        /// <param name="start">Start of the animation in the AnimatedSprite's frames list</param>
        /// <param name="end">End of the animation in the AnimatedSprite's frames list</param>
        /// <param name="fps">Frames per second that the animation should play at</param>
        public Animation(int start, int end, int fps)
        {
            this.Start = start;
            this.End = end;
            this.FPS = fps;
            this.frameOn = 0;
        }


        // === Methods ===

        /// <summary>
        /// Reset the animation to start playing
        /// </summary>
        public void StartAnimation()
        {
            frameOn = 0;
        }

        /// <summary>
        /// Move to the next frame in the animation
        /// </summary>
        /// <returns>Current frame the animation is on</returns>
        public int NextFrame()
        {
            frameOn = (frameOn + 1) % Length;
            return CurrentFrame;
        }
    }
}
