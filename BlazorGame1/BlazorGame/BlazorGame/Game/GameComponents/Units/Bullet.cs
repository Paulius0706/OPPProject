using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameComponents.Colliders;
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
            if (hitObject == -1)
            {
                //do damage
                if (MainFrame.GameObjects[gameObject].AbstarctContainsComponent<Unit>() && shooter != gameObject && MainFrame.GameObjects[gameObject] is not BulletObject)
                {
                    MainFrame.GameObjects[gameObject].AbstractGetComponent<Unit>().TakeDamage(GameObject.Id, bodyDamage);
                }
                if (MainFrame.GameObjects[gameObject] is BulletObject && shooter != MainFrame.GameObjects[gameObject].GetComponent<Bullet>().shooter)
                {
                    MainFrame.GameObjects[gameObject].AbstractGetComponent<Unit>().TakeDamage(GameObject.Id, bodyDamage);
                }
                //check if object on verge of death( it will disapear next frame)
                if (MainFrame.GameObjects[gameObject].AbstractGetComponent<Unit>().destroyedBy == GameObject.Id
                    && MainFrame.GameObjects.ContainsKey(shooter)
                    && MainFrame.GameObjects[shooter].ContainsComponent<Player>())
                {
                    MainFrame.GameObjects[shooter].GetComponent<Player>().GiveExp(MainFrame.GameObjects[gameObject].AbstractGetComponent<Unit>().CalculateDeathExp());
                }
                //checks what tipe of object it is
                if (MainFrame.GameObjects[gameObject] is not BulletObject && gameObject != shooter)
                {
                    hitObject = gameObject;
                    OnDestroy();
                    MainFrame.Destroy(GameObject);
                }

            }


        }
        public override void ConnectionUpdate()
        {

        }

        public override void OnDestroy()
        {

        }

        public override float CalculateDeathExp() { return 0; }
    }
}
