using Blazor.Extensions.Canvas.Canvas2D;

namespace BlazorGame.Game
{
    public abstract class ObjectComponent
    {
        public static int idCounter = 0;
        public int id;
        public int gameObject { get; set; } // gameobject ObjectComponent attached to

        public ObjectComponent() { }

        public abstract void Update();

        public abstract void Render(int playerId, ref Canvas2DContext context);
    }
}
