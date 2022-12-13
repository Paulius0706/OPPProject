using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents.RendersDecorum.FlyWeight
{
    public class LinkRender : Render
    {
        string name;

        public LinkRender(string name)
        {
            this.name = name;
        }

        public override void ConnectionUpdate(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public override void Show(ref Canvas2DContext context, int xoffset, int yoffset)
        {
            RenderLib.renders[name].gameObject = gameObject;
            RenderLib.renders[name].Show(ref context, xoffset, yoffset);
            //RenderLib.Get(name).Show(ref context, xoffset, yoffset);

        }
    }
}
