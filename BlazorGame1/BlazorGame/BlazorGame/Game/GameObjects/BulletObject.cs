using BlazorGame.Game.Builder;
using BlazorGame.Game.GameComponents;
using BlazorGame.Game.GameComponents.RendersDecorum;
using static System.Net.Mime.MediaTypeNames;

namespace BlazorGame.Game.GameObjects
{
    public class BulletObject : GameObject
    {
        public override void Create()
        {
            base.objectType = ObjectType.bullet;
            base.mass = 1f;
            base.deacceleration = BulletDecceleration;

            Renders renders = new Renders();

            renders.renders.Add(new CircleRender(new int[] { 0, 0 }, 15, "red"));

            components.Add(typeof(Renders), renders);
        }

        public void Mutate()
        {

        }

        public BulletObject Clone()
        {
            BulletBuilder bulletBuilder = new BulletBuilder(position, velocity);
            Director.director.Construct(ref bulletBuilder, 
                (components[typeof(Bullet)] as Bullet).shooter, 
                (components[typeof(Bullet)] as Bullet).damage, 
                (components[typeof(Bullet)] as Bullet).health);
            return bulletBuilder.GetResult() as BulletObject;
        }
    }
}
