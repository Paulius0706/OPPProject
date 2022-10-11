using BlazorGame.Game.Builder;
using BlazorGame.Game.GameComponents;
using BlazorGame.Game.GameComponents.RendersDecorum;

namespace BlazorGame.Game.GameObjects
{
    public class PlayerObject : GameObject
    {
        public override void Create()
        {
            //conclrete Product
            base.objectType = ObjectType.player;
            base.mass = 10f;

            Renders renders = new Renders();
            
            DecoratorRender gunRender = new DecoratorRender(GameComponents.RendersDecorum.Render.Type.Guns);
            renders.renders.Add(gunRender);

            renders.renders.Add(new GameComponents.RendersDecorum.CircleRender(GameComponents.RendersDecorum.Render.Type.Body,new int[] { 0, 0}, 50, "blue"));
            
            DecoratorRender healthRender = new DecoratorRender(GameComponents.RendersDecorum.Render.Type.Health);
            healthRender.renders.Add(new GameComponents.RendersDecorum.BoxRender(GameComponents.RendersDecorum.Render.Type.GameUI, new int[] { -40, 28 }, new int[] { 80, 10 }, "grey"));
            healthRender.renders.Add(new GameComponents.RendersDecorum.BoxRender(GameComponents.RendersDecorum.Render.Type.Health, new int[] { -39, 29 }, new int[] { 78, 8 }, "red"));
            renders.renders.Add(healthRender);

            DecoratorRender expRender = new DecoratorRender(GameComponents.RendersDecorum.Render.Type.Exp);
            expRender.renders.Add(new GameComponents.RendersDecorum.BoxRender(GameComponents.RendersDecorum.Render.Type.GameUI, new int[] { -40, 38 }, new int[] { 80, 6 }, "grey"));
            expRender.renders.Add(new GameComponents.RendersDecorum.BoxRender(GameComponents.RendersDecorum.Render.Type.Exp, new int[] { -39, 39 }, new int[] { 78, 4 }, "yellow"));
            renders.renders.Add(expRender);

            components.Add(typeof(Renders), renders);
        }

        public void SetInputs(int x, int y)
        {
            (components[typeof(Player)] as Player).SetInputs(x, y);
        }
        public void SetCannons(int x, int y)
        {
            (components[typeof(Cannons)] as Cannons).SetCannons(x, y);
        }
        public void SetShooting(bool shooting)
        {
            (components[typeof(Cannons)] as Cannons).SetShooting(shooting);
        }
    }
}
