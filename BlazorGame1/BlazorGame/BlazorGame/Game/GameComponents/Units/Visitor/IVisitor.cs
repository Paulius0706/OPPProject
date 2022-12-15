using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents.Units.Visitor
{
    // bug catcher
    public interface IVisitor
    {
        public abstract void Visit(GameObject obj);

    }
}
