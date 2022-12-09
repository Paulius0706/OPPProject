using BlazorGame.Game.GameComponents.Colliders;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents.Colliders
{
    public class CircleColliderStrategy : ColliderStrategy
    {
        public override void AlgorithmInterface()
        {
            foreach (GameObject gameObject in MainFrame.GameObjects.Values)
            {
                if (gameObject.Id != this.gameObject.Id
                    && (base.gameObject.Position[0] < gameObject.Position[0] + Collider.CollisionRange || base.gameObject.Position[0] > gameObject.Position[0] - Collider.CollisionRange)
                    && (base.gameObject.Position[1] < gameObject.Position[1] + Collider.CollisionRange || base.gameObject.Position[1] > gameObject.Position[1] - Collider.CollisionRange))
                {
                    if (gameObject is PlayerObject)
                    {
                        CollideCircleCircle(gameObject.Id);
                    }
                    if (gameObject is CollectibleObject)
                    {
                        CollideCircleCircle(gameObject.Id);
                    }
                    if (gameObject is BulletObject)
                    {
                        CollideCircleCircle(gameObject.Id);
                    }
                    if (gameObject.objectType == GameObject.ObjectType.Mob)
                    {
                        CollideCircleCircle(gameObject.Id);
                    }
                }
            }
        }

        private void CollideCircleCircle(int gameObjectId)
        {
            float x = MainFrame.GameObjects[gameObjectId].GetComponent<Collider>().Offset[0] + MainFrame.GameObjects[gameObjectId].Position[0] -
                      (collider.Offset[0] + base.gameObject.Position[0]);
            float y = MainFrame.GameObjects[gameObjectId].GetComponent<Collider>().Offset[1] + MainFrame.GameObjects[gameObjectId].Position[1] -
                      (collider.Offset[1] + base.gameObject.Position[1]);

            float totalRadius = MainFrame.GameObjects[gameObjectId].GetComponent<Collider>().Radius + collider.Radius;
            if (totalRadius * totalRadius > x * x + y * y)
            {
                // Circles are totaly collide
                MainFrame.GameObjects[gameObjectId].CollisionTrigger(gameObject.Id);
                base.gameObject.CollisionTrigger(gameObjectId);

                if (base.gameObject is BulletObject && MainFrame.GameObjects[gameObjectId] is BulletObject) return;
                // fix circles positions
                float totalDistance = MathF.Sqrt(x * x + y * y);
                float dirx = x / totalDistance;
                float diry = y / totalDistance;
                base.gameObject.Position[0] -= (totalRadius - totalDistance) * dirx * 1.01f;
                base.gameObject.Position[1] -= (totalRadius - totalDistance) * diry * 1.01f;

                MainFrame.GameObjects[gameObjectId].Position[0] += (totalRadius - totalDistance) * dirx * 1.01f;
                MainFrame.GameObjects[gameObjectId].Position[1] += (totalRadius - totalDistance) * diry * 1.01f;

                // change circles velocities
                float[] velocity1 = base.gameObject.Velocity;
                float[] velocity2 = MainFrame.GameObjects[gameObjectId].Velocity;

                float[] velocity = new float[] { velocity2[0] - velocity1[0], velocity2[1] - velocity1[1] };
                float totalVelocity = MathF.Sqrt(velocity[0] * velocity[0] + velocity[1] * velocity[1]);

                float mass1 = base.gameObject.Mass;
                float mass2 = MainFrame.GameObjects[gameObjectId].Mass;

                base.gameObject.Velocity[0] -= totalVelocity * dirx * (mass2 / (mass1 + mass2))*2;
                base.gameObject.Velocity[1] -= totalVelocity * diry * (mass2 / (mass1 + mass2))*2;
                MainFrame.GameObjects[gameObjectId].Velocity[0] += totalVelocity * dirx * (mass1 / (mass1 + mass2))*2;
                MainFrame.GameObjects[gameObjectId].Velocity[1] += totalVelocity * diry * (mass1 / (mass1 + mass2))*2;
            }
        }
    }
}
