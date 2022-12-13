using BlazorGame.Game.GameComponents.Units;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.Mediator
{
    public class ExpMediator : ObjectsMediator
    {
        private static ExpMediator expMediator = new ExpMediator();
        public static ExpMediator GetInstance() { return expMediator; }
        public override void Collision(Unit to, Unit from)
        {

            if ((!(to is Player) && !(to is Bullet))) return;
            Console.WriteLine(to.GameObject.Id + "<=" + from.GameObject.Id);
            if (to is Player) { (to as Player).GiveExp(from.CalculateDeathExp()); } 
            else if ((to is Bullet) && MainFrame.GameObjects.ContainsKey((to as Bullet).shooter)) { MainFrame.GameObjects[(to as Bullet).shooter].GetComponent<Player>().GiveExp(from.CalculateDeathExp()); }
        }
    }
}
