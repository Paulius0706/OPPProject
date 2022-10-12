using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents.RendersDecorum
{
    public class ImageRender : Render
    {
        public int[] size { get; set; }
        public string image { get; set; }

        public ImageRender(int[] offset, int[] size, string image)
        {
            this.offset = offset;
            this.size = size;
            this.image = image;
        }

        public override void Show(ref Canvas2DContext context, int xoffset, int yoffset)
        {
            throw new NotImplementedException();
        }
        public override void ConnectionUpdate(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }
    }
}
