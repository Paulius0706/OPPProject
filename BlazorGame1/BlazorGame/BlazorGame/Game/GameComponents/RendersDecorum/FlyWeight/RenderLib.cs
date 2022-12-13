using BlazorGame.Game.GameComponents.RendersDecorum.Decorator;

namespace BlazorGame.Game.GameComponents.RendersDecorum.FlyWeight
{
    public class RenderLib
    {
        public static Dictionary<string, Render> renders = new Dictionary<string, Render>();

        public static void Load()
        {
            renders.Add("bulletNormal", new CircleRender(new int[] { 0, 0 }, 15, "red"));
            BoxRender healthBorder = new BoxRender(new int[] { -40, 28 }, new int[] { 80, 10 }, "grey");
            BoxRender healthBackGround = new BoxRender(new int[] { -39, 29 }, new int[] { 78, 8 }, "black");
            renders.Add("playerHealthBarFiller", 
                new FillerSpaceDecorator(
                    new BoxRender(new int[] { -39, 29 }, new int[] { 78, 8 }, "black"),
                    new BoxRender(new int[] { -40, 28 }, new int[] { 80, 10 }, "grey")
                    )
                );
        }
        public static Render Get(string key)
        {
            return renders[key];
        }
    }
}
