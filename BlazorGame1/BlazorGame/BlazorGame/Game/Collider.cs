using System.Numerics;
using Blazor.Extensions.Canvas;
using Blazor.Extensions.Canvas.Canvas2D;

namespace BlazorGame.Game
{
    public class Collider : ObjectComponent
    {
        public static readonly float CollisionRange = 300f;
        public enum ColliderType
        {
            box = 0,
            circle = 1
        }
        public bool trigger { get; set; } // trigger collider
        public ColliderType colliderType;
        public float[] offset; // requred becouse collider is from the corner 
        public float[] size; // for box collider
        public float radius; // for circle collider
        public bool isActive { get; set; }

        public Collider(float[] offset, float[] size, bool trigger = false, bool active = false) { this.offset = offset; this.size = size; this.trigger = trigger; this.colliderType = ColliderType.box; this.isActive = active; }
        public Collider(float[] offset, float radius, bool trigger = false, bool active = false) { this.offset = offset; this.radius = radius; this.trigger = trigger; this.colliderType = ColliderType.circle; this.isActive = active; }

        public bool isColliding(Collider otherCollider)
        {

            return false;
        }
        public override void CollisonTrigger(int gameObject, string data, int number)
        {

        }
        public override void Update() 
        {
            if (isActive)
            {
                float[] position = MainFrame.gameObjects[gameObject].position;
                if (colliderType == ColliderType.circle)
                {
                    foreach (GameObject gameObject in MainFrame.gameObjects.Values)
                    {
                        if (gameObject.id != this.gameObject 
                            && (position[0] < gameObject.position[0] + CollisionRange || position[0] > gameObject.position[0] - CollisionRange)
                            && (position[1] < gameObject.position[1] + CollisionRange || position[1] > gameObject.position[1] - CollisionRange))
                        {
                            if (gameObject.objectType == GameObject.ObjectType.player)
                            {
                                CollideCircleCircle(gameObject.id, (int)GameObject.PlayerComponents.collider);
                            }
                            if (gameObject.objectType == GameObject.ObjectType.collectible)
                            {
                                CollideCircleCircle(gameObject.id, (int)GameObject.CollectibleComponents.collider);
                            }
                            if (gameObject.objectType == GameObject.ObjectType.mob)
                            {
                                CollideCircleCircle(gameObject.id, (int)GameObject.CollectibleComponents.collider);
                            }
                        }
                    }
                }
                if (colliderType == ColliderType.box)
                {
                    foreach (GameObject gameObject in MainFrame.gameObjects.Values)
                    {
                        if (gameObject.id != this.gameObject
                            && (position[0] < gameObject.position[0] + CollisionRange || position[0] > gameObject.position[0] - CollisionRange)
                            && (position[1] < gameObject.position[1] + CollisionRange || position[1] > gameObject.position[1] - CollisionRange))
                        {
                            if (gameObject.objectType == GameObject.ObjectType.player)
                            {
                                // need stuff
                            }
                            if (gameObject.objectType == GameObject.ObjectType.collectible)
                            {
                                //need stuff
                            }
                            if (gameObject.objectType == GameObject.ObjectType.mob)
                            {
                                //need stuff
                            }
                        }
                    }
                }
            }
            
        }
        public void CollideCircleCircle(int gameObject, int component)
        {
            float x = (MainFrame.gameObjects[gameObject].components[component] as Collider).offset[0] + MainFrame.gameObjects[     gameObject].position[0] - 
                      (                                                                     offset[0] + MainFrame.gameObjects[base.gameObject].position[0]);
            float y = (MainFrame.gameObjects[gameObject].components[component] as Collider).offset[1] + MainFrame.gameObjects[     gameObject].position[1] -
                      (                                                                     offset[1] + MainFrame.gameObjects[base.gameObject].position[1]);
            float totalRadius = (MainFrame.gameObjects[gameObject].components[component] as Collider).radius + radius;
            if (totalRadius * totalRadius > x*x+y*y)
            {
                // fix circles positions
                float totalDistance = MathF.Sqrt(x * x + y * y);
                float dirx = x / totalDistance;
                float diry = y / totalDistance;
                MainFrame.gameObjects[base.gameObject].position[0] -= (totalRadius - totalDistance) * dirx * 1.01f;
                MainFrame.gameObjects[base.gameObject].position[1] -= (totalRadius - totalDistance) * diry * 1.01f;
                MainFrame.gameObjects[gameObject].position[0] += (totalRadius - totalDistance) * dirx * 1.01f;
                MainFrame.gameObjects[gameObject].position[1] += (totalRadius - totalDistance) * diry * 1.01f;

                // change circles velocities
                float[] velocity1 = MainFrame.gameObjects[base.gameObject].velocity;
                float[] velocity2 = MainFrame.gameObjects[gameObject].velocity;
                float[] velocity = new float[] { velocity2[0] - velocity1[0], velocity2[1] - velocity1[1] };
                float totalVelocity = MathF.Sqrt(velocity[0] * velocity[0] + velocity[1] * velocity[1]);
                float mass1 = MainFrame.gameObjects[base.gameObject].mass;
                float mass2 = MainFrame.gameObjects[gameObject].mass;
                MainFrame.gameObjects[base.gameObject].velocity[0] -= (totalVelocity) * dirx * (mass2 / (mass1 + mass2));
                MainFrame.gameObjects[base.gameObject].velocity[1] -= (totalVelocity) * diry * (mass2 / (mass1 + mass2));
                MainFrame.gameObjects[gameObject].velocity[0] += (totalVelocity) * dirx * (mass1 / (mass1 + mass2));
                MainFrame.gameObjects[gameObject].velocity[1] += (totalVelocity) * diry * (mass1 / (mass1 + mass2));
            }
        }
        public void CollideCircleBox()
        {

        }
        public void CollideBoxCircle()
        {

        }
        public void CollideBoxBox()
        {

        }
    }
}
