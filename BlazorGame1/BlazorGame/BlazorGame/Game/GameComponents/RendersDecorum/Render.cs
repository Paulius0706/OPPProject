using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents.RendersDecorum
{
    public abstract class Render
    {
        private int gameObjectId = -1;
        public GameObject gameObject
        {
            get
            {
                if (!MainFrame.GameObjects.ContainsKey(gameObjectId)) return null;
                return MainFrame.GameObjects[gameObjectId];
            }
            set
            {
                gameObjectId = value.Id;
                if (MainFrame.GameObjects.ContainsKey(gameObjectId))
                    MainFrame.GameObjects[gameObjectId] = value;
            }
        }

        public int[] offset { get; set; }

        public abstract void Show(ref Canvas2DContext context, int xoffset, int yoffset);

        public abstract void ConnectionUpdate(GameObject gameObject);
    }
}
