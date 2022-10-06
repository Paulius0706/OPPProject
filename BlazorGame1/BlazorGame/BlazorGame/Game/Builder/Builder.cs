using BlazorGame.Game.GameComponents;
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
            gameObject.components.Add(collider);
            //ConnectionUpdate();
        }
        public void BuildPlayer(string name, int level)
        {
            Player player = new Player("", 1);
            gameObject.components.Add(player);
            //ConnectionUpdate();
        }
        public void BuildCannons()
        {
            Cannons cannons = new Cannons();
            gameObject.components.Add(cannons);
            //ConnectionUpdate();
        }
        public void BuildCollectible(Collectible.CollectibleType type)
        {
            Collectible collectible = new Collectible(type);
            gameObject.components.Add(collectible);
            //ConnectionUpdate();
        }
        public void BuildBullet(int shooter, float damage , float health)
        {
            Bullet bullet = new Bullet(damage, health, shooter);
            gameObject.components.Add(bullet);
            //ConnectionUpdate();
        }
        public void BuildRender(Render render)
        {
            gameObject.renders.Add(render);
        }
        public void ConnectionUpdate()
        {
            foreach(ObjectComponent objectComponent in gameObject.components)
            {
                objectComponent.ConnectionUpdate();
            }
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
