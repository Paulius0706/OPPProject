using Blazor.Extensions.Canvas.Canvas2D;
using System.Numerics;

namespace BlazorGame.Game
{
    public class Player : ObjectComponent
    {
        public static readonly float DefaultVelocity = 100f;
        public static readonly float DefaultMoveSpeed = 50f;
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
        public float[] inputs { get; set; }

        public Cannon cannon { get; set; }

        public Player(string name, int level)
        {
            this.name = name;
            this.maxHealth = DefaultHealth;
            this.health = health;
            this.maxExperiance = DefaultExperiance;
            this.moveSpeed = DefaultMoveSpeed;
            this.inputs = new float[2];
            this.cannon = new Cannon();
        }


        public void SetInputs(int inputX, int inputY)
        {
            inputs[0] = inputX;
            inputs[1] = inputY;
        }

        /// <summary>
        /// [depricated]
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void CorrectInputs(int x, int y)
        {
            //x = x != 0 ? (x > 0 ? 1 : (x < 0 ? -1 : (int)inputs[0])) : (int)inputs[0];
            //y = y != 0 ? (y > 0 ? 1 : (y < 0 ? -1 : (int)inputs[1])) : (int)inputs[1];

            x = x == 0 ? (int)inputs[0] : x;
            y = y == 0 ? (int)inputs[1] : y;

            inputs = new float[] { x, y };
        }

        public override void Update()
        {
            cannon.Update();
            SpawnCollectibles();
        }
        public override void CollisonTrigger(int gameObject, string data, int number)
        {

        }
        public void UpdateVelocity()
        {
            float[] currentVelocity = MainFrame.gameObjects[base.gameObject].velocity;

            float x = currentVelocity[0] > 50f && inputs[0] > 0f ? 0f : inputs[0];
            x = currentVelocity[0] < -50f && inputs[0] < 0f ? 0f : inputs[0];
            float y = currentVelocity[1] > 50f && inputs[1] > 0f ? 0f : inputs[1];
            y = currentVelocity[1] < -50f && inputs[1] < 0f ? 0f : inputs[1];
            currentVelocity[0] += x;
            currentVelocity[1] += y;

            Game.MainFrame.gameObjects[base.gameObject].velocity = currentVelocity;
        }
        public void SpawnCollectibles()
        {
            int counter = 0;
            foreach(GameObject gameObject in MainFrame.gameObjects.Values)
            {
                if (MathF.Abs(gameObject.position[0] - MainFrame.gameObjects[this.gameObject].position[0]) < DespawnCollectiblesDist 
                    && Math.Abs(gameObject.position[1] - MainFrame.gameObjects[this.gameObject].position[1]) < DespawnCollectiblesDist) counter++;
                if (counter > MaxCollectiblesCount) break;
            }
            if(counter < MaxCollectiblesCount)
            {
                System.Random random = new Random();
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
                    x += MainFrame.gameObjects[gameObject].position[0];
                    y += MainFrame.gameObjects[gameObject].position[1];
                    //Console.WriteLine(x + " " + y);
                    GameObject newGameObject = GameObject.CreateCollectibe(new float[] { x, y });
                    MainFrame.createGameObjectsQueue.Enqueue(newGameObject);
                }
            }
            
        }
    }
}
