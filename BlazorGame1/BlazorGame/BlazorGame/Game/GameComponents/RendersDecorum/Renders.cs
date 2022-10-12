using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameComponents;

namespace BlazorGame.Game.GameComponents.RendersDecorum
{
    public class Renders : ObjectComponent
    {
        public List<Render> renders;
        public Renders()
        {
            renders = new List<Render>();
        }

        public override void CollisonTrigger(int gameObject){ }

        public override void ConnectionUpdate()
        {
            foreach (Render render in renders)
            {
                render.ConnectionUpdate(gameObject);
            }
        }
        public void Render(ref Canvas2DContext context, int xoffset, int yoffset)
        {
            foreach (Render render in renders)
            {
                render.Show(ref context, xoffset, yoffset);
            }
        }

        public override void Update(){}
    }
}

