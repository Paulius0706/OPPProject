using BlazorGame.Game.GameComponents.Colliders;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents.Colliders
{
    public class CircleColliderStrategy : ColliderStrategy
    {
        public override void AlgorithmInterface()
        {
            foreach (GameObject gameObject in MainFrame.gameObjects.Values)
            {
                if (gameObject.id != this.gameObject.id
                    && (base.gameObject.position[0] < gameObject.position[0] + Collider.CollisionRange || base.gameObject.position[0] > gameObject.position[0] - Collider.CollisionRange)
                    && (base.gameObject.position[1] < gameObject.position[1] + Collider.CollisionRange || base.gameObject.position[1] > gameObject.position[1] - Collider.CollisionRange))
                {
                    if (gameObject.objectType == GameObject.ObjectType.player)
                    {
                        CollideCircleCircle(gameObject.id, (int)PlayerObject.PlayerComponents.collider);
                    }
                    if (gameObject.objectType == GameObject.ObjectType.collectible)
                    {
                        CollideCircleCircle(gameObject.id, (int)CollectibleObject.CollectibleComponents.collider);
                    }
                    if (gameObject.objectType == GameObject.ObjectType.mob)
                    {
                        CollideCircleCircle(gameObject.id, (int)CollectibleObject.CollectibleComponents.collider);
                    }
                }
            }
        }

        private void CollideCircleCircle(int gameObjectId, int colliderId)
        {
            throw new NotImplementedException();
        }
    }
}
