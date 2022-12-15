using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameComponents.RendersDecorum;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents.Units.Visitor
{
    public class RenderVisitor : IVisitor
    {
        public Canvas2DContext context;
        public int x;
        public int y;
        public void Visit(GameObject obj)
        {
            try
            {
                obj.GetComponent<Renders>().Render(ref context, x,y);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
