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

        public int id { get; set; }
        public string name { get; set; }
        public int level { get; set; }
        public float health { get; set; }
        public float maxHealth { get; set; }
        public float experiance { get; set; }
        public float maxExperiance { get; set; }
        public float moveSpeed { get; set; }
        public float[] inputs { get; set; }

        public List<Render> renders;

        public Player(string name, int level)
        {
            this.name = name;
            this.maxHealth = DefaultHealth;
            this.health = health;
            this.maxExperiance = DefaultExperiance;
            this.moveSpeed = DefaultMoveSpeed;
            this.inputs = new float[2];
        }

        public void CorrectInputs(int x, int y)
        {
            //x = x != 0 ? (x > 0 ? 1 : (x < 0 ? -1 : (int)inputs[0])) : (int)inputs[0];
            //y = y != 0 ? (y > 0 ? 1 : (y < 0 ? -1 : (int)inputs[1])) : (int)inputs[1];

            x = x == 0 ? (int)inputs[0] : x;
            y = y == 0 ? (int)inputs[1] : y;

            inputs = new float[] { x, y };
        }

        public override void Render(int playerId, ref Canvas2DContext context) { }
        public override void Update()
        {
            //UpdateVelocity();
            inputs = new float[] { 0, 0 };
        }
        public void UpdateVelocity()
        {
            float[] currentVelocity = MainFrame.gameObjects[gameObject].velocity;

            float x = currentVelocity[0] > 50f && inputs[0] > 0f ? 0f : inputs[0];
            x = currentVelocity[0] < -50f && inputs[0] < 0f ? 0f : inputs[0];
            float y = currentVelocity[1] > 50f && inputs[1] > 0f ? 0f : inputs[1];
            y = currentVelocity[1] < -50f && inputs[1] < 0f ? 0f : inputs[1];
            currentVelocity[0] += x;
            currentVelocity[1] += y;

            Game.MainFrame.gameObjects[gameObject].velocity = currentVelocity;
        }
    }
}
