using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents
{
    public class Bullet : ObjectComponent
    {
        public static readonly float lifeTime = 5f;
        public int shooter;
        public float health;
        public float damage;

        public float time;

        public Bullet(float damage, float health, int shooter)
        {
            time = 0;
            this.damage = damage;
            this.health = health;
            this.shooter = shooter;
        }

        public override void Update()
        {
            time += MainFrame.detaTime;
            if (time > lifeTime)
            {
                MainFrame.Destroy(gameObject);
            }
        }
        public override void CollisonTrigger(int gameObject, string data, int number)
        {

        }
        public override void ConnectionUpdate()
        {

        }
    }
}
