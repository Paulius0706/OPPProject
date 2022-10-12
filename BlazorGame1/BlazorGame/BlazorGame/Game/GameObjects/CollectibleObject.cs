using BlazorGame.Game.Builder;
using BlazorGame.Game.GameComponents;
using BlazorGame.Game.GameComponents.RendersDecorum;
using BlazorGame.Game.GameComponents.RendersDecorum.Decorator;

namespace BlazorGame.Game.GameObjects
{
    public class CollectibleObject : GameObject
    {
        public override void Create()
        {
            base.objectType = ObjectType.collectible;
            base.mass = 1f;
            base.deacceleration = CollectiblesDecceleration;

            Renders renders = new Renders();

            renders.renders.Add(new CircleRender(new int[] { 0, 0 }, 20, "green"));

            BoxRender health = new BoxRender(new int[] { -14, 6 }, new int[] { 28, 8 }, "red");
            BoxRender healthBackGround = new BoxRender(new int[] { -15, 5 }, new int[] { 30, 10 }, "grey");
            DecoratorRender healthDecorator = new HealthDecorator(health,healthBackGround);
            renders.renders.Add(healthDecorator);

            components.Add(typeof(Renders), renders);
        }
        public void Mutate()
        {

        }

        public CollectibleObject Clone()
        {
            CollectibleBuilder collectibleBuilder = new CollectibleBuilder(position);
            Director.director.Construct(ref collectibleBuilder, (components[typeof(Collectible)] as Collectible).type);
            return collectibleBuilder.GetResult() as CollectibleObject;
        }
    }
}
