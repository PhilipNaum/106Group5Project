using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Clockwork
{
    internal class Menu
    {
        /// <summary>
        /// List of all UIElements in the Menu
        /// </summary>
        public List<UIElement> UIElements { get; private set; }

        /// <summary>
        /// Create a new Menu
        /// </summary>
        /// <param name="uiElements">List of UIElements in the new Menu</param>
        public Menu(List<UIElement> uiElements)
        {
            UIElements = uiElements;
        }

        /// <summary>
        /// Update the Menu
        /// </summary>
        public void Update()
        {
            foreach (UIElement e in UIElements)
                e.Update();
        }

        /// <summary>
        /// Draw the Menu
        /// </summary>
        /// <param name="sb"></param>
        public void Draw(SpriteBatch sb)
        {
            foreach (UIElement e in UIElements)
                e.Draw(sb);
        }
    }
}
