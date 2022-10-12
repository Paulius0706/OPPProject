using System.Numerics;
using Blazor.Extensions.Canvas;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameComponents.Colliders;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents.Colliders
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
        public ColliderStrategy colliderStrategy;
        public bool isActive { get; set; }

        // box
        public Collider(float[] offset, float[] size, bool trigger = false, bool active = false) { this.offset = offset; this.size = size; this.trigger = trigger; colliderType = ColliderType.box; isActive = active; colliderStrategy = new BoxColliderStrategy(); }
        // circle
        public Collider(float[] offset, float radius, bool trigger = false, bool active = false) { this.offset = offset; this.radius = radius; this.trigger = trigger; colliderType = ColliderType.circle; isActive = active; colliderStrategy = new CircleColliderStrategy(); }

        public override void ConnectionUpdate()
        {
            colliderStrategy.gameObject = this.gameObject;
        }
        public override void CollisonTrigger(int gameObject)
        {

        }
        public override void Update()
        {
            if (isActive)
            {
                colliderStrategy.AlgorithmInterface();
            }
        }
    }
}
