using BlazorGame.Game.Builder;
using BlazorGame.Game.GameComponents;

namespace BlazorGame.Game.GameObjects.Factories
{
    /// <summary>
    /// Player evolution factory (bullet and collectible act diffirently in evolutions)
    /// </summary>
    public abstract class Factory
    {
        public int stage = 1;
        // there will be diffirent bullets for diffirent evolved players
        public abstract BulletObject CreateBulletObject();

        // there will be diffirent chances of collectibles for diffirent evolved players
        public abstract CollectibleObject CreateCollectibleObject();

        private int gameObjectId = -1;
        public GameObject gameObject
        {
            get { if (!MainFrame.gameObjects.ContainsKey(gameObjectId)) return null; return MainFrame.gameObjects[gameObjectId]; }
            set { gameObjectId = value.id; if (MainFrame.gameObjects.ContainsKey(gameObjectId)) MainFrame.gameObjects[gameObjectId] = value; }
        }
        public Player player
        {
            get
            {
                if (!MainFrame.gameObjects.ContainsKey(gameObjectId)
                    || !MainFrame.gameObjects[gameObjectId].components.ContainsKey(typeof(Player))) return null;
                return (MainFrame.gameObjects[gameObjectId].components[typeof(Player)] as Player);
            }
            set
            {
                if (MainFrame.gameObjects.ContainsKey(gameObjectId)
                    && MainFrame.gameObjects[gameObjectId].components.ContainsKey(typeof(Player)))
                    MainFrame.gameObjects[gameObjectId].components[typeof(Player)] = value;
            }
        }
    }
}
