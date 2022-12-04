using BlazorGame.Game.GameComponents.Colliders;
using BlazorGame.Game.GameObjects;


namespace BlazorGame.Game.GameComponents.Colliders
{
    public abstract class ColliderStrategy
    {
        private int gameObjectId = -1;
        public GameObject gameObject
        {
            get{ if (!MainFrame.GameObjects.ContainsKey(gameObjectId)) return null; return MainFrame.GameObjects[gameObjectId]; }
            set{ gameObjectId = value.Id; if (MainFrame.GameObjects.ContainsKey(gameObjectId)) MainFrame.GameObjects[gameObjectId] = value; }
        }
        public Collider collider
        {
            get
            {
                if (!MainFrame.GameObjects.ContainsKey(gameObjectId) 
                    || !MainFrame.GameObjects[gameObjectId].ContainsComponent<Collider>()) return null;
                return (MainFrame.GameObjects[gameObjectId].GetComponent<Collider>());
            }
            set
            {
                if (MainFrame.GameObjects.ContainsKey(gameObjectId)
                    && MainFrame.GameObjects[gameObjectId].ContainsComponent<Collider>())
                    collider = value;
            }
        }
        public abstract void AlgorithmInterface();
    }
}
