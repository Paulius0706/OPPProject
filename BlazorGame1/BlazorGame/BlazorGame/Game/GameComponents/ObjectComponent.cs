using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents
{
    public abstract class ObjectComponent
    {
        public static int idCounter = 0;
        public int id;

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

        public ObjectComponent() { }
        public abstract void Update();
        public abstract void CollisonTrigger(int gameObject, string data, int number);

        public abstract void ConnectionUpdate();
    }
}
