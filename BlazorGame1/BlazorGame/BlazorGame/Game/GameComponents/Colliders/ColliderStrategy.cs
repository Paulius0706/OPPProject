using BlazorGame.Game.GameComponents.Colliders;
using BlazorGame.Game.GameObjects;


namespace BlazorGame.Game.GameComponents.Colliders
{
    public abstract class ColliderStrategy
    {
        private int colliderId = -1;
        private int gameObjectId = -1;
        public GameObject gameObject
        {
            get{ if (!MainFrame.gameObjects.ContainsKey(gameObjectId)) return null; return MainFrame.gameObjects[gameObjectId]; }
            set{ gameObjectId = value.id; if (MainFrame.gameObjects.ContainsKey(gameObjectId)) MainFrame.gameObjects[gameObjectId] = value; }
        }
        public Collider collider
        {
            get
            {
                if (!MainFrame.gameObjects.ContainsKey(gameObjectId) 
                    || colliderId == -1 
                    || MainFrame.gameObjects[gameObjectId].components.Count >= colliderId
                    || MainFrame.gameObjects[gameObjectId].components[colliderId] is not Collider) return null;
                return (MainFrame.gameObjects[gameObjectId].components[colliderId] as Collider);
            }
            set
            {
                colliderId = value.id;
                if (MainFrame.gameObjects.ContainsKey(gameObjectId)
                    && colliderId != -1
                    && MainFrame.gameObjects[gameObjectId].components.Count < colliderId
                    && MainFrame.gameObjects[gameObjectId].components[colliderId] is Collider)
                    MainFrame.gameObjects[gameObjectId].components[colliderId] = value;
            }
        }
        public abstract void AlgorithmInterface();
    }
}
