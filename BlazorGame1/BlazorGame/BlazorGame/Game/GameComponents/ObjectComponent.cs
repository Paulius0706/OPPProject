using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents
{
    public abstract class ObjectComponent
    {
        public static int idCounter = 0;

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

        public ObjectComponent() { }
        public abstract void Update();
        public abstract void CollisonTrigger(int gameObject);

        public abstract void ConnectionUpdate();
    }
}
