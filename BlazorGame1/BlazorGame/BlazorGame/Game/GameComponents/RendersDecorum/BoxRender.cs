using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents.RendersDecorum
{
    public class BoxRender : Render
    {
        public int[] size { get; set; }
        public string color { get; set; }

        public BoxRender(int[] offset, int[] size, string color)
        {
            this.offset = offset;
            this.size = size;
            this.color = color;
        }

        public override void Show(ref Canvas2DContext context, int xoffset, int yoffset)
        {
            context.SetFillStyleAsync(color);
            context.FillRectAsync(
                gameObject.position[0] + offset[0] - xoffset,
                gameObject.position[1] + offset[1] - yoffset,
                size[0], size[1]
                );
        }
        public override void ConnectionUpdate(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}
