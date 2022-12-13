using BlazorGame.Game.GameComponents;
using BlazorGame.Game.GameComponents.Colliders;
using BlazorGame.Game.GameComponents.Units;
using BlazorGame.Game.GameComponents.Units.Mobs;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.Builder
{
    public abstract class Builder
    {
        protected GameObject gameObject = new CollectibleObject();

        public void BuildCicleCollider(float[] offset, float radius, bool trigger = false, bool active = false)
        {
            Collider collider= new Collider(offset,radius,trigger,active);
            gameObject.AddComponent(collider);
            //ConnectionUpdate();
        }
        public void BuildPlayer(string name, int level)
        {
            Player player = new Player(name, level);
            gameObject.AddComponent(player);
            //ConnectionUpdate();
        }
        public void BuildCannons()
        {
            Cannons cannons = new Cannons();
            gameObject.AddComponent(cannons);
            //ConnectionUpdate();
        }
        public void BuildCollectible(Collectible.CollectibleType type)
        {
            Collectible collectible = new Collectible(type);
            gameObject.AddComponent(collectible);
            //ConnectionUpdate();
        }
        public void BuildBullet(int shooter, float damage , float health)
        {
            Bullet bullet = new Bullet(damage, health, shooter);
            gameObject.AddComponent(bullet);
            //ConnectionUpdate();
        }
        public void BuildMob()
        {
            Mob head = new Mob();
            gameObject.AddComponent(head);
            //ConnectionUpdate();
        }
        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
