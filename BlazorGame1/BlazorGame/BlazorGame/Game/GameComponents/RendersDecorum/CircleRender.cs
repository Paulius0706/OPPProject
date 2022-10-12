using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents.RendersDecorum
{
    public class CircleRender : Render 
    {
        public int radius { get; set; }
        public string color { get; set; }

        public CircleRender(int[] offset, int radius, string color)
        {
            this.offset = offset;
            this.radius = radius;
            this.color = color;
        }

        public override void Show(ref Canvas2DContext context, int xoffset, int yoffset)
        {
            
            context.BeginPathAsync();
            context.SetFillStyleAsync(color);
            context.SetLineWidthAsync(1);
            context.ArcAsync(
                (int)gameObject.position[0] + offset[0] - xoffset,
                (int)gameObject.position[1] + offset[1] - yoffset,
                radius, 0, 2 * Math.PI
                );
            context.ClosePathAsync();
            context.FillAsync();
            context.StrokeAsync();
        }
        public override void ConnectionUpdate(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}
