using System.ComponentModel;
using System.Numerics;

namespace Tankio.GameLogic
{
    public class GameObject
    {
        public static int idCounter = 0;
        public int id { get; set; }
        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }
        public bool Unmoving { get; set; } // states if object can have velocity
        public bool ColliderDetector { get; set; }

        public List<ObjectComponent> components;

        public GameObject(Vector2 positions)
        {
            id = idCounter;
            idCounter++;
            this.position = position;
            this.components = new List<ObjectComponent>();
        }

        public void AddComponent(ObjectComponent objectComponent)
        {
            objectComponent.gameObject = id;
            components.Add(objectComponent);
        }

        public void Update()
        {
            //needs collider
            position += velocity;
        }
        public void Render()
        {

        }
        
    }
}
