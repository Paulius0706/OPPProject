using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameComponents;
using BlazorGame.Game.GameComponents.Colliders;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel;
using System.Formats.Asn1;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace BlazorGame.Game.GameObjects
{
    /// <summary>
    /// Creator abstract class
    /// </summary>
    public abstract class GameObject
    {
        public static readonly float CollectiblesDecceleration = 50f;
        public static readonly float BulletDecceleration = 10f;
        //LOGIC
        public int id { get; set; }
        public float[] position { get; set; }
        public float scale;
        public float[] velocity { get; set; }
        public float mass { get; set; }
        public bool Unmoving { get; set; } // states if object can have velocity
        public bool ColliderDetector { get; set; }

        private Dictionary<Type, ObjectComponent> components;


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
            //id = MainFrame.gameObjectsCounting;
            //MainFrame.gameObjectsCounting++;
            id = -1;
            position = new float[2];
            velocity = new float[2];
            components = new Dictionary<Type, ObjectComponent>();
            objectType = ObjectType.undentified;
            mass = 1f;
            scale = 1f;
            deacceleration = 0f;
            Create();
        }
        public abstract void Create();


        public void Update()
        {
            UpdateVelocity();
            foreach (ObjectComponent objectComponent in components.Values)
            {
                objectComponent.Update();
            }
            position[0] += velocity[0] * MainFrame.detaTime;
            position[1] += velocity[1] * MainFrame.detaTime;


        }
        public void UpdateVelocity()
        {
            // velocity change
            float x = 0f;
            float y = 0f;
            if (components.ContainsKey(typeof(Player)))
            {
                //easy access to inputs
                float moveSpeed = (components[typeof(Player)] as Player).moveSpeed;
                float[] inputs = (components[typeof(Player)] as Player).inputs;

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
            foreach (ObjectComponent objectComponent in components.Values)
            {
                objectComponent.gameObject = this;
                objectComponent.ConnectionUpdate();
            }
        }
        public void CollisionTrigger(int gameObject)
        {
             
            foreach (ObjectComponent objectComponent in components.Values)
            {
                objectComponent.CollisonTrigger(gameObject);
            }
        }


        public T GetComponent<T>() where T : ObjectComponent
        {
            return (T)components[typeof(T)];
        }
        public T AbstractGetComponent<T>() where T : ObjectComponent
        {
            foreach (Type key in components.Keys)
            {
                if (components[key].GetType().IsSubclassOf(typeof(T))) { return (T)components[key]; }
            }
            return null;
        }
        public Type GetComponentType<T>() where T : ObjectComponent
        {
            foreach (Type key in components.Keys)
            {
                if (components[key].GetType().IsSubclassOf(typeof(T))) { return components[key].GetType(); }
            }
            return null;
        }
        public void AddComponent<Type>(Type component) where Type : ObjectComponent
        {
            components.Add(typeof(Type),component);
        }
        public bool ContainsComponent<Type>() where Type : ObjectComponent
        {
            return components.ContainsKey(typeof(Type));
        }
        public bool AbstarctContainsComponent<Type>() where Type : ObjectComponent
        {
            foreach(ObjectComponent component in components.Values)
            {
                if (component.GetType().IsSubclassOf(typeof(Type))) { return true; }
            }
            return false;
        }
        
    }
}
