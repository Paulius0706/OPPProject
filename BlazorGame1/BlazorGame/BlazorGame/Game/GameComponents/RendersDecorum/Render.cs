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
                if (!MainFrame.gameObjects.ContainsKey(gameObjectId)) return null;
                return MainFrame.gameObjects[gameObjectId];
            }
            set
            {
                gameObjectId = value.id;
                if (MainFrame.gameObjects.ContainsKey(gameObjectId))
                    MainFrame.gameObjects[gameObjectId] = value;
            }
        }

        public enum Type
        {
            UI,
            GameUI,
            Body,
            Gun,
            Guns,
            Health,
            Exp

        }
        public Type type;
        public int[] offset { get; set; }

        public abstract void Show(ref Canvas2DContext context, int xoffset, int yoffset);

        public abstract void ConnectionUpdate(GameObject gameObject);
    }
}
