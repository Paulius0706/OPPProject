using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameComponents.Colliders;
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
        public override void CollisonTrigger(int gameObject)
        {
            switch (MainFrame.gameObjects[gameObject].objectType)
            {
                case GameObject.ObjectType.collectible: MainFrame.Destroy(this.gameObject); break;
                case GameObject.ObjectType.bullet: TakeDamage(gameObject, MainFrame.gameObjects[gameObject].GetComponent<Bullet>().damage); break;
                case GameObject.ObjectType.player: MainFrame.Destroy(this.gameObject); break;
                case GameObject.ObjectType.mob: MainFrame.Destroy(this.gameObject); break;
                case GameObject.ObjectType.undentified: break;
            }
        }
        public void TakeDamage(int gameObject, float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                MainFrame.Destroy(this.gameObject);
            }
        }
        public override void ConnectionUpdate()
        {

        }
    }
}
