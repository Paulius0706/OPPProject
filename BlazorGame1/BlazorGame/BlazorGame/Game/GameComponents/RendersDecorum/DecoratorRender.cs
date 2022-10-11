using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents.RendersDecorum
{
    public class DecoratorRender : Render
    {
        public List<Render> renders;

        public DecoratorRender(Type type)
        {
            this.type = type;
            this.renders = new List<Render>();
        }

        public override void ConnectionUpdate(GameObject gameObject)
        {
            this.gameObject = gameObject;
            foreach(Render render in renders)
            {
                render.ConnectionUpdate(gameObject);
            }
        }

        public override void Show(ref Canvas2DContext context, int xoffset, int yoffset)
        {
            foreach (Render render in renders)
            {
                render.Show(ref context, xoffset, yoffset);
            }
        }

    }
}
