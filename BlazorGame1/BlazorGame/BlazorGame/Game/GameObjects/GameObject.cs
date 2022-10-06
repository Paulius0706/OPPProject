using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameComponents;
using BlazorGame.Game.GameComponents.Colliders;
using System.Formats.Asn1;
using System.Numerics;

namespace BlazorGame.Game.GameObjects
{
    public abstract class GameObject
    {
        public static readonly float CollectiblesDecceleration = 50f;
        public static readonly float BulletDecceleration = 10f;
        //LOGIC
        public int id { get; set; }
        public float[] position { get; set; }
        public float[] velocity { get; set; }
        public float mass { get; set; }
        public bool Unmoving { get; set; } // states if object can have velocity
        public bool ColliderDetector { get; set; }

        public List<ObjectComponent> components;
        //public Dictionary<Type, ObjectComponent> components;
        public List<Render> renders;

        public float deacceleration { get; set; }
        public ObjectType objectType;

        public enum ObjectType
        {
            undentified = 0,
            player = 1,
            collectible = 2,
            mob = 3,
            bullet = 4
        }
        
        
        


        public GameObject()
        {
            id = MainFrame.gameObjectsCounting;
            MainFrame.gameObjectsCounting++;
            position = new float[2];
            velocity = new float[2];
            components = new List<ObjectComponent>();
            renders = new List<Render>();
            objectType = ObjectType.undentified;
            mass = 1f;
            deacceleration = 0f;
            Create();
        }
        public abstract void Create();

        

        public void Update()
        {
            //needs collider
            UpdateVelocity();
            UpdateCollisions();
            position[0] += velocity[0] * MainFrame.detaTime;
            position[1] += velocity[1] * MainFrame.detaTime;
            foreach (ObjectComponent objectComponent in components)
            {
                objectComponent.Update();
            }


        }
        public void UpdateCollisions()
        {
            if (objectType == ObjectType.player)
            {
                components[(int)PlayerObject.PlayerComponents.collider].Update();
            }
        }
        public void UpdateVelocity()
        {
            // velocity change
            float x = 0f;
            float y = 0f;
            if (objectType == ObjectType.player)
            {
                //easy access to inputs
                float moveSpeed = (components[0] as Player).moveSpeed;
                float[] inputs = (components[0] as Player).inputs;

                // acceleration
                x = inputs[0] > 0f ? moveSpeed * MainFrame.detaTime : x;
                x = inputs[0] > 0f && velocity[0] > moveSpeed ? 0f : x;

                x = inputs[0] < 0f ? -moveSpeed * MainFrame.detaTime : x;
                x = inputs[0] < 0f && velocity[0] < -moveSpeed ? 0f : x;

                y = inputs[1] > 0f ? moveSpeed * MainFrame.detaTime : y;
                y = inputs[1] > 0f && velocity[1] > moveSpeed ? 0f : y;

                y = inputs[1] < 0f ? -moveSpeed * MainFrame.detaTime : y;
                y = inputs[1] < 0f && velocity[1] < -moveSpeed ? 0f : y;

                //deacceleration
                if (inputs[0] == 0)
                {
                    if (velocity[0] > 0) { x = velocity[0] > moveSpeed * MainFrame.detaTime ? -moveSpeed * MainFrame.detaTime : -velocity[0]; }
                    if (velocity[0] < 0) { x = velocity[0] < -moveSpeed * MainFrame.detaTime ? moveSpeed * MainFrame.detaTime : -velocity[0]; }
                }
                if (inputs[1] == 0)
                {
                    if (velocity[1] > 0) { y = velocity[1] > moveSpeed * MainFrame.detaTime ? -moveSpeed * MainFrame.detaTime : -velocity[1]; }
                    if (velocity[1] < 0) { y = velocity[1] < -moveSpeed * MainFrame.detaTime ? moveSpeed * MainFrame.detaTime : -velocity[1]; }
                }

                //collision
                components[(int)PlayerObject.PlayerComponents.collider].Update();
            }
            else
            {
                if (velocity[0] > 0) { x = velocity[0] > deacceleration * MainFrame.detaTime ? -deacceleration * MainFrame.detaTime : -velocity[0]; }
                if (velocity[0] < 0) { x = velocity[0] < -deacceleration * MainFrame.detaTime ? deacceleration * MainFrame.detaTime : -velocity[0]; }
                if (velocity[1] > 0) { y = velocity[1] > deacceleration * MainFrame.detaTime ? -deacceleration * MainFrame.detaTime : -velocity[1]; }
                if (velocity[1] < 0) { y = velocity[1] < -deacceleration * MainFrame.detaTime ? deacceleration * MainFrame.detaTime : -velocity[1]; }
            }

            //assing new velocity
            velocity[0] += x;
            velocity[1] += y;
        }

        public void ConnectionUpdate()
        {
            for(int i=0;i< components.Count; i++)
            {
                components[i].gameObject = this;
                components[i].id = i;
                components[i].ConnectionUpdate();
            }
        }
    }
}
