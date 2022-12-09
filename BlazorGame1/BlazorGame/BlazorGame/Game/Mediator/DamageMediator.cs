using BlazorGame.Game.GameComponents.Units;

namespace BlazorGame.Game.Mediator
{
    public class DamageMediator : ObjectsMediator
    {
        // ConcreteColleagues are Mainframe.gameobjects

        private static DamageMediator damageMediator = new DamageMediator();
        public static DamageMediator GetInstance() { return damageMediator; }
        public override void Collision(Unit unit1, Unit unit2)
        {
            unit1.TakeDamage(unit2.GameObject.Id, unit2.bodyDamage);
            unit2.TakeDamage(unit1.GameObject.Id, unit1.bodyDamage);
        }

    }
}
