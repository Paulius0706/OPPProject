using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameComponents.Colliders;
using BlazorGame.Game.GameComponents.Units;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents
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
            this.maxHealth = health;
            this.health = maxHealth;
            this.shooter = shooter;
        }

        public override void Update()
        {
            time += MainFrame.DeltaTime;
            if (time > lifeTime)
            {
                MainFrame.Destroy(gameObject);
            }
        }
        public override void CollisonTrigger(int gameObject)
        {
            if(hitObject == -1)
            {
                //do damage
                if (MainFrame.GameObjects[gameObject].AbstarctContainsComponent<Unit>() && shooter != gameObject && MainFrame.GameObjects[gameObject] is not BulletObject)
                {
                    MainFrame.GameObjects[gameObject].AbstractGetComponent<Unit>().TakeDamage(this.gameObject.Id, bodyDamage);
                }
                if(MainFrame.GameObjects[gameObject] is BulletObject && shooter != MainFrame.GameObjects[gameObject].GetComponent<Bullet>().shooter)
                {
                    MainFrame.GameObjects[gameObject].AbstractGetComponent<Unit>().TakeDamage(this.gameObject.Id, bodyDamage);
                }
                //check if object on verge of death( it will disapear next frame)
                if (MainFrame.GameObjects[gameObject].AbstractGetComponent<Unit>().destroyedBy == this.gameObject.Id
                    && MainFrame.GameObjects.ContainsKey(shooter)
                    && MainFrame.GameObjects[shooter].ContainsComponent<Player>())
                {
                    MainFrame.GameObjects[shooter].GetComponent<Player>().GiveExp(MainFrame.GameObjects[gameObject].AbstractGetComponent<Unit>().CalculateDeathExp());
                }
                //checks what tipe of object it is
                if(MainFrame.GameObjects[gameObject] is  not BulletObject && (gameObject != shooter))
                {
                    hitObject = gameObject;
                    OnDestroy();
                    MainFrame.Destroy(this.gameObject);
                }

            }
            

        }
        public override void ConnectionUpdate()
        {
            
        }

        public override void OnDestroy()
        {
            
        }

        public override float CalculateDeathExp(){return 0;}
    }
}
