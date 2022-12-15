using System.Numerics;
using Blazor.Extensions.Canvas;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameComponents.Colliders;
using BlazorGame.Game.GameComponents.Units.Visitor;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents.Colliders
{
    public class Collider : ObjectComponent
    {
        public static readonly float CollisionRange = 300f;
        
        public bool Trigger { get; set; } // trigger collider
        public ColliderType colliderType;
        public float[] Offset; // requred becouse collider is from the corner 
        public float[]? Size; // for box collider
        public float Radius; // for circle collider
        public ColliderStrategy ColliderStrategy;
        public bool IsActive { get; set; }

        // box
        public Collider(float[] offset, float[] size, bool trigger = false, bool active = false) { this.Offset = offset; this.Size = size; this.Trigger = trigger; colliderType = ColliderType.box; IsActive = active; ColliderStrategy = new BoxColliderStrategy(); }
        // circle
        public Collider(float[] offset, float radius, bool trigger = false, bool active = false) { this.Offset = offset; this.Radius = radius; this.Trigger = trigger; colliderType = ColliderType.circle; IsActive = active; ColliderStrategy = new CircleColliderStrategy(); }

        public override void ConnectionUpdate()
        {
            ColliderStrategy.gameObject = this.GameObject;
        }

        public override void CollisonTrigger(int gameObject)
        {

        }

        public override void Update()
        {
            if (IsActive)
            {
                ColliderStrategy.AlgorithmInterface();
            }
        }

        public override void accept(IVisitor visitor)
        {
            
        }

        public enum ColliderType
        {
            box = 0,
            circle = 1
        }
    }
}
