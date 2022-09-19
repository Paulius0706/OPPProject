using System.Numerics;
using Blazor.Extensions.Canvas;
using Blazor.Extensions.Canvas.Canvas2D;

namespace BlazorGame.Game
{
    public class Collider : ObjectComponent
    {
        public int gameObject; // gameobject attached to collider
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

        public override void Render(int playerId, ref Canvas2DContext context)
        {
            // Debug colliders (if it is commented then debug disabled)
            //context.SetFillStyleAsync("green");
            //context.FillRectAsync((int)MainFrame.gameObjects[gameObject].position[0] + offset[0], (int)MainFrame.gameObjects[gameObject].position[1] + offset[0], 100, 100);

        }
        public override void Update() { }
    }
}
