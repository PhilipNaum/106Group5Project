using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Clockwork
{
    internal class Menu
    {
        public List<UIElement> UIElements { get; private set; }

        public Menu(List<UIElement> uiElements)
        {
            UIElements = uiElements;
        }

        public void Update()
        {
            foreach (UIElement e in UIElements)
                e.Update();
        }

        public void Draw(SpriteBatch sb)
        {
            foreach (UIElement e in UIElements)
                e.Draw(sb);
        }
    }
}
