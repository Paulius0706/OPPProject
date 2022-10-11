﻿using Blazor.Extensions.Canvas.Canvas2D;
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
        public float[] velocity { get; set; }
        public float mass { get; set; }
        public bool Unmoving { get; set; } // states if object can have velocity
        public bool ColliderDetector { get; set; }

        //public List<ObjectComponent> components;
        public Dictionary<Type, ObjectComponent> components;

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
            components = new Dictionary<Type, ObjectComponent>();
            objectType = ObjectType.undentified;
            mass = 1f;
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
        public void CollisionTrigger(string data, int number)
        {
            foreach (ObjectComponent objectComponent in components.Values)
            {
                objectComponent.CollisonTrigger(id,data, number);
            }
        }
    }
}