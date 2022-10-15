using BlazorGame.Game.GameComponents.Colliders;
using BlazorGame.Game.GameObjects;


namespace BlazorGame.Game.GameComponents.Colliders
{
    public abstract class ColliderStrategy
    {
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
                    || !MainFrame.gameObjects[gameObjectId].ContainsComponent<Collider>()) return null;
                return (MainFrame.gameObjects[gameObjectId].GetComponent<Collider>());
            }
            set
            {
                if (MainFrame.gameObjects.ContainsKey(gameObjectId)
                    && MainFrame.gameObjects[gameObjectId].ContainsComponent<Collider>())
                    collider = value;
            }
        }
        public abstract void AlgorithmInterface();
    }
}
