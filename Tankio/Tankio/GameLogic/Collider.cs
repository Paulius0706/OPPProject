using System.Numerics;

namespace Tankio.GameLogic
{
    public class Collider : ObjectComponent
    {
        public GameObject gameObject; // gameobject attached to collider
        public enum ColliderType
        {
            box = 0,
            circle = 1
        }
        public bool trigger { get; set; } // trigger collider
        public ColliderType colliderType;
        public Vector2 size; // for box collider
        public float radius; // for circle collider

        public Collider(Vector2 size, bool trigger = false) { this.size = size; this.trigger = trigger; this.colliderType = ColliderType.box; }
        public Collider(float radius, bool trigger = false) { this.radius = radius; this.trigger = trigger; this.colliderType = ColliderType.box; }

        public bool isColliding(Collider otherCollider)
        {

            return false;
        }

        public override void Inputting(int x, int y) { }
        public override void Render() { }
        public override void Update() { }
    }
}
