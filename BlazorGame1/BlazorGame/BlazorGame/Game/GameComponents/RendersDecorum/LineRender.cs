using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents.RendersDecorum
{
    public class LineRender : Render
    {
        public int[] offset1 { get; set; }
        public int width { get; set; }
        public string color { get; set; }

        public LineRender(int[] offset, int[] offset1, int width, string color)
        {
            //this.type = type;
            this.offset = offset;
            this.offset1 = offset1;
            this.width = width;
            this.color = color;
        }

        public override void Show(ref Canvas2DContext context, int xoffset, int yoffset)
        {
            context.SetFillStyleAsync(color);
            context.SetLineWidthAsync(width);
            context.BeginPathAsync();
            context.MoveToAsync(
                gameObject.Position[0] + offset[0] - xoffset,
                gameObject.Position[1] + offset[1] - yoffset);
            context.LineToAsync(
                gameObject.Position[0] + offset1[0] - xoffset,
                gameObject.Position[1] + offset1[1] - yoffset);
            context.StrokeAsync();
        }
        public override void ConnectionUpdate(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}
