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

            AddComponent(renders);
        }

        public void Mutate()
        {

        }

        public BulletObject Clone()
        {
            BulletBuilder bulletBuilder = new BulletBuilder(position, velocity);
            //Console.WriteLine("BulletClone: shooter" + GetComponent<Bullet>().shooter);
            Director.Construct(ref bulletBuilder,
                GetComponent<Bullet>().shooter,
                GetComponent<Bullet>().bodyDamage,
                GetComponent<Bullet>().health);
			return bulletBuilder.GetResult() as BulletObject;
        }
    }
}
