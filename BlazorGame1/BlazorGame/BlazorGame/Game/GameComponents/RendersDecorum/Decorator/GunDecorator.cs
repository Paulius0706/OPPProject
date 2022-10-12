namespace BlazorGame.Game.GameComponents.RendersDecorum.Decorator
{
    public class GunDecorator : DecoratorRender
    {
        public enum Type
        {
            gun = 1,
            detail = 0
        }
        public GunDecorator(LineRender main, LineRender details)
        {
            renders = new List<Render>();
            renders.Add(details);
            renders.Add(main);
        }
    }
}
