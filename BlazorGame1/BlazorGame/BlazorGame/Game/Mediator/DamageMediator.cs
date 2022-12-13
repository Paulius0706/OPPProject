using BlazorGame.Game.GameComponents.Units;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.Mediator
{
    public class DamageMediator : ObjectsMediator
    {
        // ConcreteColleagues are Mainframe.gameobjects
        List<(int, int)> damageLog = new List<(int, int)>();

        private static DamageMediator damageMediator = new DamageMediator();
        public static DamageMediator GetInstance() { return damageMediator; }
        public override void Collision(Unit unit1, Unit unit2)
        {
            if (damageLog.Contains((unit1.GameObject.Id, unit2.GameObject.Id)) || damageLog.Contains((unit2.GameObject.Id, unit1.GameObject.Id))) return;
            damageLog.Add((unit1.GameObject.Id, unit2.GameObject.Id));
            if (!(unit1 is Player && unit2 is Bullet && (unit2 as Bullet).shooter == unit1.GameObject.Id)) unit1.TakeDamage(unit2.GameObject.Id, unit2.bodyDamage);
            if (!(unit2 is Player && unit1 is Bullet && (unit1 as Bullet).shooter == unit2.GameObject.Id)) unit2.TakeDamage(unit1.GameObject.Id, unit1.bodyDamage);
            if(unit1 is not Bullet || unit2 is not Bullet)
            {
                if (unit1 is BulletObject) unit1.Destroy();
                if (unit2 is BulletObject) unit1.Destroy();
            }
        }
        public static void Update()
        {
            damageMediator.damageLog.Clear();
        }


    }
}
