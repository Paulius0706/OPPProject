using BlazorGame.Game.Builder;
using BlazorGame.Game.GameComponents.Units;

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
            get { if (!MainFrame.GameObjects.ContainsKey(gameObjectId)) return null; return MainFrame.GameObjects[gameObjectId]; }
            set { gameObjectId = value.Id; if (MainFrame.GameObjects.ContainsKey(gameObjectId)) MainFrame.GameObjects[gameObjectId] = value; }
        }
        public Player player
        {
            get
            {
                if (!MainFrame.GameObjects.ContainsKey(gameObjectId)
                    || !MainFrame.GameObjects[gameObjectId].ContainsComponent<Player>()) return null;
                return (MainFrame.GameObjects[gameObjectId].GetComponent<Player>());
            }
            set
            {
                if (MainFrame.GameObjects.ContainsKey(gameObjectId)
                    && MainFrame.GameObjects[gameObjectId].ContainsComponent<Player>())
                    player = value;
            }
        }
    }
}
