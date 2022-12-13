using BlazorGame.Game.GameComponents.RendersDecorum.Decorator;
using BlazorGame.Game.GameComponents.RendersDecorum;
using BlazorGame.Game.Builder;
using BlazorGame.Game.GameComponents.Units;

namespace BlazorGame.Game.GameObjects
{
    public class MobObject : CollectibleObject
    {
        public override void Create()
        {
            base.objectType = ObjectType.Mob;
            base.Mass = 1f;
            base.Deacceleration = CollectiblesDecceleration;

            Renders renders = new Renders();

            renders.renders.Add(new CircleRender(new int[] { 0, 0 }, 20, "lime"));


            BoxRender healthBorder = new BoxRender(new int[] { -15, 5 }, new int[] { 30, 10 }, "grey");
            BoxRender healthBackGround = new BoxRender(new int[] { -14, 6 }, new int[] { 28, 8 }, "black");
            FillerSpaceDecorator fillerSpaceDecorator = new FillerSpaceDecorator(healthBackGround, healthBorder);


            BoxRender health = new BoxRender(new int[] { -14, 6 }, new int[] { 28, 8 }, "red");
            DecoratorRender healthDecorator = new HealthDecorator(health, fillerSpaceDecorator);
            renders.renders.Add(healthDecorator);

            AddComponent(renders);
        }

        public MobObject Clone()
        {
            MobBuilder collectibleBuilder = new MobBuilder(new float[] { 0, 0 }, new float[] { 0,0 });
            Director.GetInstance().Construct(ref collectibleBuilder, 1);
            return collectibleBuilder.GetResult() as MobObject;
        }
    }
}
