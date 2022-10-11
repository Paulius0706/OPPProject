﻿using BlazorGame.Game.GameComponents;
using BlazorGame.Game.GameComponents.Colliders;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.Builder
{
    public abstract class Builder
    {
        protected GameObject gameObject;
        public void BuildCicleCollider(float[] offset, float radius, bool trigger = false, bool active = false)
        {
            Collider collider= new Collider(offset,radius,trigger,active);
            gameObject.components.Add(typeof(Collider),collider);
            //ConnectionUpdate();
        }
        public void BuildPlayer(string name, int level)
        {
            Player player = new Player("", 1);
            gameObject.components.Add(typeof(Player), player);
            //ConnectionUpdate();
        }
        public void BuildCannons()
        {
            Cannons cannons = new Cannons();
            gameObject.components.Add(typeof(Cannons),cannons);
            //ConnectionUpdate();
        }
        public void BuildCollectible(Collectible.CollectibleType type)
        {
            Collectible collectible = new Collectible(type);
            gameObject.components.Add(typeof(Collectible),collectible);
            //ConnectionUpdate();
        }
        public void BuildBullet(int shooter, float damage , float health)
        {
            Bullet bullet = new Bullet(damage, health, shooter);
            gameObject.components.Add(typeof(Bullet),bullet);
            //ConnectionUpdate();
        }
        public GameObject GetResult()
        {
            return gameObject;
        }

        //public void BuildRenderBox(int[] offset, int[] size, Render.Type type, string str)
        //{
        //    gameObject.renders.Add(new Game.Render(offset, size, type, str));
        //}
        //public void BuildRenderCircle(int[] offset, int radius, string str)
        //{
        //    gameObject.renders.Add(new Game.Render(offset, radius, str));
        //}
        //public void BuildRenderLine(int[] offset, int[] offset1, int width, string str)
        //{
        //    gameObject.renders.Add(new Game.Render(offset, offset1, width, str));
        //}
    }
}