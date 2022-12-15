using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameComponents;
using BlazorGame.Game.GameComponents.Units.Visitor;

namespace BlazorGame.Game.GameComponents.RendersDecorum
{
    public class Renders : ObjectComponent
    {
        public List<Render> renders;
        public Renders()
        {
            renders = new List<Render>();
        }

        public override void accept(IVisitor visitor)
        {

        }

        public override void CollisonTrigger(int gameObject){ }

        public override void ConnectionUpdate()
        {
            foreach (Render render in renders)
            {
                render.ConnectionUpdate(GameObject);
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

