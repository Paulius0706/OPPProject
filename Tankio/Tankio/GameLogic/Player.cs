using System.Numerics;

namespace Tankio.GameLogic
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
        public Vector2 inputs { get; set; }

        public Player(string name, int level)
        {
            this.name = name;
            this.maxHealth = DefaultHealth;
            this.health = health;
            this.maxExperiance = DefaultExperiance;
            this.inputs = new Vector2();
        }

        public void CorrectInputs(int x, int y)
        {
            x = x != 0 ? (x > 0 ? 1 : (x < 0 ? -1 : 0)) : 0;
            y = y != 0 ? (y > 0 ? 1 : (y < 0 ? -1 : 0)) : 0;

            inputs = new Vector2(x, y);
        }

        public override void Inputting(int x, int y)
        {
            CorrectInputs(x, y);
        }
        public override void Render(){}
        public override void Update(){
            UpdateVelocity();
        }
        public void UpdateVelocity()
        {
            Vector2 currentVelocity = MainFarme.gameObjects[gameObject].velocity;

            float x = currentVelocity.X > 50f && inputs.X > 0f ? 0f : inputs.X;
            x = currentVelocity.X < -50f && inputs.X < 0f ? 0f : inputs.X;
            float y = currentVelocity.Y > 50f && inputs.Y > 0f ? 0f : inputs.Y;
            y = currentVelocity.Y < -50f && inputs.Y < 0f ? 0f : inputs.Y;
            currentVelocity += new Vector2(x, y);
            MainFarme.gameObjects[gameObject].velocity = currentVelocity;
        }
    }
}
