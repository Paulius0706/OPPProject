using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameComponents.Colliders;
using BlazorGame.Game.GameComponents.Units.Visitor;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents.Units
{
    public class Bullet : Unit
    {
        public static readonly float lifeTime = 5f;
        public int shooter;
        public int hitObject = -1;

        public float time;

        public Bullet(float bodyDamage, float health, int shooter)
        {
            time = 0;
            this.bodyDamage = bodyDamage;
            maxHealth = health;
            this.health = maxHealth;
            this.shooter = shooter;
        }

        public override void Update()
        {
            time += MainFrame.DeltaTime;
            if (time > lifeTime)
            {
                MainFrame.Destroy(GameObject);
            }
        }
        public override void CollisonTrigger(int gameObject)
        {
            damageMediator.Collision(this, MainFrame.GameObjects[gameObject].AbstractGetComponent<Unit>());
        }
        public override void ConnectionUpdate()
        {

        }
        public override void OnDestroy()
        {

        }

        public override float CalculateDeathExp() { return 0; }

        public void Accept()
        {

        }

        public override void accept(Visitor.IVisitor visitor)
        {

        }
    }
}
