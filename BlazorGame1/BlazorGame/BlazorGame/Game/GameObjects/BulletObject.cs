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

            renders.renders.Add(new GameComponents.RendersDecorum.CircleRender(GameComponents.RendersDecorum.Render.Type.Body, new int[] { 0, 0 }, 15, "red"));

            components.Add(typeof(Renders), renders);
        }
    }
}
