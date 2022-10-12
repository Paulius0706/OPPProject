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
                    if (gameObject is PlayerObject)
                    {
                        CollideCircleCircle(gameObject.id);
                    }
                    if (gameObject is CollectibleObject)
                    {
                        CollideCircleCircle(gameObject.id);
                    }
                    if (gameObject.objectType == GameObject.ObjectType.mob)
                    {
                        CollideCircleCircle(gameObject.id);
                    }
                }
            }
        }

        private void CollideCircleCircle(int gameObjectId)
        {
            float x = (MainFrame.gameObjects[gameObjectId].components[typeof(Collider)] as Collider).offset[0] + MainFrame.gameObjects[gameObjectId].position[0] -
                      (collider.offset[0] + base.gameObject.position[0]);
            float y = (MainFrame.gameObjects[gameObjectId].components[typeof(Collider)] as Collider).offset[1] + MainFrame.gameObjects[gameObjectId].position[1] -
                      (collider.offset[1] + base.gameObject.position[1]);

            float totalRadius = (MainFrame.gameObjects[gameObjectId].components[typeof(Collider)] as Collider).radius + collider.radius;
            if (totalRadius * totalRadius > x * x + y * y)
            {
                // Circles are totaly collide
                MainFrame.gameObjects[gameObjectId].CollisionTrigger(gameObject.id);
                base.gameObject.CollisionTrigger(gameObjectId);


                // fix circles positions
                float totalDistance = MathF.Sqrt(x * x + y * y);
                float dirx = x / totalDistance;
                float diry = y / totalDistance;
                base.gameObject.position[0] -= (totalRadius - totalDistance) * dirx * 1.01f;
                base.gameObject.position[1] -= (totalRadius - totalDistance) * diry * 1.01f;

                MainFrame.gameObjects[gameObjectId].position[0] += (totalRadius - totalDistance) * dirx * 1.01f;
                MainFrame.gameObjects[gameObjectId].position[1] += (totalRadius - totalDistance) * diry * 1.01f;

                // change circles velocities
                float[] velocity1 = base.gameObject.velocity;
                float[] velocity2 = MainFrame.gameObjects[gameObjectId].velocity;

                float[] velocity = new float[] { velocity2[0] - velocity1[0], velocity2[1] - velocity1[1] };
                float totalVelocity = MathF.Sqrt(velocity[0] * velocity[0] + velocity[1] * velocity[1]);

                float mass1 = base.gameObject.mass;
                float mass2 = MainFrame.gameObjects[gameObjectId].mass;

                base.gameObject.velocity[0] -= totalVelocity * dirx * (mass2 / (mass1 + mass2));
                base.gameObject.velocity[1] -= totalVelocity * diry * (mass2 / (mass1 + mass2));
                MainFrame.gameObjects[gameObjectId].velocity[0] += totalVelocity * dirx * (mass1 / (mass1 + mass2));
                MainFrame.gameObjects[gameObjectId].velocity[1] += totalVelocity * diry * (mass1 / (mass1 + mass2));
            }
        }
    }
}
