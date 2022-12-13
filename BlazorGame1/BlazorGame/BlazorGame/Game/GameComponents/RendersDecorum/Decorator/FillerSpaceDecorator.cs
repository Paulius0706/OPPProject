using Microsoft.AspNetCore.Components.RenderTree;

namespace BlazorGame.Game.GameComponents.RendersDecorum.Decorator
{
    public class FillerSpaceDecorator : DecoratorRender
    {
        public enum Type
        {
            backGround = 0,
            fill = 1,
        }
        public int fillWidth { get; private set; }
        public FillerSpaceDecorator(BoxRender fill, BoxRender backGround)
        {
            renders = new List<Render>();
            renders.Add(backGround);
            renders.Add(fill);
            fillWidth = fill.size[0];
        }

        public void Update(float exp, float maxExp)
        {
        }
    }
}
