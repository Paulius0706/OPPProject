using BlazorGame.Game.Builder;
using BlazorGame.Game.GameComponents.RendersDecorum;
using BlazorGame.Game.GameComponents.RendersDecorum.FlyWeight;
using BlazorGame.Game.GameComponents.Units;
using static System.Net.Mime.MediaTypeNames;

namespace BlazorGame.Game.GameObjects
{
    public class BulletObject : GameObject
    {
        public override void Create()
        {
            base.objectType = ObjectType.Bullet;
            base.Mass = 1f;
            base.Deacceleration = BulletDecceleration;

            Renders renders = new Renders();
            renders.renders.Add(new LinkRender("bulletNormal"));
            //renders.renders.Add(new CircleRender(new int[] { 0, 0 }, 15, "red"));

            AddComponent(renders);
        }

        public void Mutate()
        {

        }

        public BulletObject Clone()
        {
            BulletBuilder bulletBuilder = new BulletBuilder(Position, Velocity);
            //Console.WriteLine("BulletClone: shooter" + GetComponent<Bullet>().shooter);
            Director.GetInstance().Construct(ref bulletBuilder,
                GetComponent<Bullet>().shooter,
                GetComponent<Bullet>().bodyDamage,
                GetComponent<Bullet>().health);
			return bulletBuilder.GetResult() as BulletObject;
        }
    }
}
