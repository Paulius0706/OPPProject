using Blazor.Extensions.Canvas.Canvas2D;

namespace BlazorGame.Game
{
    public class Bullet : ObjectComponent
    {
        public static readonly float lifeTime = 5f;
        public int health;
        public int damage;

        public float time;

        public Bullet(int damage, int health)
        {
            time = 0;
            this.damage = damage;
            this.health = health;
        }

        public override void Update()
        {
            time += MainFrame.detaTime;
            if(time > lifeTime)
            {
                MainFrame.destroyGameObjectsQueue.Enqueue(gameObject);
            }
        }
        public override void CollisonTrigger(int gameObject, string data, int number)
        {

        }
    }
}
