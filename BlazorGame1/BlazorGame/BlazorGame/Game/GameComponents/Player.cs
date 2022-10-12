using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.Builder;
using BlazorGame.Game.GameComponents.RendersDecorum.Decorator;
using BlazorGame.Game.GameComponents.RendersDecorum;
using BlazorGame.Game.GameObjects;
using BlazorGame.Game.GameObjects.Factories;
using System.Numerics;
using System.Drawing;

namespace BlazorGame.Game.GameComponents
{
    public class Player : ObjectComponent
    {
        public static readonly float DefaultMoveSpeed = 200f;
        public static readonly float DefaultDamage = 1f;
        public static readonly float DefaultBodyDamage = 1f;
        public static readonly float DefaultHealth = 20f;
        public static readonly float DefaultExperiance = 10f;

        public static readonly float MinCollectiblesDist = 150f;
        public static readonly float MaxCollectiblesDist = 300f;
        public static readonly float DespawnCollectiblesDist = 450f;
        public static readonly int MaxCollectiblesCount = 10;

        public int id { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public int atributePoints { get; set; }
        public float health { get; set; }
        public float maxHealth { get; set; }
        public float experiance { get; set; }
        public float maxExperiance { get; set; }
        public float moveSpeed { get; set; }
        public float damage { get; set; }
        public float bodyDamage { get; set; }
        public float[] inputs { get; set; }
        public Spawner spawner { get; set; }



        public Player(string name, int level)
        {
            this.name = name;
            maxHealth = DefaultHealth;
            health = maxHealth;
            atributePoints = 0;
            experiance = 0;
            maxExperiance = DefaultExperiance;
            moveSpeed = DefaultMoveSpeed;
            damage = DefaultDamage;
            bodyDamage = DefaultBodyDamage;
            inputs = new float[2];
        }
        public void GiveExp(float exp)
        {
            experiance += exp;
            while(experiance >= maxExperiance)
            {
                level++;
                atributePoints++;
                experiance -= maxExperiance;
                maxExperiance = CalculateMaxExp();
            }
        }


        public void SetInputs(int inputX, int inputY)
        {
            inputs[0] = inputX;
            inputs[1] = inputY;
        }

        public override void Update()
        {
            SpawnCollectibles();
            if (healthDecorator != null) healthDecorator.Update(health, maxHealth);
            if (experienceDecorator != null) experienceDecorator.Update(experiance, maxExperiance);
            if (spawner != null) spawner.Mutate();

            //health update
        }
        public override void CollisonTrigger(int gameObject)
        {
            //Console.WriteLine("Collision Trigger: " + this.gameObject.id +" HP:"+health);
            switch (MainFrame.gameObjects[gameObject].objectType)
            {
                case GameObject.ObjectType.collectible: TakeDamage(gameObject, (MainFrame.gameObjects[gameObject].components[typeof(Collectible)] as Collectible).bodyDamage); break;
                case GameObject.ObjectType.bullet:      TakeDamage(gameObject, (MainFrame.gameObjects[gameObject].components[typeof(Bullet)] as Bullet).damage); break;
                case GameObject.ObjectType.player:      TakeDamage(gameObject, (MainFrame.gameObjects[gameObject].components[typeof(Player)] as Player).bodyDamage); break;
                case GameObject.ObjectType.mob:         TakeDamage(gameObject, (MainFrame.gameObjects[gameObject].components[typeof(Collectible)] as Collectible).bodyDamage); break;
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
                        int playerId = (MainFrame.gameObjects[gameObject].components[typeof(Bullet)] as Bullet).shooter;
                        if (MainFrame.gameObjects.ContainsKey(playerId)) { (MainFrame.gameObjects[playerId].components[typeof(Player)] as Player).GiveExp(CalculateDeathExp()); }
                        break;
                    //case of player object damage
                    case GameObject.ObjectType.player:
                        (MainFrame.gameObjects[gameObject].components[typeof(Player)] as Player).GiveExp(CalculateDeathExp());
                        break;
                }
                MainFrame.Destroy(this.gameObject);
            }
        }
        public float CalculateDeathExp(){ return level * 10;}
        public float CalculateMaxExp() { return DefaultExperiance + DefaultExperiance * (level - 1) * 2f; }
        public void UpdateVelocity()
        {
            float[] currentVelocity = gameObject.velocity;

            float x = currentVelocity[0] > 50f && inputs[0] > 0f ? 0f : inputs[0];
            x = currentVelocity[0] < -50f && inputs[0] < 0f ? 0f : inputs[0];
            float y = currentVelocity[1] > 50f && inputs[1] > 0f ? 0f : inputs[1];
            y = currentVelocity[1] < -50f && inputs[1] < 0f ? 0f : inputs[1];
            currentVelocity[0] += x;
            currentVelocity[1] += y;

            gameObject.velocity = currentVelocity;
        }
        public void SpawnCollectibles()
        {
            int counter = 0;
            foreach (GameObject gameObject in MainFrame.gameObjects.Values)
            {
                if (MathF.Abs(gameObject.position[0] - this.gameObject.position[0]) < DespawnCollectiblesDist
                    && Math.Abs(gameObject.position[1] - this.gameObject.position[1]) < DespawnCollectiblesDist) counter++;
                if (counter > MaxCollectiblesCount) break;
            }
            if (counter < MaxCollectiblesCount)
            {
                Random random = new Random();
                if (random.NextDouble() < 1 * MainFrame.detaTime)
                {
                    float x = random.NextSingle() * 2f - 1f;
                    float y = random.NextSingle() * 2f - 1f;
                    x += x > 0 ? 
                        MinCollectiblesDist + (MaxCollectiblesDist - MinCollectiblesDist) * x + gameObject.position[0] 
                     : -MinCollectiblesDist + (MaxCollectiblesDist - MinCollectiblesDist) * x + gameObject.position[0];
                    y += y > 0 ? 
                        MinCollectiblesDist + (MaxCollectiblesDist - MinCollectiblesDist) * y + gameObject.position[1] 
                     : -MinCollectiblesDist + (MaxCollectiblesDist - MinCollectiblesDist) * y + gameObject.position[1];

                    if(spawner != null)
                    {
                        CollectibleObject collectibleObject = spawner.collectibleObject.Clone();
                        collectibleObject.position = new float[] { x, y };
                        MainFrame.Instantiate(collectibleObject);
                    }
                }
            }

        }

        public override void ConnectionUpdate()
        {
            spawner = new Spawner(new BaseFactory(gameObject));

            for (int i = 0; i < renders.renders.Count; i++)
            {
                if (renders.renders[i] is HealthDecorator)
                {
                    healthRenderId = i;
                    healthDecorator.ConnectionUpdate(gameObject);
                }
                if (renders.renders[i] is ExperienceDecorator)
                {
                    expRenderId = i;
                    experienceDecorator.ConnectionUpdate(gameObject);
                }
            }
        }

        public Renders renders
        {
            get
            {
                if (gameObject == null) return null;
                if (!gameObject.components.ContainsKey(typeof(Renders))) return null;
                return (gameObject.components[typeof(Renders)] as Renders);
            }
            set
            {
                if (gameObject != null && gameObject.components.ContainsKey(typeof(Renders)))
                    gameObject.components[typeof(Renders)] = value;
            }
        }
        private int expRenderId = -1;
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
        public ExperienceDecorator experienceDecorator
        {
            get
            {
                if (renders == null || expRenderId == -1) return null;
                if (renders.renders.Count <= expRenderId) return null;
                if (renders.renders[expRenderId] is not ExperienceDecorator) return null;
                return renders.renders[expRenderId] as ExperienceDecorator;
            }
            set
            {
                if (expRenderId != -1 && renders != null && renders.renders.Count > expRenderId && renders.renders[expRenderId] is ExperienceDecorator)
                    renders.renders[expRenderId] = value;
            }
        }
    }
}
