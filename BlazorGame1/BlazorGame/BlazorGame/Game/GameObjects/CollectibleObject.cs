using BlazorGame.Game.GameComponents.RendersDecorum;

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

            renders.renders.Add(new GameComponents.RendersDecorum.CircleRender(GameComponents.RendersDecorum.Render.Type.Body, new int[] { 0, 0 }, 20, "green"));

            DecoratorRender healthRender = new DecoratorRender(GameComponents.RendersDecorum.Render.Type.Health);
            healthRender.renders.Add(new GameComponents.RendersDecorum.BoxRender(GameComponents.RendersDecorum.Render.Type.GameUI, new int[] { -15, 5 }, new int[] { 30, 10 }, "grey"));
            healthRender.renders.Add(new GameComponents.RendersDecorum.BoxRender(GameComponents.RendersDecorum.Render.Type.Health, new int[] { -14, 6 }, new int[] { 28,  8 }, "red" ));
            renders.renders.Add(healthRender);

            components.Add(typeof(Renders), renders);
        }
    }
}
