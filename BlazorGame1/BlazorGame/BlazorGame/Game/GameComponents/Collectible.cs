using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.GameComponents.RendersDecorum.Decorator;
using BlazorGame.Game.GameComponents.RendersDecorum;
using BlazorGame.Game.GameObjects;
using BlazorGame.Game.GameComponents.Units;

namespace BlazorGame.Game.GameComponents
{
    public class Collectible : Unit
    {
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
                    experiance = 2;
                    health = 2;
                    maxHealth = health;
                    bodyDamage = 2;
                    break;
                case CollectibleType.col2:
                    experiance = 8;
                    health = 6;
                    maxHealth = health;
                    bodyDamage = 4;
                    break;
                case CollectibleType.col3:
                    experiance = 32;
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
            foreach (GameObject gameObject in MainFrame.GameObjects.Values)
            {
                if (gameObject.objectType == GameObject.ObjectType.Player
                    && MathF.Abs(gameObject.Position[0] - this.gameObject.Position[0]) <= Player.DespawnCollectiblesDist
                    && Math.Abs(gameObject.Position[1] - this.gameObject.Position[1]) <= Player.DespawnCollectiblesDist)
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
            if (MainFrame.GameObjects[gameObject].AbstarctContainsComponent<Unit>())
            {
                MainFrame.GameObjects[gameObject].AbstractGetComponent<Unit>().TakeDamage(this.gameObject.Id, bodyDamage);
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

        public override float CalculateDeathExp()
        {
            return experiance;
        }

        public override void OnDestroy() { }

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
