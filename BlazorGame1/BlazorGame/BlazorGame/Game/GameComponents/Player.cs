using Blazor.Extensions.Canvas.Canvas2D;
using BlazorGame.Game.Builder;
using BlazorGame.Game.GameObjects;
using System.Numerics;

namespace BlazorGame.Game.GameComponents
{
    public class Player : ObjectComponent
    {
        public static readonly float DefaultMoveSpeed = 200f;
        public static readonly float DefaultDamage = 1f;
        public static readonly float DefaultHealth = 50f;
        public static readonly float DefaultExperiance = 50f;

        public static readonly float MinCollectiblesDist = 150f;
        public static readonly float MaxCollectiblesDist = 300f;
        public static readonly float DespawnCollectiblesDist = 450f;
        public static readonly int MaxCollectiblesCount = 10;

        public int id { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public float health { get; set; }
        public float maxHealth { get; set; }
        public float experiance { get; set; }
        public float maxExperiance { get; set; }
        public float moveSpeed { get; set; }
        public float damage { get; set; }
        public float[] inputs { get; set; }


        public Player(string name, int level)
        {
            this.name = name;
            maxHealth = DefaultHealth;
            health = health;
            maxExperiance = DefaultExperiance;
            moveSpeed = DefaultMoveSpeed;
            damage = DefaultDamage;
            inputs = new float[2];
        }


        public void SetInputs(int inputX, int inputY)
        {
            inputs[0] = inputX;
            inputs[1] = inputY;
        }

        public override void Update()
        {
            SpawnCollectibles();

            //health update
        }
        public override void CollisonTrigger(int gameObject, string data, int number)
        {

        }
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
                    float len = MathF.Sqrt(x * x + y * y);
                    x = x / len;
                    y = y / len;
                    float dist = MinCollectiblesDist + (MaxCollectiblesDist - MinCollectiblesDist) * random.NextSingle();
                    x *= dist;
                    y *= dist;
                    x += gameObject.position[0];
                    y += gameObject.position[1];
                    //Console.WriteLine(x + " " + y);
                    CollectibleBuilder collectibleBuilder = new CollectibleBuilder(new float[] { x, y });
                    Director.director.Construct(ref collectibleBuilder);
                    MainFrame.Instantiate(collectibleBuilder.GetResult());
                }
            }

        }
        public override void ConnectionUpdate()
        {

        }
    }
}
