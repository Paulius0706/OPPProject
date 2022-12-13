using BlazorGame.Game.Mediator;
using System.Reflection.Emit;

namespace BlazorGame.Game.GameComponents.Units
{
    public abstract class Unit : ObjectComponent
    {
        public DamageMediator damageMediator = DamageMediator.GetInstance();
        public ExpMediator expMediator = ExpMediator.GetInstance();

        public float health { get; set; }
        public float maxHealth { get; set; }
        public float bodyDamage { get; set; }
        public float experiance { get; set; }
        
        public int destroyedBy = -1;

        public void TakeDamage(int gameObject, float damage)
        {
            health -= damage;
            if (health <= 0 && destroyedBy == -1)
            {
                destroyedBy = gameObject;
                OnDestroy();
                expMediator.Collision(MainFrame.GameObjects[gameObject].AbstractGetComponent<Unit>(), this);
                Destroy();
            }
        }
        public abstract void OnDestroy();
        public void Destroy()
        {
            MainFrame.Destroy(this.GameObject);
        }
        public abstract float CalculateDeathExp();
    }
}
