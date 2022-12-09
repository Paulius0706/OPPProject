using BlazorGame.Game.GameComponents.Units;

namespace BlazorGame.Game.Mediator
{
    public abstract class ObjectsMediator
    {
        public abstract void Collision(Unit unit1, Unit unit2);
    }
}
