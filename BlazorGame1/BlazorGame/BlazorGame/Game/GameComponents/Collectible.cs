using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameComponents.RendersDecorum.Decorator;
using BlazorGame.Game.GameComponents.RendersDecorum;
using BlazorGame.Game.GameObjects;

namespace BlazorGame.Game.GameComponents
{
    public class Collectible : ObjectComponent
    {
        public int exp;
        public float health;
        public float maxHealth;
        public float bodyDamage;
        public CollectibleType type;
        public enum CollectibleType
        {
            col1,
            col2,
            col3,
            mob
        }

        public Collectible(CollectibleType collectibleType)
        {
            type = collectibleType;
            switch (collectibleType)
            {
                case CollectibleType.col1:
                    exp = 2;
                    health = 2;
                    maxHealth = health;
                    bodyDamage = 2;
                    break;
                case CollectibleType.col2:
                    exp = 8;
                    health = 6;
                    maxHealth = health;
                    bodyDamage = 4;
                    break;
                case CollectibleType.col3:
                    exp = 32;
                    health = 18;
                    maxHealth = health;
                    bodyDamage = 12;
                    break;
            }
        }
        public override void Update()
        {
            bool delete = true;
            
            // despawn
            foreach (GameObject gameObject in MainFrame.gameObjects.Values)
            {
                if (gameObject.objectType == GameObject.ObjectType.player
                    && MathF.Abs(gameObject.position[0] - this.gameObject.position[0]) <= Player.DespawnCollectiblesDist
                    && Math.Abs(gameObject.position[1] - this.gameObject.position[1]) <= Player.DespawnCollectiblesDist)
                {
                    delete = false;
                    break;
                }
            }
            if (delete) { MainFrame.Destroy(gameObject); }

            if (healthDecorator != null) healthDecorator.Update(health, maxHealth);
        }
        public override void CollisonTrigger(int gameObject)
        {
            //Console.WriteLine("Collectible Collision Trigger: " + this.gameObject.id + " HP:" + health);
            switch (MainFrame.gameObjects[gameObject].objectType)
            {
                case GameObject.ObjectType.collectible: 
                    
                    break;
                case GameObject.ObjectType.bullet: TakeDamage(gameObject, MainFrame.gameObjects[gameObject].GetComponent<Bullet>().damage); break;
                case GameObject.ObjectType.player: TakeDamage(gameObject, MainFrame.gameObjects[gameObject].GetComponent<Player>().bodyDamage); break;
                case GameObject.ObjectType.mob: break;
                case GameObject.ObjectType.undentified: break;
            }
        }
        public void TakeDamage(int gameObject, float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                switch (MainFrame.gameObjects[gameObject].objectType)
                {
                    //case of bullet damage
                    case GameObject.ObjectType.bullet:
                        int playerId = MainFrame.gameObjects[gameObject].GetComponent<Bullet>().shooter;
                        if (MainFrame.gameObjects.ContainsKey(playerId)) { MainFrame.gameObjects[playerId].GetComponent<Player>().GiveExp(exp); }
                        break;
                    //case of player object damage
                    case GameObject.ObjectType.player:
                        MainFrame.gameObjects[gameObject].GetComponent<Player>().GiveExp(exp);
                        break;
                }
                MainFrame.Destroy(this.gameObject);
            }
        }
        public override void ConnectionUpdate()
        {
            for (int i = 0; i < renders.renders.Count; i++)
            {
                if (renders.renders[i] is HealthDecorator)
                {
                    healthRenderId = i;
                    healthDecorator.ConnectionUpdate(gameObject);
                    break;
                }
            }
        }

        public Renders renders
        {
            get
            {
                if (gameObject == null) return null;
                if (!gameObject.ContainsComponent<Renders>()) return null;
                return (gameObject.GetComponent<Renders>());
            }
            set
            {
                if (gameObject != null && gameObject.ContainsComponent<Renders>())
                    renders = value;
            }
        }
        private int healthRenderId = -1;
        public HealthDecorator healthDecorator
        {
            get
            {
                if (renders == null || healthRenderId == -1) return null;
                if (renders.renders.Count <= healthRenderId) return null;
                if (renders.renders[healthRenderId] is not HealthDecorator) return null;
                return renders.renders[healthRenderId] as HealthDecorator;
            }
            set
            {
                if (healthRenderId != -1 && renders != null && renders.renders.Count > healthRenderId && renders.renders[healthRenderId] is HealthDecorator)
                    renders.renders[healthRenderId] = value;
            }
        }
    }
}
