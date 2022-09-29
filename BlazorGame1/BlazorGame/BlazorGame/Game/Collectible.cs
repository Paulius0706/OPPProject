using Blazor.Extensions.Canvas.Canvas2D;

namespace BlazorGame.Game
{
    public class Collectible : ObjectComponent
    {
        public int exp;
        public int health;
        public enum CollectibleType
        {
            col1,
            col2,
            col3,
            mob
        }

        public Collectible(CollectibleType collectibleType)
        {
            switch (collectibleType)
            {
                case CollectibleType.col1:
                    exp = 2;
                    health = 2;
                    break;
                case CollectibleType.col2:
                    exp = 8;
                    health = 6;
                    break;
                case CollectibleType.col3:
                    exp = 32;
                    health = 18;
                    break;
            }
        }
        public override void Update()
        {
            bool delete = true;
            foreach(GameObject gameObject in MainFrame.gameObjects.Values)
            {
                if(gameObject.objectType == GameObject.ObjectType.player 
                    && MathF.Abs(gameObject.position[0] - MainFrame.gameObjects[this.gameObject].position[0]) < Player.DespawnCollectiblesDist
                    && Math.Abs(gameObject.position[1] - MainFrame.gameObjects[this.gameObject].position[1]) < Player.DespawnCollectiblesDist)
                {
                    delete = false;
                    break;
                }
            }
            if (delete) MainFrame.destroyGameObjectsQueue.Enqueue(gameObject);

            if(health <= 0)
            {
                MainFrame.destroyGameObjectsQueue.Enqueue(gameObject);
            }
        }
        public override void CollisonTrigger(int gameObject, string data, int number)
        {

        }
    }
}
