// Copyright (c) LAB 4 KTU. All rights reserved.

using System.ComponentModel;
using System.Formats.Asn1;
using System.Numerics;
using System.Runtime.CompilerServices;
using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameComponents;
using BlazorGame.Game.GameComponents.Colliders;
using BlazorGame.Game.GameComponents.Units;
using BlazorGame.Game.GameComponents.Units.Visitor;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace BlazorGame.Game.GameObjects
{
    /// <summary>
    /// Creator abstract class
    /// </summary>
    public abstract class GameObject : Visitor
    {
        public static readonly float CollectiblesDecceleration = 50f;
        public static readonly float BulletDecceleration = 10f;
        
        public float Scale;
        public ObjectType objectType;
        private Dictionary<Type, ObjectComponent> components;
        public float[] Velocity { get; set; }
        public float Mass { get; set; }
        public bool Unmoving { get; set; } // states if object can have velocity
        public bool ColliderDetector { get; set; }
        public int Id { get; set; }
        public float[] Position { get; set; }
        public float Deacceleration { get; set; }
        
        public GameObject()
        {
            //id = MainFrame.gameObjectsCounting;
            //MainFrame.gameObjectsCounting++;
            this.Id = -1;
            this.Position = new float[2];
            this.Velocity = new float[2];
            this.components = new Dictionary<Type, ObjectComponent>();
            this.objectType = ObjectType.Undentified;
            this.Mass = 1f;
            this.Scale = 1f;
            this.Deacceleration = 0f;
            this.Create();
        }

        public enum ObjectType
        {
            Undentified = 0,
            Player = 1,
            Collectible = 2,
            Mob = 3,
            Bullet = 4,
        }
        public abstract void Create();


        public void Update()
        {
            this.UpdateVelocity();
            foreach (ObjectComponent objectComponent in this.components.Values)
            {
                objectComponent.Update();
            }

            this.Position[0] += this.Velocity[0] * MainFrame.DeltaTime;
            this.Position[1] += this.Velocity[1] * MainFrame.DeltaTime;
        }

        public void UpdateVelocity()
        {
            // velocity change
            float x = 0f;
            float y = 0f;
            if (this.components.ContainsKey(typeof(Player)))
            {
                // easy access to inputs
                float moveSpeed = (this.components[typeof(Player)] as Player).moveSpeed;
                float[] inputs = (this.components[typeof(Player)] as Player).inputs;

                // acceleration
                x = inputs[0] > 0f ? moveSpeed * MainFrame.DeltaTime : x;
                x = inputs[0] > 0f && this.Velocity[0] > moveSpeed ? 0f : x;

                x = inputs[0] < 0f ? -moveSpeed * MainFrame.DeltaTime : x;
                x = inputs[0] < 0f && this.Velocity[0] < -moveSpeed ? 0f : x;

                y = inputs[1] > 0f ? moveSpeed * MainFrame.DeltaTime : y;
                y = inputs[1] > 0f && this.Velocity[1] > moveSpeed ? 0f : y;

                y = inputs[1] < 0f ? -moveSpeed * MainFrame.DeltaTime : y;
                y = inputs[1] < 0f && this.Velocity[1] < -moveSpeed ? 0f : y;

                // deacceleration
                if (inputs[0] == 0)
                {
                    if (Velocity[0] > 0) { x = Velocity[0] > moveSpeed * MainFrame.DeltaTime ? -moveSpeed * MainFrame.DeltaTime : -Velocity[0]; }
                    if (Velocity[0] < 0) { x = Velocity[0] < -moveSpeed * MainFrame.DeltaTime ? moveSpeed * MainFrame.DeltaTime : -Velocity[0]; }
                }
                if (inputs[1] == 0)
                {
                    if (Velocity[1] > 0) { y = Velocity[1] > moveSpeed * MainFrame.DeltaTime ? -moveSpeed * MainFrame.DeltaTime : -Velocity[1]; }
                    if (Velocity[1] < 0) { y = Velocity[1] < -moveSpeed * MainFrame.DeltaTime ? moveSpeed * MainFrame.DeltaTime : -Velocity[1]; }
                }

            }
            else
            {
                if (Velocity[0] > 0) { x = Velocity[0] > Deacceleration * MainFrame.DeltaTime ? -Deacceleration * MainFrame.DeltaTime : -Velocity[0]; }
                if (Velocity[0] < 0) { x = Velocity[0] < -Deacceleration * MainFrame.DeltaTime ? Deacceleration * MainFrame.DeltaTime : -Velocity[0]; }
                if (Velocity[1] > 0) { y = Velocity[1] > Deacceleration * MainFrame.DeltaTime ? -Deacceleration * MainFrame.DeltaTime : -Velocity[1]; }
                if (Velocity[1] < 0) { y = Velocity[1] < -Deacceleration * MainFrame.DeltaTime ? Deacceleration * MainFrame.DeltaTime : -Velocity[1]; }
            }

            //assing new velocity
            Velocity[0] += x;
            Velocity[1] += y;
        }

        public void ConnectionUpdate()
        {
            foreach (ObjectComponent objectComponent in components.Values)
            {
                objectComponent.GameObject = this;
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

        public void visitBullet(Bullet bullet)
        {
            components.Add(typeof(Bullet), bullet);
            //throw new NotImplementedException();
        }

        public void visitCollectible(Collectible collectible)
        {
            components.Add(typeof(Collectible), collectible);
        }

        public void visitPlayer(Player player)
        {
            components.Add(typeof(Player), player);
        }
    }
}
